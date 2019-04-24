using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コントローラーのトリガーを押している時にカメラを移動させる
/// 移動方向はコントローラーの向き(Rayの方向)
/// OVRCameraRigにアタッチされている。
/// </summary>
public class ControllerMover : MonoBehaviour {

    public GameObject LaserPointer;
    public float moveSpeed = 0.5f; // 移動速度
	
	void Update () {
        // トリガーを押している間はコントローラーの向きに移動
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKey(KeyCode.A)){ // トリガー押されたら
            moveFoward();
        }
    }

    /// <summary>
    /// Rayの方向に一定のスピードでカメラを移動させる。
    /// </summary>
    private void moveFoward()
    {
        Vector3 movedirection = LaserPointer.transform.forward;
        transform.Translate(movedirection * moveSpeed * Time.deltaTime);
    }

}
