using UnityEngine;
using TMPro;

public class RescueManager : MonoBehaviour
{
    public static RescueManager instance;

    [Header("UI")]
    public TextMeshProUGUI rescueText;

    [Header("Rescue Count")]
    public int totalAnimals = 0;
    public int savedAnimals = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    public void SetTotalAnimals(int total)
    {
        totalAnimals = total;
        savedAnimals = 0;
        UpdateUI();
    }

    public void AnimalSaved()
    {
        savedAnimals++;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (rescueText != null)
        {
            rescueText.text = "Saved: " + savedAnimals + " / " + totalAnimals;
        }
    }
}