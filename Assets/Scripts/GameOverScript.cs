using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [Header("Game Over UI Panel")]
    public GameObject gameOverUI; // Húzd be Inspectorban

    public string mainMenuSceneName = "MainMenu"; // Vagy bármelyik főmenü scene neve

    public void OnPlayerDeath()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameOver UI nincs beállítva!");
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

}
