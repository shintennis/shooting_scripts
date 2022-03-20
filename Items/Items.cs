using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    Player player; 

    // 取得できる経験値
    public int m_exp; 
    
    // 散らばる時の減速量、数値が小さいほどすぐ減速する
    public float m_brake = 1f; 

    //プレイヤーを追尾する時の加速度 
    public float m_followPlayer = 0.01f;
    
    //プレイヤーを追尾するモード（true）
    private bool m_isFollow;

    //プレイヤーを追尾する速さ
    private float m_followSpeed;
    
    // 散らばる時の進行方向
    private Vector3 m_direction; 
    
    // 散らばる時の速さ
    private float m_speed; 
    
    public static Items m_instance;
    

    void Start()
    {
        player = FindObjectOfType<Player>();

        m_instance = this;
    }

    // 毎フレーム呼び出される関数
    private void Update()
    {
        // if (!player) Destroy(gameObject);
        if (!player) return;
        //プレイヤーの現在地を取得
        var playerPos = Player.m_instance.transform.position;

        //プレイヤーとアイテムの距離を計算
        var distance = Vector3.Distance(playerPos, transform.localPosition);

        if (distance < Player.m_instance.m_itemDistance)
        {
            //追尾モードON
            m_isFollow = true;
        }
        
        //追尾モードONかつプレイヤーが破壊されていない場合
        if (m_isFollow && Player.m_instance.gameObject.activeSelf)
        {
            //プレイヤーへ向かうベクトルを作成
            var direction = playerPos - transform.localPosition;
            direction.Normalize();

            //プレイヤーの方向へ移動
            transform.localPosition += direction * m_followSpeed;

           //加速しながらプレイヤーへ近づく
           m_followSpeed += m_followPlayer; 
           return;

        }
        
		// 画面左下のワールド座標をビューポートから取得
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		
		// 画面右上のワールド座標をビューポートから取得
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        
        //オブジェクトの現在地を取得
        Vector2 pos = transform.position;

        // 散らばる速度を計算する
        var velocity = m_direction * m_speed;

        // 散らばる
        transform.localPosition += velocity;

        // だんだん減速する
        m_speed *= m_brake;

        // 宝石が画面外に出ないように位置を制限する
        // transform.localPosition = 
        //     Utils.ClampPosition( transform.localPosition );
        
        // pos.x = Mathf.Clamp(pos.x, min.x, max.x);             
        // pos.y = Mathf.Clamp(pos.y, min.y, max.y);             
        
        // transform.localPosition = pos;
    }

    // 宝石が出現する時に初期化する関数
    public void Init( int score, float speedMin, float speedMax )
    {
        // 宝石がどの方向に散らばるかランダムに決定する
        var angle = Random.Range( 0, 360 );

        // 進行方向をラジアン値に変換する
        var f = angle * Mathf.Deg2Rad;

        // 進行方向のベクトルを作成する
        m_direction = new Vector3( Mathf.Cos( f ), Mathf.Sin( f ), 0 );

        // 宝石の散らばる速さをランダムに決定する
        m_speed = Mathf.Lerp( speedMin, speedMax, Random.value );

        // 5 秒後に宝石を削除する
        Destroy( gameObject, 5 );
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //衝突したオブジェクトがプレイヤー以外だと無視する
        if (!collision.name.Contains("Player")) return;
        
        Destroy(gameObject);

        var player = collision.GetComponent<Player>();
        player.AddExp(m_exp);
        
    }
}
