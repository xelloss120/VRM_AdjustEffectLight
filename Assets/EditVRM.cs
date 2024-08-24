using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniGLTF;

public class EditVRM : MonoBehaviour
{
    public RuntimeGltfInstance Instance;

    [SerializeField] Scrollbar ScrollbarLitgh;
    [SerializeField] Scrollbar ScrollbarColor;

    List<Material> MaterialList = new List<Material>();
    List<Color> ShadeColorList = new List<Color>();

    Color Color;
    Color ShadeColor;
    Color EmissionColor;

    public void Init()
    {
        // 全マテリアルの取得
        MaterialList.Clear();
        foreach (var mesh in Instance.Root.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            foreach (var mat in mesh.materials)
            {
                MaterialList.Add(mat);
            }
        }
        foreach (var mesh in Instance.Root.GetComponentsInChildren<MeshRenderer>())
        {
            foreach (var mat in mesh.materials)
            {
                MaterialList.Add(mat);
            }
        }

        ShadeColorList.Clear();
        foreach (var mat in MaterialList)
        {
            // 影色を変更するため、値が失われないように、開いた時点の値を覚えておく
            var color = mat.GetColor("_ShadeColor");
            ShadeColorList.Add(color);

            // メインテクスチャをエミッションマップに設定
            var texture = mat.GetTexture("_MainTex");
            mat.SetTexture("_EmissionMap", texture);

            // エミッションカラーを0x000000にする（元の値を無視するが仕様とする）
            mat.SetColor("_EmissionColor", Color.black);

            // メインカラーを0xFFFFFFにする（元の値を無視するが仕様とする）
            mat.SetColor("_Color", Color.white);

            // 色を黒と白にする設定は、あえてここで設定する必要はないが、意図的であることの明示が目的
        }
    }

    public void OnValueChangedLight()
    {
        // 光源影響の調整
        EmissionColor = Color.white * ScrollbarLitgh.value;
        Color = Color.white - EmissionColor;
        Color.a = 1;
        var count = 0;
        foreach (var mat in MaterialList)
        {
            ShadeColor = ShadeColorList[count] - EmissionColor;
            ShadeColor.a = 1;
            mat.SetColor("_Color", Color);
            mat.SetColor("_ShadeColor", ShadeColor);
            mat.SetColor("_EmissionColor", EmissionColor);
            count++;
        }
    }

    public void OnValueChangedColor()
    {
        // 光源色影響の調整
        foreach (var mat in MaterialList)
        {
            mat.SetFloat("_LightColorAttenuation", ScrollbarColor.value);
        }
    }
}
