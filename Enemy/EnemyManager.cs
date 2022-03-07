using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Enemy[] enemyPrefabs;
    public float interval;
    private float timer;

    public static EnemyManager m_instance;    

    void Start()
    {
        m_instance = this;

        interval = .5f;
    }

    void Update()
    {
        if (Player.m_instance.gameObject.activeSelf == false) return;
        
        timer += Time.deltaTime;
        
        if (timer < interval) return;

        timer = 0;

        var enemyIndex = Random.Range(0, enemyPrefabs.Length);

        var enemyPrefab = enemyPrefabs[enemyIndex];

        var enemy = Instantiate(enemyPrefab);

        var respawnType = (RESPAWN_TYPE)Random.Range(
            0, (int)RESPAWN_TYPE.SIZEOF);

        transform.position = Player.m_instance.transform.position;

        enemy.Init(respawnType);
    }
    
    
}
