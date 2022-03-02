using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleBullet : MonoBehaviour
{

    [SerializeField] GameObject targetItem;

    [SerializeField] int endNum;

    private float k;  
    
    public int circleNum;

    public Quaternion rotation = Quaternion.identity;

    Player player; 

    void Awake()
    {
        player = Player.m_instance;
        StartCoroutine("targetSet");
        k = 0;
    }
    
    void Update()
    {
        transform.position = player.transform.position;
    }

    IEnumerator targetSet()
    {
        for (int i=0; i<endNum; i++)
        {
            rotation.eulerAngles = new Vector3(0, 0, k);
            yield return new WaitForSeconds(0.1f);
            Instantiate(targetItem, transform.position, rotation);
            k += circleNum;
        }

    }
    
}
