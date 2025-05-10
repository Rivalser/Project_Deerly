using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenuScene"; // Állítsd be a főmenü scene nevét

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Visszaállítjuk az időt
        SceneManager.LoadScene(mainMenuSceneName);
    }

}
