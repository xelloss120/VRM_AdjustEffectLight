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
        // �O��Ƃ̍����l���擾�i������e����ʂɓK�p�j
        var diff = LastPos - Input.mousePosition;

        if (Input.GetMouseButton(1))
        {
            // ��]
            var euler = diff;
            euler.x = -diff.y * SensitivityR;
            euler.y = -diff.x * SensitivityR;
            Pivot.eulerAngles += euler;
        }

        if (Input.GetMouseButton(2))
        {
            // �ړ�
            Pivot.position += Pivot.right * -diff.x * SensitivityXY * Camera.orthographicSize;
            Pivot.position += Pivot.up * diff.y * SensitivityXY * Camera.orthographicSize;
        }

        // �g�k
        Camera.orthographicSize += Input.mouseScrollDelta.y * -SensitivityZ;

        LastPos = Input.mousePosition;
    }
}
