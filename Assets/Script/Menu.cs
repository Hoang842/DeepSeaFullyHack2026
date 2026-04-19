using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Play button
    public void PlayGame()
    {
        Time.timeScale = 1f;

        // đổi tên scene của bạn ở đây
        SceneManager.LoadScene("GamePlay");
    }

    // Quit button
    public void QuitGame()
    {
        Debug.Log("Quit Game");

        Application.Quit();
    }

    public void About()
    {
        Time.timeScale = 1f;

        // đổi tên scene của bạn ở đây
        SceneManager.LoadScene("About");
    }

    public void Tutorial()
    {
        Time.timeScale = 1f;

        // đổi tên scene của bạn ở đây
        SceneManager.LoadScene("Tutorial");
    }
}