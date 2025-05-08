using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoad : MonoBehaviour
{
    public static bool gameSceneAlreadyLoaded = false;

    [Tooltip("A játék jelenet build indexe")]
    [SerializeField] private int gameSceneIndex = 1;

    void Start()
    {
        if (!gameSceneAlreadyLoaded && SceneManager.GetActiveScene().buildIndex == 0)
        {
            Debug.Log("Loading game scene additively as background...");
            SceneManager.LoadScene(gameSceneIndex, LoadSceneMode.Additive);
            gameSceneAlreadyLoaded = true;
            Time.timeScale = 0f;
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        LoadScreenManager.Instance.ShowLoadingScreen();  // Loading screen megjelenítése

        yield return new WaitForSecondsRealtime(0.5f);  // Rövid várakozás a loading screen-nek

        SceneManager.LoadScene(gameSceneIndex, LoadSceneMode.Single);  // Új jelenet betöltése

        LoadScreenManager.Instance.HideLoadingScreen();  // Ha elkészült, eltüntetjük a loading screen-t
    }
    public void Exit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
