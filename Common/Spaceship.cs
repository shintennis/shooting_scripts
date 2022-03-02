using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Spaceship : MonoBehaviour
{
	// 移動スピード
	public float speed;
	
	// 弾を撃つ間隔
	public float shotDelay;
	
	// 弾のPrefab
	public GameObject bullet;
	
	public GameObject playerBullet_1;

	public GameObject playerBullet_2;

	public GameObject playerBullet_3;
	
	// 弾を撃つかどうか
	public bool canShot;

	// 爆発のPrefab
	public GameObject explosion;

	// アニメーターコンポーネント
	private Animator animator;
	
	public static Spaceship m_instance;


	void Start ()
	{
		m_instance = this;	
		
		// アニメーターコンポーネントを取得
		animator = GetComponent<Animator> ();
	}

	// 爆発の作成
	public void Explosion ()
	{
		Instantiate (explosion, transform.position, transform.rotation);
	}
	
	// 弾の作成
	public void Shot (Transform origin)
	{
		Instantiate (bullet, origin.position, origin.rotation);
	}
	
	public void playerShot_1(Transform origin)
	{
		Instantiate (playerBullet_1, origin.position, origin.rotation);
	}

	public void playerShot_2(Transform origin)
	{
	Instantiate (playerBullet_1, origin.position, origin.rotation);
	}

	public void playerShot_3(Transform origin)
	{
		Instantiate (playerBullet_1, origin.position, origin.rotation);
	}

	// アニメーターコンポーネントの取得
	public Animator GetAnimator()
	{
		return animator;
	}
	
	
}