using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

//Unity 2020.3.17で動作確認済み
/// <summary>
/// MySql(MariaDB)にアクセスするための命令群
/// </summary>
public class DB_users : MonoBehaviour
{
    /// <summary>
    /// アクセスするURLを返す。クエリパラメータを含めた全てのURLで返す。
    /// </summary>
    /// <returns>クエリパラメータ等を含む総URL</returns>
    public string CreateURL(string Url, string user, string password, string sql)
    {
        string param = Url + "?user=" + user + "&password=" + password + "&sql=" + sql;
        return param;
    }
    /// <summary>
    /// MySQLからデータを呼び出すコルーチン
    /// </summary>
    /// <param name="param">クエリパラメータ等を含むURL</param>
    /// <param name="callback">コールバック関数。stringで返す</param>
    /// <returns></returns>
    public IEnumerator RequestMySQL(string param, Action<string> callback)
    {
        // サーバーにアクセスして結果を得る
        UnityWebRequest www = UnityWebRequest.Get(param);

        yield return www.SendWebRequest();

        // エラー判定
        if (www.result == UnityWebRequest.Result.ConnectionError ||
            www.result == UnityWebRequest.Result.ProtocolError)
        {
            //エラーメッセージをテキストで返す
            string e = www.result.ToString();
            callback(e);
        }
        else
        {
            // 結果をテキストとして表示します

            callback(www.downloadHandler.text);
        }
    }
    /// <summary>
    /// エラーか否か判定。エラーならtrue
    /// </summary>
    /// <param name="res">レスの文字列</param>
    /// <returns>レスにエラーの文字が含まれていればtrue</returns>
    public bool IsError(string log)
    {
        return log.ToLower().Contains("error");
    }
}