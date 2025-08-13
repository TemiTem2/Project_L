using UnityEngine;
using UnityEngine.SceneManagement;

public class UIRelay: MonoBehaviour
{
    public static UIRelay Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private UIManager uiManager;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        uiManager = FindFirstObjectByType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UIManager ¾øÀ½");
        }
    }
    
    public void TogglePauseUI()
    {
        uiManager.TogglePauseUI();
    }
    public void ToggleExitUI()
    {
        uiManager.ToggleExitUI();
    }
    public void ToggleSettingUI()
    {
        uiManager.ToggleSettingUI();
    }
}
