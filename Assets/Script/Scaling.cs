using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スケールを変更する。
/// オブジェクトの座標と大きさを変更することで、相対的に拡大および縮小する。
/// OVRCameraRigにアタッチされている。
/// </summary>
public class Scaling : MonoBehaviour {

    // Update is called once per frame
    void Update () {
        // タッチされた座標の取得
        Vector2 touchPadPt = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

        // タッチパッドを押している時は拡大or縮小
        if (touchPadPt.y > 0.5 && -0.5 < touchPadPt.x && touchPadPt.x < 0.5) // 上方向を押している時は拡大
        {
            enlarge();
        }
        else if (touchPadPt.y < -0.5 && -0.5 < touchPadPt.x && touchPadPt.x < 0.5) // 下方向を押している時は縮小
        {
            reduce();
        }

    }

    private void enlarge()
    {
        Vector3 scale = this.transform.localScale; // プロットのスケール
        //Vector3 centerCoordinates = this.transform.position; // カメラの座標を中心座標とする
        //float x_new, y_new, z_new;
        float enlarge_speed = 1.1f; // スケールをa倍する

        //// x, y, zの計算
        //float x_scale = scale.x;
        //float y_scale = scale.y;
        //float z_scale = scale.z;

        //GameObject camera = OVRCameraRig;
        //Transform camera = OVRCameraRig.transform;
        // カメラの位置を取得
        //Transform transform = GetComponent<Transform>(OVRCameraRig);
        Vector3 new_scale = scale * enlarge_speed * Time.deltaTime;
        this.transform.localScale = new_scale;
    }

    private void reduce()
    {
        Vector3 scale = this.transform.localScale; // プロットのスケール
        float reduce_speed = 0.9f;
        Vector3 new_scale = scale * reduce_speed * Time.deltaTime;
        this.transform.localScale = new_scale;
    }

}
