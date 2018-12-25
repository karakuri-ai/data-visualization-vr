using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMover : MonoBehaviour {

    GameObject LaserPointer;
    LaserPointer script;
    public float moveSpeed = 0.1f;

    void Start () {
        LaserPointer = GameObject.Find("LaserPointer"); // LaserPointerをオブジェクトの名前から取得して変数に格納する
        script = LaserPointer.GetComponent<LaserPointer>();
    }
	
	void Update () {
        // トリガーを押している間はコントローラーの向きに移動
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)){ // トリガー押されたら
            moveFoward();
        }
    }

    private void moveFoward()
    {
        Vector3 direction = new Vector3(LaserPointer.GetComponent<Transform>().forward.x, LaserPointer.GetComponent<Transform>().forward.y, LaserPointer.GetComponent<Transform>().forward.z).normalized * moveSpeed;
        LaserPointer.GetComponent<Transform>().position += direction;
    }
}
