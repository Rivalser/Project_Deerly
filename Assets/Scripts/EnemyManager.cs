using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    private Enemy_Health[] enemies;
    private bool allEnemiesDead = false;
    private float checkDelay = 1f;

    private void Start()
    {
        InvokeRepeating(nameof(CheckEnemies), checkDelay, checkDelay);
    }

    private void CheckEnemies()
    {
        enemies = FindObjectsOfType<Enemy_Health>();

        bool allDead = true;
        foreach (var enemy in enemies)
        {
            if (enemy != null && enemy.isAlive)
            {
                allDead = false;
                break;
            }
        }

        if (allDead && !allEnemiesDead)
        {
            allEnemiesDead = true;
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Minden enemy meghalt, betöltés: scene index " + nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Nincs több pálya a build settings-ben.");
        }
    }
}
