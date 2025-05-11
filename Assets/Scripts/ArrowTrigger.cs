using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowTrigger : MonoBehaviour
{
    public int sceneToLoadIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Játékos elérte a nyilat! Betöltés: " + sceneToLoadIndex);
            SceneManager.LoadScene(sceneToLoadIndex);
        }
    }
}
