using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RescueManager : MonoBehaviour
{
    public static RescueManager instance;

    [Header("UI")]
    public TextMeshProUGUI rescueText;

    [Header("Rescue Count")]
    public int totalAnimals = 0;
    public int savedAnimals = 0;

    private bool hasWon = false;

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
        AnimalRescue[] animals = FindObjectsByType<AnimalRescue>(FindObjectsSortMode.None);
        TrappedAnimal[] trappedAnimals = FindObjectsByType<TrappedAnimal>(FindObjectsSortMode.None);

        totalAnimals = animals.Length + trappedAnimals.Length;
        savedAnimals = 0;

        UpdateUI();
    }

    public void AnimalSaved()
    {
        savedAnimals++;
        UpdateUI();
        CheckWin();
    }

    void UpdateUI()
    {
        if (rescueText != null)
        {
            rescueText.text = "Saved: " + savedAnimals + " / " + totalAnimals;
        }
    }

    void CheckWin()
    {
        if (hasWon) return;
        if (TrashManager.instance == null) return;

        if (savedAnimals >= totalAnimals &&
            TrashManager.instance.collectedTrash >= TrashManager.instance.totalTrash)
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        if (hasWon) return;

        hasWon = true;

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayWinningSound();
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("WinScene");
    }
}