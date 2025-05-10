using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menük")]
    public GameObject mainMenu;
    public GameObject playMenu;
    public GameObject optionsMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        mainMenu.SetActive(true);
        playMenu.SetActive(false);
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 0;
    }

    public void ShowPlayMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        mainMenu.SetActive(false);
        playMenu.SetActive(true);
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ShowOptionsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        mainMenu.SetActive(false);
        playMenu.SetActive(false);
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ShowPauseMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        mainMenu.SetActive(false);
        playMenu.SetActive(false);
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ShowGameOverMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        SceneLoad.gameSceneAlreadyLoaded = false;
        FindObjectOfType<SceneLoad>().StartGame();
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void GoToMainMenu()
    {
        LoadScreenManager.Instance.ShowLoadScreen();
        Time.timeScale = 0f;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void LoadSavedGame()
    {
        Time.timeScale = 1f;
        LoadScreenManager.Instance.ShowLoadScreen();
        // GameManager.Instance.LoadGame();
    }
}
