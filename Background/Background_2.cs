using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_2 : MonoBehaviour
{
    public Transform player;
    
    Vector2 limit;
    public static Vector2 moveLimit = new Vector2(4.7f, 3.4f);
    
    
    void Start()
    {
    }

    void Update()
    {
        if (player == null) return;

            var pos = player.localPosition;
            var limit = moveLimit;

            //プレイヤーが画面のどの位置に存在するのか、0～1の値に置き換える
            var tx = 1 - Mathf.InverseLerp( -limit.x, limit.x, pos.x);
            var ty = 1 - Mathf.InverseLerp( -limit.y, limit.y, pos.y);
            
            //プレイヤーの現在地から背景の表示位置を計算
            var x = Mathf.Lerp( -limit.x, limit.x, tx);
            var y = Mathf.Lerp( -limit.y, limit.y, ty);
            
            //背景の表示位置を更新
            transform.localPosition = new Vector3(x, y, 0);
    }
    
}
