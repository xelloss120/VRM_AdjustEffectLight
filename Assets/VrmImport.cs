using System.Threading.Tasks;
using UnityEngine;
using SFB;
using VRM;

public class VrmImport : MonoBehaviour
{
    [SerializeField] VrmEdit VrmEdit;

    public void OnClick()
    {
        Async();
    }

    async void Async()
    {
        await Import();
    }

    async Task Import()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "vrm", false);
        var instance = await VrmUtility.LoadAsync(paths[0]);
        instance.EnableUpdateWhenOffscreen();
        instance.ShowMeshes();
        VrmEdit.Instance = instance;
    }
}
