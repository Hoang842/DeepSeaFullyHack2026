using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject loseUI;

    // Hiện màn hình thua
    public void ShowLoseUI()
    {
        if (loseUI != null)
        {
            loseUI.SetActive(true);
        }
    }

    // Nút Play Again
    public void PlayAgain()
    {
        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    // Nút về Menu
    public void BackToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu"); // nhớ đặt đúng tên scene menu
    }
}