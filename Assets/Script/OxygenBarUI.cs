using UnityEngine;
using UnityEngine.UI;

public class OxygenBarUI : MonoBehaviour
{
    public PlayerController player;
    public Image fillImage;

    void Update()
    {
        if (player != null && fillImage != null)
        {
            fillImage.fillAmount = player.currentOxygen / player.maxOxygen;
        }
    }
}