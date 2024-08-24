using UnityEngine;

public class License : MonoBehaviour
{
    [SerializeField] GameObject GameObject;
    public void OnClick()
    {
        GameObject.SetActive(!GameObject.activeInHierarchy);
    }
}
