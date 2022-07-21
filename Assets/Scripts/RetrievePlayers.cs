using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrievePlayers : MonoBehaviour
{
    public void GetDetails()
    {
        StartCoroutine(RetrievePlayerDetails());
    }

    IEnumerator RetrievePlayerDetails()
    {
        Debug.Log("Retrieving Details");

        WWWForm form = new WWWForm();
        WWW www = new WWW("https://jrm-project-server.000webhostapp.com/getplayers.php", form);

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
