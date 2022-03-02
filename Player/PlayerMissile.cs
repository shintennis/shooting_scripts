using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    Rigidbody2D rb2d;

    [SerializeField] GameObject p_explosion;
    
    

    IEnumerator shot()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);

        Instantiate(p_explosion, transform.position, Quaternion.identity);
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Init(); 

        StartCoroutine(shot());
    }

    void Update()
    {
        rb2d.velocity -= new Vector2(0, 0.005f);

    }
    
    public void Init()
    {
        var pos = Vector3.zero;
        Vector2 resEnemyPosition_ = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        Vector2 resEnemyPosition__ = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        pos.x = Random.Range(resEnemyPosition_.x, resEnemyPosition__.x);
        pos.y = resEnemyPosition_.y;
        transform.localPosition = pos;
    }
}
