﻿// コントローラのトリガーを押しているときにカメラを移動する。
// 移動方向はコントローラの向き(レーザーのベクトル方向)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMover : MonoBehaviour {

    public GameObject LaserPointer;
    public float moveSpeed = 0.5f;

    void Start () {
    }
	
	void Update () {
        // トリガーを押している間はコントローラーの向きに移動
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || Input.GetKey(KeyCode.A)){ // トリガー押されたら
            moveFoward();
        }
    }

    private void moveFoward()
    {
        Vector3 movedirection = LaserPointer.transform.forward;
        transform.Translate(movedirection * moveSpeed * Time.deltaTime);
    }

}
