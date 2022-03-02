using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomingBullet : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 bulletPos;
    private Transform target;
    private float period = 3.0f;

    void Start(){
        target = GameObject.FindGameObjectWithTag("Player").transform;
        bulletPos = this.transform.position;
    }

    void Update(){
        var acceleration = Vector3.zero;

        var diff = target.position - bulletPos;
        acceleration += (diff - velocity * period)*2f
                    /(period * period);
        period -= Time.deltaTime;
        if(period < 0f){
            return;
        }
        if(acceleration.magnitude >10f){
            acceleration = acceleration.normalized * 10f;
        }

        velocity += acceleration * Time.deltaTime;
        bulletPos += velocity * Time.deltaTime;
        this.transform.position = bulletPos;
        
    }
}



























































