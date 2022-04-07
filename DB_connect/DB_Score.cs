using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DB_Score : MonoBehaviour
{
    WWWForm wwwform;
    
    [SerializeField] Button scoreButton;
    
    public string url = "http://localhost:8080/Unity/userScores.php";
    
    
    public void OnUsersScoreButton()
    {
        StartCoroutine(Scores());
    } 

    IEnumerator Scores()
    {
        wwwform = new WWWForm();
        Score score = new Score();
        DB_user sendData = new DB_user(); 
        
        sendData.id = 1;
        sendData.user_id = 11;
        sendData.score = 3000;

        wwwform.AddField("user", JsonUtility.ToJson(sendData));
        using (UnityWebRequest www = UnityWebRequest.Post(url,  wwwform))
        {
            yield return www.SendWebRequest();

            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log("error:" + www.error);
                yield break;
            }
            Debug.Log("text:" + www.downloadHandler.text);
            Debug.Log("<color=yellow>" + "connect" + "</color>");
            DB_user user = JsonUtility.FromJson<DB_user>(www.downloadHandler.text);
            Debug.Log("id:"+user.id + ", user_id:"+user.user_id + ", score:"+user.score);
        }
    }
}
