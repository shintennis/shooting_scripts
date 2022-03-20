using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; 

public class ItemsManager : MonoBehaviour
{
    private GameObject playerItem;
    
    public GameObject[] playerItems;
    
    public static ItemsManager m_instance;

    

    void Start()
    {
        m_instance = this;
    }

    public GameObject resItems()
    {
        var items = playerItems.ToArray();

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

        Debug.Log(playerItem.name);

        // return Instantiate(playerItem, transform.localPosition, Quaternion.identity);
        return playerItem;
    }

}
