using System.Threading.Tasks;
using UnityEngine;
using SFB;
using VRM;

public class ImportVRM : MonoBehaviour
{
    [SerializeField] EditVRM EditVRM;

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
        EditVRM.Instance = instance;
        EditVRM.Init();
    }
}
