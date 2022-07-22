using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DbConnect : MonoBehaviour
{

    public Text email;
    public Text userName;
    public Text score;

    public void AddDetails()
    {
        StartCoroutine(AddPlayerDetails());
    }

    IEnumerator AddPlayerDetails()
    {
        Debug.Log("Adding Details");

        WWWForm form = new WWWForm();
        form.AddField("email", email.text);
        form.AddField("name", userName.text);
        form.AddField("score", score.text);

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
}
