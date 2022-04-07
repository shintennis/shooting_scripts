using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleCameraController : MonoBehaviour
{
    private GameObject player;

    private Vector3 startPlayerOffset;

    private Vector3 startCameraPos;

    private static readonly float RATE = 0.12f;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPlayerOffset = player.transform.position;
        startCameraPos = this.transform.position;
    }

    void Update()
    {
        Vector3 v = (player.transform.position - startPlayerOffset) * RATE;
        this.transform.position = startCameraPos + v;
        
        // transform.position = new Vector3(startCameraPos.x, pos.y, startCameraPos.z);
    }
}
