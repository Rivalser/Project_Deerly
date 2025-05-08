using System.Collections.Generic;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    private PlayerHealth player;
    private List<Enemy_Health> enemies;

    private void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
        enemies = new List<Enemy_Health>(FindObjectsOfType<Enemy_Health>());

        if (player == null)
            Debug.LogWarning("PlayerHealth példány nem található!");

        if (enemies.Count == 0)
            Debug.LogWarning("Nem található egy Enemy_Health sem!");

    }

    public void SaveGame()
    {
        if (player != null && enemies != null)
            SaveSystem.SaveGame(player, enemies);
        Debug.Log("Sikeresen meghívódott a SaveSystem.SaveGame()!");

        Debug.Log("SaveGame() hívva...");
        if (player != null && enemies != null)
            SaveSystem.SaveGame(player, enemies);
        else
            Debug.LogWarning("Nem lehet menteni! player vagy enemies null.");

    }

    public void LoadGame()
    {
        if (player != null && enemies != null)
            SaveSystem.LoadGame(player, enemies);
    }
}