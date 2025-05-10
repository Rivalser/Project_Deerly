using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public List<Enemy_Health> enemies;

    private void Start()
    {
        // Ha szeretnéd, hogy a betöltés azonnal történjen a játék indításakor:
        SaveSystem.LoadGame(playerHealth, enemies);
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(playerHealth, enemies);
    }

    public void LoadGame()
    {
        SaveSystem.LoadGame(playerHealth, enemies);
    }
}
