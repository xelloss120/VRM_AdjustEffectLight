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
        // �S�}�e���A���̎擾
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
            // �e�F��ύX���邽�߁A�l�������Ȃ��悤�ɁA�J�������_�̒l���o���Ă���
            var color = mat.GetColor("_ShadeColor");
            ShadeColorList.Add(color);

            // ���C���e�N�X�`�����G�~�b�V�����}�b�v�ɐݒ�
            var texture = mat.GetTexture("_MainTex");
            mat.SetTexture("_EmissionMap", texture);

            // �G�~�b�V�����J���[��0x000000�ɂ���i���̒l�𖳎����邪�d�l�Ƃ���j
            mat.SetColor("_EmissionColor", Color.black);

            // ���C���J���[��0xFFFFFF�ɂ���i���̒l�𖳎����邪�d�l�Ƃ���j
            mat.SetColor("_Color", Color.white);

            // �F�����Ɣ��ɂ���ݒ�́A�����Ă����Őݒ肷��K�v�͂Ȃ����A�Ӑ}�I�ł��邱�Ƃ̖������ړI
        }
    }

    public void OnValueChangedLight()
    {
        // �����e���̒���
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
        // �����F�e���̒���
        foreach (var mat in MaterialList)
        {
            mat.SetFloat("_LightColorAttenuation", ScrollbarColor.value);
        }
    }
}
