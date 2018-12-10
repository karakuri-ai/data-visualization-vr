using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Transformの座標を直接操作して移動するクラス
public class TransformMover : MonoBehaviour {

    //VR上で目の中心(見ている方向)を確認する用のアンカー
    [SerializeField]
    private Transform _centerEyeAnchor = null;

    //移動速度の係数
    [SerializeField]
    private float _moveSpeed = 2;

    //=================================================================================
    //初期化
    //=================================================================================

    //コンポーネントがAddされた時に実行される
    private void Reset()
    {
        //中心のアンカー取得
        _centerEyeAnchor = transform.Find("TrackingSpace/CenterEyeAnchor");
    }

    //=================================================================================
    //更新
    //=================================================================================

    private void Update()
    {
        //タッチパッドを触っている所の座標(-1 ~ 1)取得
        Vector2 primaryTouchpad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

        //Yの+の方向のMaxが0.5ぐらい(デバイスの不具合かも？)なので増やす
        if (primaryTouchpad.y > 0)
        {
            primaryTouchpad.y *= 2;
        }

        //向いてる方向、タッチパッドを触ってる場所から移動距離計算
        Vector3 distance = _centerEyeAnchor.rotation * new Vector3(primaryTouchpad.x, 0, primaryTouchpad.y);

        //上向いてる時に上にいっちゃうので上下方向の速度0に
        distance.y = 0;

        //上下方向の速度を減らした分を左右に振るために正規化
        float speedMagnitude = _moveSpeed * primaryTouchpad.magnitude;//速度の大きさ
        distance = distance.normalized * speedMagnitude;

        //座標を直接操作して移動
        transform.position += distance * _moveSpeed * Time.deltaTime;
    }
}
