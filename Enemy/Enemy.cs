using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq; 


public enum RESPAWN_TYPE
{
    UP, 
    DOWN,
    LEFT,
    RIGHT,
    SIZEOF,
}
public class Enemy : MonoBehaviour
{

    //ヒットポイント
    public int hp = 1;

    //スコアのポイント
    public int point = 100;

    //Exp
    public int m_exp;
    
    //Damage
    public int damage;

    //Spaceshipコンポーネント
    Spaceship spaceship;
    
    Player player;
    
    public ItemsManager itemsManager;
    
    // private GameObject player;
    
    //プレイヤーが取得可能なアイテム一覧
    public PlayerItems[] m_playerItemsPrefabs;
    
    //アイテムを管理する配列(経験値)
    public Items[] m_itemsPrefabs;

    //アイテムの移動の速さ（最小値）
    public float m_itemsSpeedMin;


    //アイテムの移動の速さ（最大値）
    public float m_itemsSpeedMax;
    
    public static Enemy m_instance;
    

    IEnumerator Start()
    {
        m_instance = this; 
        
        player = Player.m_instance;        
        
        // player = GameObject.FindGameObjectWithTag("Player");

        //Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship> ();

        //ローカル座標のY軸のマイナス方向に移動する
        Move (transform.forward * spaceship.speed);

        //canShotがfalseの場合、ここでコルーチンを終了させる
        if (spaceship.canShot == false) {
            yield break;
        }
        while(true)
        {
            //子要素をすべて取得する
            for(int i = 0; i < transform.childCount; i++) {
                Transform shotPosition = transform.GetChild(i);

                //ShotPositionの位置/角度で弾を撃つ
                spaceship.Shot(shotPosition);
            }
            //shotDelay秒待つ
            yield return new WaitForSeconds (spaceship.shotDelay);
        } 
        
    }

    public void Move (Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * spaceship.speed;
    }

    void OnParticleCollision(GameObject obj)
    {
        Debug.Log(obj.gameObject.name + "への爆撃!!");
        var score = Score.m_instance;
        var dame = obj.gameObject.GetComponent<ExplosionToEnemy>().ex_damage;
        hp = hp - dame;
        if (hp <= 0)
        {
            spaceship.Explosion();
            Destroy(gameObject);

            //経験値アイテムの出現
            getExpItems();
            score.AddPoint(point);
        }
    }
    
    void OnTriggerEnter2D (Collider2D c)
    {
        //レイヤー名取得
        string layerName = LayerMask.LayerToName(c.gameObject.layer);
        
        //レイヤー名がbullet(Player)以外の時は何も行わない
        if(layerName != "Bullet(Player)") return;

        //PlayerBulletのTransformを取得
        Transform playerBulletTransform = c.transform.parent;
        Transform targetTrans = c.transform;


        string name = c.name;
        switch(name)
        {
            case "subBullet":

                //Bulletコンポ―ネントを取得
                Bullet bullet = playerBulletTransform.GetComponent<Bullet>();

                //ヒットポイントを減らす
                hp = hp - bullet.power;
                if (hp <= 0)
                {
                    spaceship.Explosion();
                    Destroy(gameObject);
                }

                break;
            case "playerHomingMissile(Clone)":
                Debug.Log("playerShot_1");
                playerHomingMissile playerHoming = targetTrans.GetComponent<playerHomingMissile>();
                hp = hp - playerHoming.damage;
                if (hp <= 0)
                {
                    spaceship.Explosion();
                    Destroy(gameObject);
                }
                break;
            case "playerHomingMissile":
                Debug.Log("playerShot_2");
                break;
            case "Missile(Clone)":
                PlayerMissile playerMissile = c.GetComponent<PlayerMissile>();
                playerMissile.missile();
                var ex_damage = FindObjectOfType<ExplosionToEnemy>().GetComponent<ExplosionToEnemy>().ex_damage;
                hp = hp - ex_damage;
                Debug.Log("爆撃!!");
                break;
        }


        //弾の削除
        Destroy(c.gameObject);

        //ヒットポイントが0以下であれば
        if(hp <= 0)
        {
            var score = Score.m_instance;
            var player = Player.m_instance;
            // var item_m = FindObjectOfType<ItemsManager>().GetComponent<ItemsManager>();
            var rand = Random.Range(0, 10);

            //爆発
            spaceship.Explosion();

            //エネミーの削除
            Destroy(gameObject);
        

            if (rand == 1)
            {
                //プレイヤー強化アイテムの生成
                getPlayerItems();
            }
            
            //経験値アイテムの生成
            getExpItems();

            //スコアコンポーネントを取得してポイントを追加
            score.AddPoint(point);
            
        } else {

            spaceship.GetAnimator().SetTrigger("Damage");

        }

        
    }
    
    //プレイヤー用アイテム出現関数
    void getPlayerItems()
    {
           //生成可能なアイテムを配列で取得
           var itemPrefabs = m_playerItemsPrefabs.ToArray();

           //生成可能なアイテムから生成するアイテムを決定する
           var itemPrefab = itemPrefabs[ Random.Range(0, itemPrefabs.Length)];

           //敵の位置にアイテムを生成する
           var item = Instantiate(itemPrefab, transform.localPosition, Quaternion.identity);
           
           item.Init(m_itemsSpeedMin, m_itemsSpeedMax);
    }

    //経験値アイテムの出現関数
    void getExpItems()
    {
       var exp = m_exp;

       while (0 < exp) 
       {
           //生成可能なアイテムを配列で取得
           var itemPrefabs = m_itemsPrefabs.Where(c => c.m_exp <= exp).ToArray();

           //生成可能なアイテムから生成するアイテムを決定する
           var itemPrefab = itemPrefabs[ Random.Range(0, itemPrefabs.Length)];

           //敵の位置にアイテムを生成する
           var item = Instantiate(itemPrefab, transform.localPosition, Quaternion.identity);

           //アイテムを初期化
           item.Init(m_exp, m_itemsSpeedMin, m_itemsSpeedMax);

           exp -= item.m_exp;
       }
    }
    
    
    public void Init(RESPAWN_TYPE respawnType) 
    {
        var pos = Vector3.zero;
        Vector2 resEnemyPosition = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 resEnemyPosition_ = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        Vector2 resEnemyPosition__ = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 resEnemyPosition___ = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
        
        switch( respawnType )
        {
            case RESPAWN_TYPE.UP:
                pos.x = Random.Range(
                    resEnemyPosition_.x, resEnemyPosition__.x);
                pos.y = resEnemyPosition_.y;
                break; 
            
            case RESPAWN_TYPE.DOWN:
                pos.x = Random.Range(
                    resEnemyPosition.x, resEnemyPosition___.x);
                pos.y = resEnemyPosition.y;
                break;
                
            case RESPAWN_TYPE.RIGHT:
                pos.x = resEnemyPosition__.x;
                pos.y = Random.Range(
                    resEnemyPosition___.y, resEnemyPosition__.y);
                break;
                
            case RESPAWN_TYPE.LEFT:
                pos.x = resEnemyPosition.x;
                pos.y = Random.Range(
                    resEnemyPosition.y, resEnemyPosition_.y);
                break;
        }
        
        transform.localPosition = pos;
    }
}


