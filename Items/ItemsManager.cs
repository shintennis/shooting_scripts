using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; 

public class ItemsManager : MonoBehaviour
{
    PlayerItems playerItem;
    Player player;
    
    Enemy enemy;
    
    Score score;
    
    public PlayerItems[] itemPrefabs;
    
    public static ItemsManager m_instance;

    

    void Start()
    {
        m_instance = this;
        player = Player.m_instance;
        enemy = Enemy.m_instance; 
        score = Score.m_instance;
    }

    public PlayerItems resItems()
    {
        var items = itemPrefabs.ToArray();

        var itemNum = Random.Range(0, 10);

        if (itemNum < 2)
        {
            playerItem = items[0];

        } else if (itemNum < 4)
        {
            playerItem = items[1];
        } else if (itemNum < 6)
        {
            playerItem = items[2];
        } else if (itemNum < 8)
        {
            playerItem = items[3];
        } else
        {
            playerItem = items[4];
        } 

        Debug.Log(itemNum);
        return playerItem;
    }

}
