using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DbConnect : MonoBehaviour
{

    public Text email;
    public Text userName;
    public Text score;
    public Text password;

    public void AddDetails()
    {
        StartCoroutine(AddPlayerDetails());
    }

    public void GetDetails()
    {
        StartCoroutine(RetrievePlayerDetails());
    }

    public void UpdateDetails()
    {
        StartCoroutine(UpdatePlayerDetails());
    }

    IEnumerator AddPlayerDetails()
    {
        Debug.Log("Adding Details");

        WWWForm form = new WWWForm();
        form.AddField("email", email.text);
        form.AddField("name", userName.text);
        form.AddField("score", score.text);
        form.AddField("password", password.text);

        WWW www = new WWW("https://jrm-project-server.000webhostapp.com/saveplayer.php", form);

        yield return www;

        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.text);
        }
    }

    IEnumerator RetrievePlayerDetails()
    {
        Debug.Log("Retrieving Details");

        WWWForm form = new WWWForm();
        form.AddField("email", email.text);
        form.AddField("password", password.text);

        WWW www = new WWW("https://jrm-project-server.000webhostapp.com/retrieveplayer.php", form);

        yield return www;

        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.text);
            string[] details = www.text.Split('|');
            userName.transform.parent.GetComponent<InputField>().text = details[0];
            if (details.Length > 2)
            {
                score.transform.parent.GetComponent<InputField>().text = details[1];
            }
        }
    }

    IEnumerator UpdatePlayerDetails()
    {
        Debug.Log("Updating Details");

        WWWForm form = new WWWForm();
        form.AddField("email", email.text);
        form.AddField("name", userName.text);
        form.AddField("score", score.text);
        form.AddField("password", password.text);

        WWW www = new WWW("https://jrm-project-server.000webhostapp.com/updateplayer.php", form);

        yield return www;

        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.text);
        }
    }
}
