using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float m_speed = 0.1f;

    float y; 

    Vector2 offset;
    
    GameObject player;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void LateUpdate()
    {
        var p_speed = player.GetComponent<Spaceship>();
        
        //時間によってY軸の値が0から1に変化していく。1になったら0に戻り、繰り返す。
        
        y = Mathf.Repeat (Time.time * p_speed.speed * m_speed, 1);

        //Y軸の値がずれていくオフセットを作成
        offset = new Vector2 (0, -y);

        //マテリアルにオフセットを設定する
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);
    }
}
