using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject uiElementPrefab;  // A UI elem Prefab-ja

    void Start()
    {
        // Az UI elem nem jelenik meg azonnal
        if (uiElementPrefab != null)
            uiElementPrefab.SetActive(false);  // Kezdetben deaktiváljuk
    }

    // Ez a metódus fog futni, amikor a gombot megnyomják
    public void StartGame()
    {
        if (uiElementPrefab != null)
            uiElementPrefab.SetActive(true);  // Aktiválja az UI elemet
    }
}
