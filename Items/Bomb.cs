using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Spaceship spaceship;
    public float speed;

    private Rigidbody2D rb; 
    
    bool enemyDeleteBool;
    
    public static Bomb m_instance;



    void Start()
    {
        m_instance = this;
        spaceship = GetComponent<Spaceship>();
        rb = GetComponent<Rigidbody2D>();
    }

    
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.name.Contains("Player")) return;

        if (!enemyDeleteBool)
        {
            Destroy(gameObject);

            GameObject[] allEnemys = GameObject.FindGameObjectsWithTag("Enemy");
            List<GameObject> enemyList = new List<GameObject>();
            
            foreach(GameObject deleteEnemy in allEnemys)
            {
                    enemyList.Add(deleteEnemy);
            }
            for(int i=0; i<enemyList.Count; i++)
            {
                Destroy(enemyList[i]);
            }

            enemyDeleteBool = false;
        } else {
            Debug.Log("NoDelete");
        }
    }
}
