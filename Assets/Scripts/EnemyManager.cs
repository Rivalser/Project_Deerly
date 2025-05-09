using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    private Enemy_Health[] enemies;
    private PickUpItem[] items;

    private bool transitionStarted = false;
    private float checkDelay = 1f;

    private void Start()
    {
        InvokeRepeating(nameof(CheckConditions), checkDelay, checkDelay);
    }

    private void CheckConditions()
    {
        if (transitionStarted) return;

        enemies = FindObjectsOfType<Enemy_Health>();
        items = FindObjectsOfType<PickUpItem>();

        bool allEnemiesDead = true;
        bool allItemsCollected = true;

        foreach (var enemy in enemies)
        {
            if (enemy != null && enemy.isAlive)
            {
                allEnemiesDead = false;
                break;
            }
        }

        foreach (var item in items)
        {
            if (item != null && !item.isCollected)
            {
                allItemsCollected = false;
                break;
            }
        }

        if (allEnemiesDead && allItemsCollected)
        {
            transitionStarted = true;
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Minden enemy meghalt ÉS minden tárgy fel lett véve. Következő pálya betöltése...");
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Nincs több pálya a build settings-ben.");
        }
    }
}
