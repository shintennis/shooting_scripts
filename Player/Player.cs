using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	// Spaceshipコンポーネント
	Spaceship spaceship;

	PlayerMissile playerMissile;
	
	playerHomingMissile playerHoming;
	
	private SpriteRenderer sp;
	
	private bool isDamage;
	
	//次のレベルまでに必要な経験値の基本値
	public int nextExpBase;

	//次のレベルまでに必要な経験値の増加値
	public int nextExpIntarval;
	
	//前のレベルに必要だった経験値
	public int prevNeedExp;

	//次のレベルに必要な経験値
	public int needExp;	
	
	//level
	public int level;
	
	//MaxHP
	public int maxhp;
	
	//NowHP
	public int nowhp;
	
	
	//NowExp
	public int m_exp;
	
	//アイテムをひきつける距離
	public float m_itemDistance;

	//アイテムをひきつける距離（最大値）
	public Vector2 m_itemDistanceTo;

	//アイテムをひきつける距離（最小値）
	public Vector2 m_itemDistanceFrom;
	
	public float m_missileDistance;
	
	public Vector2 m_missileDistanceFrom;

	public Vector2 m_missileDistanceTo;
	
	//プレイヤーのインスタンスを管理する static変数
	public static Player m_instance;
	
	
	
	IEnumerator Start ()
	{
		m_instance = this;	
		
		sp = GetComponent<SpriteRenderer>(); 
		
		//ダメージ判定
		isDamage = false;
		
		m_itemDistanceFrom = transform.localPosition;
		
		m_itemDistanceTo = new Vector2(1.5f, 1.5f);
		
		m_missileDistanceFrom = new Vector2(3.0f, 3.0f);

		m_missileDistance = Vector2.Distance(m_missileDistanceFrom, m_missileDistanceTo);
		
		m_itemDistance = Vector2.Distance(m_itemDistanceFrom, m_itemDistanceTo);

		// Spaceshipコンポーネントを取得
		spaceship = Spaceship.m_instance;
		
		playerMissile = PlayerMissile.m_instance;
		
		playerHoming = playerHomingMissile.m_instance;

		var score = Score.m_instance;
		
		//shotDelayの初期値
		spaceship.shotDelay = .5f;
		
		//現在のHPを取得
		nowhp = maxhp;	
		
		//現在のレベル
		level = 1;
		
		//次のレベルに必要な経験値
		needExp = GetNeedExp(1);	
		

		while (true) {
			
			// 弾をプレイヤーと同じ位置/角度で作成
			spaceship.Shot (transform);
			
			if (spaceship.playerShot1 == true)
			{
				spaceship.playerShot_1(transform);
			}
			if (spaceship.playerShot2 == true)
			{
				spaceship.playerShot_2(transform);
			}

			// ショット音を鳴らす
			GetComponent<AudioSource>().Play();
			
			// shotDelay秒待つ
			yield return new WaitForSeconds (spaceship.shotDelay);
		}
		
	}
	
	void Update ()
	{
		// 右・左
		float x = Input.GetAxisRaw ("Horizontal");
		
		// 上・下
		float y = Input.GetAxisRaw ("Vertical");
		
		// 移動する向きを求める
		Vector2 direction = new Vector2 (x, y).normalized;

		
		// 移動の制限
		Move (direction);
		
		//ダメージ判定
		if (isDamage)
		{
			float lev = Mathf.Abs(Mathf.Sin(Time.time * 15));
			sp.color = new Color(1f, 1f, 1f, lev);
		}

	}
	
	// 機体の移動
	void Move (Vector2 direction)
	{
		// 画面左下のワールド座標をビューポートから取得
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		
		// 画面右上のワールド座標をビューポートから取得
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		
		// プレイヤーの座標を取得
		Vector2 pos = transform.position;

		//プレイヤーのスクリーン座標を計算
		var screenPos = Camera.main.WorldToScreenPoint(transform.position);
		
		//プレイヤーから見たマウスカーソルの方向を計算
		var dire = Input.mousePosition - screenPos;

		//マウスカーソルが存在する方向の角度を取得
		var angle = GetAngle(Vector3.zero, dire);
		
		//プレイヤーがマウスカーソルの方向を見る
		var angles = transform.localEulerAngles;
		angles.z = angle - 90;
		transform.localEulerAngles = angles;
		
		// 移動量を加える
		pos += direction * spaceship.speed * Time.deltaTime;
		
		// プレイヤーの位置が画面内に収まるように制限をかける
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);
		
		// 制限をかけた値をプレイヤーの位置とする
		transform.position = pos;
		
	}
	
	public static float GetAngle(Vector2 from, Vector2 to)
	{
		var dx = to.x - from.x;
		var dy = to.y - from.y;
		var rad = Mathf.Atan2(dy, dx);
		
		return rad * Mathf.Rad2Deg;
	}
    private int GetNeedExp(int level)
    {
        return nextExpBase + nextExpIntarval * ( (level - 1) * (level - 1) );
    }
	
	//Expの取得及びレベルアップ
	public void AddExp(int exp)
	{

		m_exp += exp;

		Score.m_instance.expgauge.value = m_exp;

		if (m_exp < needExp) return;

		level++;
		
		if (level % 2 == 0 && level < 10)
		{
			EnemyManager.m_instance.interval = EnemyManager.m_instance.interval - .1f;
		}
		
		Score.m_instance.LevelGUIText.text = "Level : " + level.ToString();
		
		prevNeedExp = needExp;

		needExp = GetNeedExp(level);
		
		Score.m_instance.expgauge.minValue = prevNeedExp;
	}

	public IEnumerator OnDamage()
	{
		yield return new WaitForSeconds(1.0f);

		//通常の状態に戻す
		isDamage = false;
		sp.color = new Color(1f, 1f, 1f, 1f);
		Debug.Log("damage");
	}	

	
	// ぶつかった瞬間に呼び出される
	void OnTriggerEnter2D (Collider2D c) 
	{
		// レイヤー名を取得
		
		string layerName = LayerMask.LayerToName(c.gameObject.layer);
		var score = Score.m_instance;
		
		//"Item"レイヤーにぶつかったらItemオブジェクトを削除
		if(layerName == "Items")
		{
			string name = c.name;

			switch(name)	
			{
				case "ammo(Clone)":
					Debug.Log("攻撃力UP");
					if (spaceship.shotDelay < 0.02f) spaceship.shotDelay = 0.01f;
					spaceship.shotDelay = spaceship.shotDelay - 0.01f;	
					break;
				case "Bomb(Clone)":
					Debug.Log("ザコ敵一掃");
					break;
				case "HeartGreen(Clone)":
					Debug.Log("HP全回復");
					nowhp = nowhp + 10;
					score.hpgauge.value = nowhp;					
					break;
				case "PlayerBullet_3(Clone)":
					Debug.Log("攻撃パターン1");
					spaceship.playerShot1 = true;
					break;

				case "PlayerBullet_4(Clone)":
					Debug.Log("攻撃パターン2");
					spaceship.playerShot2 = true;
					break;

				case "PlayerBullet_5(Clone)":
					Debug.Log("攻撃パターン3");
					break;

			}
			
			Destroy(c.gameObject);
		}
		
		
		// レイヤー名がBullet (Enemy)の時は弾を削除
		if( layerName == "Bullet(Enemy)" || layerName == "Enemy")
		{
			//ダメージ判定中は処理をスキップ
			if (isDamage) return;

			isDamage = true;
			StartCoroutine(OnDamage());
			
			var enemy = FindObjectOfType<Enemy>();

			nowhp = nowhp - enemy.damage; 

			score.hpgauge.value = nowhp;	

			spaceship.GetAnimator().SetTrigger("Player");
			

			// 弾の削除
			Destroy(c.gameObject);
		}

		
		// HPが0になったらPlayerを破壊 
		if(nowhp <= 0)
		{
			
			// 爆発する
			spaceship.Explosion();
			
			// プレイヤーを削除
			Destroy (gameObject);

			// Managerコンポーネントをシーン内から探して取得し、GameOverメソッドを呼び出す
			FindObjectOfType<Manager>().GameOver();
		}
	}

	
}