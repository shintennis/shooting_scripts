using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class DB_Register : MonoBehaviour
{
    [SerializeField] GameObject LoginPage;
    [SerializeField] GameObject WelcomePage;
    [SerializeField] GameObject RegisterPage;
    [SerializeField] InputField username;
    [SerializeField] InputField password;
    
    [SerializeField] InputField checkPassword;
    [SerializeField] Button registerButton;
    
    [SerializeField] Text errorMessages;
    [SerializeField] string url;
    

    public void OnRegisterButtonClicked()
    {
        StartCoroutine(Register());
    }
    

    IEnumerator Register()
    {
        WWWForm  form = new WWWForm();

        form.AddField("username", username.text);
        form.AddField("password", password.text);
        form.AddField("checkPassword", checkPassword.text);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form)){
            yield return www.SendWebRequest();

            if(www.result != UnityWebRequest.Result.Success)
            {
                errorMessages.color = Color.red;
                errorMessages.text = www.error;
                Debug.Log("<color=red>" + www.downloadHandler.text + "</color>");
            } else {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    LoginPage.SetActive(true);
                    RegisterPage.SetActive(false);                    
                    Debug.Log("<color=green>" + www.downloadHandler.text + "</color>");
                } else {
                    errorMessages.color = Color.red;
                    errorMessages.text = responseText;
                    Debug.Log("<color=red>" + www.downloadHandler.text + "</color>");
                }
            }
        }
    }
}
