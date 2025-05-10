using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : MonoBehaviour
{
    void Start()
    {
        // Csak ha tényleg a játék scene van aktívan
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            Time.timeScale = 1f;
        }
    }
}
