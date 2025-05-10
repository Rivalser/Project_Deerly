using UnityEngine;
using UnityEngine.SceneManagement;

public class PMenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;
    public string mainMenuSceneName = "MainMenu";

    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        gameOverMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != mainMenuSceneName)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                if (isPaused)
                    Resume();
                else
                    Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void GoToMainMenu()
    {
        LoadScreenManager.Instance.ShowLoadingScreen();  // Betöltési képernyő megjelenítése
        gameOverMenuUI.SetActive(false);  // Game Over UI eltűnik
        SceneManager.LoadScene(mainMenuSceneName, LoadSceneMode.Single);  // Főmenü betöltése
        Time.timeScale = 1f;  // Játék folytatása
    }

    public void RestartGame()
    {
        LoadScreenManager.Instance.ShowLoadingScreen();  // Betöltési képernyő megjelenítése
        gameOverMenuUI.SetActive(false);  // Game Over UI eltűnik

    // Scene újratöltése
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Time.timeScale = 1f;  // Játék újraindul
    }

    public void GameOver()
    {
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoToMainMenuFromGameOver()
    {
        LoadScreenManager.Instance.ShowLoadScreen();
        gameOverMenuUI.SetActive(false);
        SceneManager.LoadScene(mainMenuSceneName, LoadSceneMode.Single);
        Time.timeScale = 1f;
    }
}
