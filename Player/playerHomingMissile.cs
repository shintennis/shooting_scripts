using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHomingMissile : MonoBehaviour
{
    
   Spaceship spaceship; 

    private GameObject enemyTrans;
    
    float spaceship_speed;

    //スピード
    // public float speed;
    
    //経過時間
    float time = 0f; 

    //ホーミング終了
    public float endTime;
    
    public int damage;
    
    public float shotDelay = 2f;
    
    public static playerHomingMissile m_instance;
    

    private void Awake()
    {
        m_instance = this;
        
        enemyTrans = GameObject.FindGameObjectWithTag("Enemy");
        spaceship = GetComponent<Spaceship>();
        spaceship_speed = spaceship.speed;

        //経過時間の初期化
        time = 0;
        
        //コルーチン
        StartCoroutine(DelayMethod(endTime + 1f, () => {
            Destroy(gameObject);
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

         

        Vector2 v = new Vector2(0,spaceship_speed * Time.deltaTime);

        transform.Translate(v);

        //endTimeまで続ける
        if (time <= endTime && enemyTrans != null)
        {
            //ターゲットの方向を常に向く
            Vector3 diff = (enemyTrans.transform.position - this.transform.position).normalized;
            this.transform.rotation= Quaternion.FromToRotation(Vector2.up, -diff);
        }
    }


    void OnTriggerEnter2D (Collider2D c)
    {
        string layerName = LayerMask.LayerToName(c.gameObject.layer);

        if (layerName == "Enemy")
        {
            spaceship.Explosion();

            Destroy(gameObject);
        }
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
