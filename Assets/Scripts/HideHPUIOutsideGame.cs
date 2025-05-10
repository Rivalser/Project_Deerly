using UnityEngine;
using UnityEngine.SceneManagement;

public class HideHPUIOutsideGame : MonoBehaviour
{
    // Add meg, melyik jelenetekben LÁTSZÓDJON a HP UI
    [SerializeField] private string[] allowedScenes = { "Level1", "Level2", "BossFight" };

    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        bool shouldBeActive = false;
        foreach (string sceneName in allowedScenes)
        {
            if (sceneName == currentScene)
            {
                shouldBeActive = true;
                break;
            }
        }

        gameObject.SetActive(shouldBeActive);
    }
}
