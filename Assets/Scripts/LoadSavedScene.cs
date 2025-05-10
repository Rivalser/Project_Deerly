using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LoadSavedScene : MonoBehaviour
{
    private SaveData loadedSaveData;

    public void LoadSavedSceneFromSave()
    {
        string savedSceneName = SaveSystem.GetSavedSceneName();

        if (!string.IsNullOrEmpty(savedSceneName))
        {
            loadedSaveData = SaveSystem.LoadSaveDataRaw();

            LoadScreenManager.Instance.ShowLoadingScreen();  // Betöltési képernyő megjelenítése

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(savedSceneName, LoadSceneMode.Single);  // Betöltés Single módban
        }
        else
        {
            Debug.LogWarning("No saved scene found.");
        }
    }  

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Show loading screen
        LoadScreenManager.Instance.ShowLoadingScreen();

        // Start async load
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        SceneManager.sceneLoaded += OnSceneLoaded;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Wait one frame extra, so OnSceneLoaded can finish
        yield return null;

        // Hide loading screen after scene loaded and restored
        LoadScreenManager.Instance.HideLoadingScreen();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        PlayerHealth player = FindObjectOfType<PlayerHealth>();
        List<Enemy_Health> enemies = new List<Enemy_Health>(FindObjectsOfType<Enemy_Health>());

        if (loadedSaveData != null && player != null)
        {
            player.SetHealth(loadedSaveData.playerData.health);
            player.SetPosition(loadedSaveData.playerData.position);

            for (int i = 0; i < enemies.Count && i < loadedSaveData.enemyDataList.Count; i++)
            {
                enemies[i].SetHealth(loadedSaveData.enemyDataList[i].health);
                enemies[i].SetPosition(loadedSaveData.enemyDataList[i].position);
            }

            Debug.Log("Saved game state loaded successfully.");
        }
        else
        {
            Debug.LogWarning("Player or saved data missing in loaded scene.");
        }

        LoadScreenManager.Instance.HideLoadingScreen();  // ⬅ loading screen eltüntetése
    }
}
