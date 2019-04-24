using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// コントラーラーからRayを出す。
/// ref http://rikoubou.hatenablog.com/entry/2018/06/04/193607
/// LaserPointerにアタッチされている。
/// </summary>
public class LaserPointer : MonoBehaviour
{

    [SerializeField]
    private Transform _RightHandAnchor;

    [SerializeField]
    private Transform _LeftHandAnchor;

    [SerializeField]
    private Transform _CenterEyeAnchor; // 目の中心

    [SerializeField]
    private float _MaxDistance = 100.0f; // レーザーの最大距離

    [SerializeField]
    private LineRenderer _LaserPointerRenderer;

    // コントローラー
    public Transform Pointer
    {
        get
        {
            // 現在アクティブなコントローラーを取得
            var controller = OVRInput.GetActiveController();
            if (controller == OVRInput.Controller.RTrackedRemote)
            {
                return _RightHandAnchor;
            }
            else if (controller == OVRInput.Controller.LTrackedRemote)
            {
                return _LeftHandAnchor;
            }
            // 左右のどちらからも取れなければ目の間からビームが出る
            return _CenterEyeAnchor;
        }
    }

    void Update()
    {
        var pointer = Pointer; // コントローラーを取得

        // コントローラーがない or LineRendererがなければ何もしない
        if (pointer == null || _LaserPointerRenderer == null)
        {
            return;
        }

        // コントローラー位置からRayを飛ばす
        Ray pointerRay = new Ray(pointer.position, pointer.forward);

        // 起点を設定
        _LaserPointerRenderer.SetPosition(0, pointerRay.origin);

        // Rayの長さの調整
        RaycastHit hitInfo;
        if (Physics.Raycast(pointerRay, out hitInfo, _MaxDistance))
        {
            // Rayが起点から_MaxDistanceの間でColliderと交わるとき、それ以上伸ばさない
            _LaserPointerRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            // Rayと交わるColliderがなければ、_MaxDistanceまで伸ばす。
            _LaserPointerRenderer.SetPosition(1, pointerRay.origin + pointerRay.direction * _MaxDistance);
        }
    }
}
