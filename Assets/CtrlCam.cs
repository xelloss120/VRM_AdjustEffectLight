using UnityEngine;

public class CtrlCam : MonoBehaviour
{
    [SerializeField] float SensitivityXY = 0.004f;
    [SerializeField] float SensitivityZ = 0.03f;
    [SerializeField] float SensitivityR = 1;

    Camera Camera;
    Transform Pivot;

    Vector3 LastPos;

    void Start()
    {
        Camera = gameObject.GetComponentInChildren<Camera>();
        Pivot = gameObject.transform;
    }

    void Update()
    {
        // 前回との差分値を取得（これを各操作量に適用）
        var diff = LastPos - Input.mousePosition;

        if (Input.GetMouseButton(1))
        {
            // 回転
            var euler = diff;
            euler.x = -diff.y * SensitivityR;
            euler.y = -diff.x * SensitivityR;
            Pivot.eulerAngles += euler;
        }

        if (Input.GetMouseButton(2))
        {
            // 移動
            Pivot.position += Pivot.right * -diff.x * SensitivityXY * Camera.orthographicSize;
            Pivot.position += Pivot.up * diff.y * SensitivityXY * Camera.orthographicSize;
        }

        // 拡縮
        Camera.orthographicSize += Input.mouseScrollDelta.y * -SensitivityZ;

        LastPos = Input.mousePosition;
    }
}
