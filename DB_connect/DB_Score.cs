using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DB_Score : MonoBehaviour
{
    
    [SerializeField] Button scoreButton;
    
    [SerializeField] Text scoreText;

    public string url = "http://localhost:8080/Unity/userScores.php";
    
    
    public void OnUsersScoreButton()
    {
        StartCoroutine(Scores());
    } 

    IEnumerator Scores()
    {
        WWWForm form = new WWWForm();
        Score score = new Score();
        DB_user sendData = new DB_user(); 
        
        sendData.id = 1;
        sendData.user_id = 11;
        sendData.score = 3000;

        form.AddField("user_id", sendData.user_id);
        form.AddField("score", sendData.score);
        using (UnityWebRequest request = UnityWebRequest.Post(url,  form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ProtocolError || request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("error:" + request.result);
                yield break;
            }
            // Debug.Log(request.downloadHandler.text);
            scoreText.text = request.downloadHandler.text;
            Debug.Log("<color=yellow>" + "connect" + "</color>");
        }
    }
}
