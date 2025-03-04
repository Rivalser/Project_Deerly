using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    
    void Start()
    {
        // A Game Scene betöltése háttérben, hogy a kamera aktív legyen
        SceneManager.LoadScene(1 , LoadSceneMode.Additive);

        Time.timeScale = 0;



    }

    public void StartGame()
    {
        Time.timeScale = 1;

        // Amikor elindítjuk a játékot, átváltunk a Game Scene-re
        SceneManager.LoadScene(1);
    }
}
