using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatConnects : MonoBehaviour
{
    public InputField chatName;

    public void ConnectToChat()
    {
        ChatUI chatui = FindObjectOfType<ChatUI>();
        chatui.userName = this.chatName.text.Trim();
        if (!string.IsNullOrEmpty(chatui.userName))
        {
            chatui.Connect();
            enabled = false;
        }
    }
}
