using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Player player; 

    // スコアを表示するGUIText
    public Text scoreGUIText;

    // ハイスコアを表示するGUIText
    public Text highScoreGUIText;
    
    //レベルを表示するGUIText
    public Text LevelGUIText;

    // スコア
    private int score;

    // ハイスコア
    private int highScore;

    //PlayerHP
    public Text playerHPText;    
    
    //hp
    // private int hp;

    //exp
    private int m_exp; 
    
    //PlayerHPgauge
    public Slider hpgauge;

    //PlayerEXPgauge
    public Slider expgauge;
    
    
    // PlayerPrefsで保存するためのキー
    private string highScoreKey = "highScore";
    

    public static Score m_instance;





    void Start ()
    {
        m_instance = this; 

        player = FindObjectOfType<Player>();

        hpgauge.maxValue = player.maxhp;
        
        hpgauge.value = player.maxhp;

        Initialize ();
    }

    void Update ()
    {
        //プレイヤーを取得
        var player = Player.m_instance;
        
        //プレイヤーの経験値の表示を更新
        var exp = player.m_exp;
        var prevNeedExp = player.prevNeedExp;
        var needExp = player.needExp;
        
        expgauge.maxValue = needExp;
        

        // スコアがハイスコアより大きければ
        if (highScore < score) {
            highScore = score;
        }

        // スコア・ハイスコアを表示する
        scoreGUIText.text = score.ToString ();
        highScoreGUIText.text = "HighScore : " + highScore.ToString ();

    }

    // ゲーム開始前の状態に戻す
    private void Initialize ()
    {
        // スコアを0に戻す
        score = 0;
        
        // ハイスコアを取得する。保存されてなければ0を取得する。
        highScore = PlayerPrefs.GetInt (highScoreKey, 0);
    }

    // ポイントの追加
    public void AddPoint (int point)
    {
        score = score + point;
    }

    // ハイスコアの保存
    public void Save ()
    {
        // ハイスコアを保存する
        PlayerPrefs.SetInt (highScoreKey, highScore);
        PlayerPrefs.Save ();

        // ゲーム開始前の状態に戻す
        Initialize ();
    }
        
    
}
