using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionToEnemy : MonoBehaviour
{
    public int ex_damage; 

    public static ExplosionToEnemy m_instance; 
    

    void Start()
    {
        m_instance = this;
    }

    void Update()
    {
        
    }
    
    void OnParticleCollision(GameObject o)
    {
        // o.gameObject.GetComponent<SpriteRenderer>().material.color = Random.ColorHSV();
        // var toEnemyDamege = o.gameObject.GetComponent<Enemy>().hp;
    }
}
