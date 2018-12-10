using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMover : MonoBehaviour {

    Rigidbody m_Rigidbody;
    GameObject LaserPointer;
    LaserPointer script;

    void Start () {
        LaserPointer = GameObject.Find("LaserPointer"); //LaserPointerをオブジェクトの名前から取得して変数に格納する
        script = LaserPointer.GetComponent<LaserPointer>();
    }
	
	// Update is called once per frame
	void Update () {
    
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)){ // トリガー押されたら
            transform.position += pointerRay.position;
        }
    }
}
