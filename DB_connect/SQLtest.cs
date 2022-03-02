using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SQLtest : MonoBehaviour
{
    //サーバーアクセスのためのログイン情報
    [SerializeField]  string Url = "http://localhost:8080/Unity/unity_connect.php";
    [SerializeField]  string user = "root";
    [SerializeField]  string password = "root";
    // [SerializeField]  private string database; 
    [SerializeField] private string sql;
    
    public Text ResultArea;
    /// <summary>
    /// コルーチン結果待ちでyield returnをするために、呼び出し元もIEnumeratorにする必要がある
    /// </summary>
    /// <returns></returns>
    IEnumerator Start()
    {
        DB_users dB_Users = GetComponent<DB_users>();

        // コルーチン結果待ちをする
        string param = dB_Users.CreateURL(Url, user, password, sql);
        string res = null;
        yield return StartCoroutine(dB_Users.RequestMySQL(param, r => res = r));

        // 結果を処理。このサンプルではログ表示のみ
        if (dB_Users.IsError(res))
        {

            Debug.LogError(res);
        } else {
            Debug.Log(res);

            // ResultArea.GetComponent<Text>().text = res.ToString(); 
        }
    }
    
    
}