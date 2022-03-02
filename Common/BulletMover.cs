using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    
    private GameObject playerTrans;
    
    float spaceship_speed;

    //スピード
    // public float speed;
    
    //経過時間
    float time = 0f; 

    //ホーミング終了
    public float endTime;
    

    private void Awake()
    {
        
        playerTrans = GameObject.FindGameObjectWithTag("Player");
        spaceship_speed = GetComponent<Spaceship>().speed;

        //経過時間の初期化
        time = 0;
        
        //コルーチン
        StartCoroutine(DelayMethod(endTime + 1f, () => {
            objDelete();
        }));
    }
    
    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    private void Update()
    {
        time += Time.deltaTime;

        Vector2 v = new Vector3(0,spaceship_speed * Time.deltaTime);
        transform.Translate(v);

        //endTimeまで続ける
        if (time <= endTime && playerTrans != null)
        {
            //ターゲットの方向を常に向く
            Vector3 diff = (playerTrans.transform.position - this.transform.position).normalized;
            this.transform.rotation= Quaternion.FromToRotation(Vector2.up, -diff);
        }
    }


    //オブジェクトの削除
    public void objDelete()
    {
        // Explosion();
        Destroy(gameObject);
    }
    
    // public static float AngleWithSign(Vector2 from, Vector2 to)  
    // {
    //     float angle = Vector2.Angle(from, to);
    //     float cross = Cross2D(from, to);
        
    //     return (cross != 0) ? angle * Mathf.Sign(cross) : angle;

    // }
    
    // public static float Cross2D(Vector2 a, Vector2 b)
    // {
    //     return a.x * b.y - a.y * b.x;
    // }
}
