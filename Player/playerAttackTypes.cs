using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttackTypes : MonoBehaviour
{
    Player player;


    //プレイヤーの攻撃パターン1
    public GameObject playShotType_1; 

    //プレイヤーの攻撃パターン2
    public GameObject playShotType_2; 

    //プレイヤーの攻撃パターン3
    public GameObject playShotType_3; 

    public static playerAttackTypes m_instance; 



    void Start()
    {
        m_instance = this;
        player = Player.m_instance;    
    }
    
    void Update()
    {

    }

    public void playerShot_1(Transform origin)
    {
        Instantiate(playShotType_1, origin.position, origin.rotation);
    }

    public void playerShot_2(Transform origin)
    {
        Instantiate(playShotType_2, origin.position, origin.rotation);
    }

    public void playerShot_3(Transform origin)
    {
        Instantiate(playShotType_3, origin.position, origin.rotation);
    }
}
