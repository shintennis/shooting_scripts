using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DB_connect : MonoBehaviour {


    [SerializeField] GameObject welcomePage;
    [SerializeField] GameObject LoginPage;
    [SerializeField] GameObject RegisterPage;
    [SerializeField] Text user;
    [Space]
    [SerializeField] InputField username;
    [SerializeField] InputField password;

    [SerializeField] Text errorMessages;
    [SerializeField] GameObject progressCircle;
    
    [SerializeField] Button loginbutton;
    
    [SerializeField] string url;
    
    WWWForm form;
    public void OnLoginButtonClicked()
    {
        loginbutton.interactable = false;
        progressCircle.SetActive(true);
        StartCoroutine(Login());
    }
    
    public void toLoginButton()
    {
        welcomePage.SetActive(false);
        LoginPage.SetActive(true);
        RegisterPage.SetActive(false);
    }
    
    public void toRegisterButton()
    {
        RegisterPage.SetActive(true);
        welcomePage.SetActive(false);
        LoginPage.SetActive(false);
    }
    
    public void toTitleButton()
    {
        SceneManager.LoadScene("Title");
    }


    IEnumerator Login()
    {
        form = new WWWForm();

        form.AddField ("username", username.text);
        form.AddField ("password", password.text);
        
        using(UnityWebRequest www = UnityWebRequest.Post(url, form)){
        
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                errorMessages.color = Color.red;
                errorMessages.text = "404 not found!";
                Debug.Log("<color=red>" + www.downloadHandler.text + "</color>");
            } else {
                if (www.isDone)
                {
                    if (www.downloadHandler.text.Contains ("error")) 
                    {
                        errorMessages.color = Color.red;
                        errorMessages.text = "username or password is invalid";
                        Debug.Log("<color=red>" + www.downloadHandler.text + "</color>");
                    } else {
                        welcomePage.SetActive(true);
                        LoginPage.SetActive(false);
                        user.text = username.text;
                        Debug.Log("<color=green>" + www.downloadHandler.text + "</color>");
                    }
                }
            }

            loginbutton.interactable = true;
            progressCircle.SetActive(false);
            www.Dispose();
        }
        
    }
    
}











































































































