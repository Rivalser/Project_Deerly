using UnityEngine;

public class LoadScreenManager : MonoBehaviour
{
    public static LoadScreenManager Instance;

    [SerializeField] private GameObject loadingScreenPrefab;
    private GameObject loadingScreenInstance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        loadingScreenInstance = Instantiate(loadingScreenPrefab);
        loadingScreenInstance.transform.SetParent(transform);
        loadingScreenInstance.SetActive(false);
    }

    public void ShowLoadingScreen(float duration = 0.5f)
    {
        Time.timeScale = 0f;
        if (loadingScreenInstance == null) return;
        loadingScreenInstance.SetActive(true);
        Invoke(nameof(HideLoadingScreen), duration);
    }

    public void HideLoadingScreen()
    {
        if (loadingScreenInstance == null) return;
        loadingScreenInstance.SetActive(false);
        Time.timeScale = 1f;
    }

    // 🔥 ÚJ METÓDUS, ami megoldja a hibát!
    public void ShowLoadScreen()
    {
        ShowLoadingScreen();
    }
}
