using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Chat;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;

public class ChatUI : MonoBehaviour, IChatClientListener
{
    public string userName;
    public GameObject connectPanel;
    public ChatClient chatClient;
    public GameObject connectingLabel;
    protected internal AppSettings chatAppSettings;
    public GameObject chatPanel;
    public Text messageText;
    public InputField messageInputField;
    public string[] channelsToAutoJoin;
    public int historyLength = 10;
    public Text userList;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        this.chatAppSettings = PhotonNetwork.PhotonServerSettings.AppSettings;
        bool appIdPresent = !string.IsNullOrEmpty(this.chatAppSettings.AppIdChat);
        // this.connectPanel.gameObject.SetActive(appIdPresent);

        if (!appIdPresent)
        {
            Debug.LogError("Chat Id is missing in settings!");
        }

        this.connectingLabel.SetActive(false);

        this.chatPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Service();
        }
    }

    public void Connect()
    {
        this.connectPanel.gameObject.SetActive(false);
        this.connectingLabel.gameObject.SetActive(true);

        this.chatClient = new ChatClient(this);
        this.chatClient.UseBackgroundWorkerForSending = true;
        this.chatClient.Connect(this.chatAppSettings.AppIdChat, "1.0",
                                    new Photon.Chat.AuthenticationValues(this.userName));
        Debug.Log("Connecting as " + this.userName);
    }

    public void DebugReturn(DebugLevel level, string message)
    {

    }

    public void OnChatStateChange(ChatState state)
    {
    }

    public void OnConnected()
    {
        Debug.Log("Connected to chat server");
        this.connectingLabel.SetActive(false);
        this.chatPanel.SetActive(true);
        this.chatClient.Subscribe("Main", 0, historyLength, creationOptions:
                                                    new ChannelCreationOptions { PublishSubscribers = true });
    }

    public void SendChatMessages(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            this.chatClient.PublishMessage(this.channelsToAutoJoin[0], message);
            this.messageInputField.text = "";
            this.messageInputField.ActivateInputField();
            this.messageInputField.Select();
        }
    }

    public void OnDisconnected()
    {

    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        ChatChannel channel = null;
        this.chatClient.TryGetChannel(channelName, out channel);
        this.messageText.text = channel.ToStringMessages();
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {

    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {

    }

    public void PrintSubscribedUsers(string channelName)
    {
        ChatChannel channel = null;
        this.chatClient.TryGetChannel(channelName, out channel);
        this.userList.text = "";
        if (!channel.Subscribers.Contains(this.chatClient.UserId))
        {
            this.userList.text += this.chatClient.UserId + '\n';
        }
        foreach (string u in channel.Subscribers)
        {
            this.userList.text += u + '\n';
        }
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {

        this.chatClient.PublishMessage(channels[0], "has joined the chat.");
        PrintSubscribedUsers(channels[0]);
    }

    public void OnUnsubscribed(string[] channels)
    {

    }

    public void OnUserSubscribed(string channel, string user)
    {
        PrintSubscribedUsers(channel);
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        PrintSubscribedUsers(channel);
    }
}
