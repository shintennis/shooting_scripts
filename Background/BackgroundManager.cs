using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    // 背景の枚数
    int spriteRLCount = 3;
    
    // 背景が回り込み
    // float rightOffset = 2.3f;
    float rightOffset = 1.5f;
    float leftOffset = -1.5f;
 
    Transform bgTfm;
    SpriteRenderer mySpriteRndr;
    float width;
    
    
 
    void Start () {
        bgTfm = transform;
        mySpriteRndr = GetComponent<SpriteRenderer>();
        // width = mySpriteRndr.bounds.size.x - 0.96f;
        width = 15.99f;
    }
 
 
    void Update () {
        // 座標変換
        Vector3 myViewport = Camera.main.WorldToViewportPoint(bgTfm.position);
        
        // 背景の回り込み(カメラがX軸プラス方向に移動時)
        if (myViewport.x < leftOffset) {
            bgTfm.position += Vector3.right * (width * spriteRLCount);
        }
        // 背景の回り込み(カメラがX軸マイナス方向に移動時)
        if (myViewport.x > rightOffset) {
            bgTfm.position -= Vector3.right * (width * spriteRLCount);
        }         

        // 背景の回り込み(カメラがY軸プラス方向に移動時)
        // if (myViewport.y < UpOffset) {
        //     bgTfm.position += Vector3.up * (Heigth * spriteUDCount);
        // }
        // 背景の回り込み(カメラがY軸マイナス方向に移動時)
        // else if (myViewport.y > DownOffset) {
        //     bgTfm.position -= Vector3.up * (Heigth * spriteUDCount);
        // }
    }
}
