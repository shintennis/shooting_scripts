using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    Rigidbody2D rb2d;
    
    GameObject player;
    
    Spaceship spaceship;

    [SerializeField] GameObject p_explosion;
    
    private float interval; 
    
    private float timer;
    
    private int i = 0;
    
    Vector3 playerPos;
    
    public static PlayerMissile m_instance;

    IEnumerator shot()
    {
        yield return new WaitForSeconds(Random.Range(0f, 4f));
        Destroy(gameObject);

        Instantiate(p_explosion, transform.position, Quaternion.identity);
    }
    
    public void missile()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        var playerPos = player.transform.position;
        var myPos = this.transform.position;

        if (playerPos.y == myPos.y) Destroy(gameObject);
        Destroy(gameObject);
        Instantiate(p_explosion, transform.position, Quaternion.identity);
    }

    IEnumerator del()
    {
        // yield return new WaitForSeconds(5f);
        var startTime = Time.time;
        Init();

        while(Time.time - startTime < 2)
        {
            Init_();
            yield return null;
        }

        Instantiate(p_explosion, transform.position, Quaternion.identity);
    }

    void Start()
    {
        m_instance = this;        

        spaceship = Spaceship.m_instance;        

        player = GameObject.FindGameObjectWithTag("Player");
        // var shot = player.GetComponent<Spaceship>().canShot;
        spaceship.canShot = true;
        playerPos = player.transform.position;
        rb2d = GetComponent<Rigidbody2D>();

        StartCoroutine(shot());
        
    }

    // public void missileShot()
    // {
    //     rb2d = GetComponent<Rigidbody2D>();
    //     Init();

    //     StartCoroutine(shot());
    // }

    void Update()
    {
        rb2d.velocity -= new Vector2(0, 0.009f);
    }
    
    // public void Init()
    // {
    //     var pos = Vector3.zero;
    //     Vector2 resEnemyPosition_ = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
    //     Vector2 resEnemyPosition__ = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

    //     pos.x = Random.Range(resEnemyPosition_.x, resEnemyPosition__.x);
    //     pos.y = resEnemyPosition_.y;
    //     transform.localPosition = pos;
    // }
    public void Init()
    {
        var pos = Vector3.zero;

        Vector2 resEnemyPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        playerPos = player.transform.position;
        pos.x = playerPos.x - 2f;
        pos.y = resEnemyPosition.y;

        transform.localPosition = pos; 
    }

    public void Init_()
    {
        var pos = Vector3.zero;

        playerPos = player.transform.position;

        transform.localPosition = playerPos; 
    }
}
