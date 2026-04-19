using UnityEngine;
using TMPro;

public class TrashManager : MonoBehaviour
{
    public static TrashManager instance;

    [Header("UI")]
    public TextMeshProUGUI trashText;

    [Header("Trash Count")]
    public int totalTrash = 0;
    public int collectedTrash = 0;

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
        TrashCollect[] trash = FindObjectsByType<TrashCollect>(FindObjectsSortMode.None);
        totalTrash = trash.Length;
        collectedTrash = 0;
        UpdateUI();
    }

    public void AddTrash()
    {
        collectedTrash++;
        UpdateUI();
        CheckWin();
    }

    void UpdateUI()
    {
        if (trashText != null)
        {
            trashText.text = "Trash: " + collectedTrash + " / " + totalTrash;
        }
    }

    void CheckWin()
    {
        if (RescueManager.instance == null) return;

        if (collectedTrash >= totalTrash &&
            RescueManager.instance.savedAnimals >= RescueManager.instance.totalAnimals)
        {
            RescueManager.instance.WinGame();
        }
    }
}