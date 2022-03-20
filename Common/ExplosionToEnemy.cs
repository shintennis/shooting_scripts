using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionToEnemy : MonoBehaviour
{
    public int ex_damage; 
    
    

    void Start()
    {
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
