using UnityEngine;
using UnityEngine.UI;

public class AudioSliderBinder : MonoBehaviour
{
    public enum SliderType { BGM, SFX }
    public SliderType type;

    void Start()
    {
        if (AudioManager.instance == null) return;

        Slider slider = GetComponent<Slider>();

        if (type == SliderType.BGM)
        {
            AudioManager.instance.RegisterBgmSlider(slider);
        }
        else
        {
            AudioManager.instance.RegisterSfxSlider(slider);
        }
    }
}