using System.IO;
using UnityEngine;
using SFB;
using VRM;
using UniGLTF;

public class VrmExport : MonoBehaviour
{
    [SerializeField] VrmEdit VrmEdit;

    public void OnClick()
    {
        Export();
    }

    void Export()
    {
        var path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "", "vrm");
        var vrm = VRMExporter.Export(new GltfExportSettings(), VrmEdit.Instance.Root, new RuntimeTextureSerializer());
        var bytes = vrm.ToGlbBytes();
        File.WriteAllBytes(path, bytes);
    }
}
