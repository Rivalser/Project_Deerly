using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    private static string saveFilePath = Application.persistentDataPath + "/savegame.json";

    public static void SaveGame(PlayerHealth playerHealth, List<Enemy_Health> enemies)
    {
        SaveData saveData = new SaveData();

        saveData.currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        saveData.playerData = new PlayerData
        {
            health = playerHealth.GetHealth(),
            position = playerHealth.GetPosition()
        };

        saveData.enemyDataList = new List<EnemyData>();
        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;

            EnemyData enemyData = new EnemyData
            {
                enemyID = enemy.uniqueID,
                health = enemy.GetHealth(),
                position = enemy.GetPosition()
            };
            saveData.enemyDataList.Add(enemyData);
        }

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game Saved.");
    }

    public static void LoadGame(PlayerHealth playerHealth, List<Enemy_Health> enemies)
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning("No save file found.");
            return;
        }

        string json = File.ReadAllText(saveFilePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        playerHealth.SetHealth(saveData.playerData.health);
        playerHealth.SetPosition(saveData.playerData.position);

        for (int i = 0; i < enemies.Count; i++)
        {
            if (i < saveData.enemyDataList.Count && enemies[i] != null)
            {
                enemies[i].SetHealth(saveData.enemyDataList[i].health);
                enemies[i].SetPosition(saveData.enemyDataList[i].position);
            }
        }

        Debug.Log("Game Loaded.");
    }

    public static string GetSavedSceneName()
    {
        if (!File.Exists(saveFilePath)) return null;

        string json = File.ReadAllText(saveFilePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);
        return saveData.currentSceneName;
    }

    public static List<EnemyData> GetEnemyDataList()
    {
        if (!File.Exists(saveFilePath)) return null;

        string json = File.ReadAllText(saveFilePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);
        return saveData.enemyDataList;
    }

    public static SaveData LoadSaveDataRaw()
    {
        if (!File.Exists(saveFilePath)) return null;

        string json = File.ReadAllText(saveFilePath);
        return JsonUtility.FromJson<SaveData>(json);
    }
}
