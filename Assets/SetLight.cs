using UnityEngine;
using UnityEngine.UI;

public class SetLight : MonoBehaviour
{
    [SerializeField] Light Light;
    [SerializeField] Scrollbar ScrollbarIntensity;
    [SerializeField] Scrollbar ScrollbarR;
    [SerializeField] Scrollbar ScrollbarG;
    [SerializeField] Scrollbar ScrollbarB;

    Color Color;

    public void OnValueChangedIntensity()
    {
        Light.intensity = ScrollbarIntensity.value;
    }

    public void OnValueChangedR()
    {
        Color = Light.color;
        Color.r = ScrollbarR.value;
        Light.color = Color;
    }

    public void OnValueChangedG()
    {
        Color = Light.color;
        Color.g = ScrollbarG.value;
        Light.color = Color;
    }

    public void OnValueChangedB()
    {
        Color = Light.color;
        Color.b = ScrollbarB.value;
        Light.color = Color;
    }
}
