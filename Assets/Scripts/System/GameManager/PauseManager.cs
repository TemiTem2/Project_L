using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private UIManager uiManager;
    public bool isPaused;

    public static PauseManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        isPaused = false;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        uiManager = FindFirstObjectByType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UIManager ����");
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (GameManager.Instance.currentState == GameState.Night)
        {
            if (isPaused)
            {
                Time.timeScale = 0f;
                uiManager.TogglePauseUI();
                Debug.Log("�Ͻ� ����");
            }
            else
            {
                uiManager.TogglePauseUI();
                Time.timeScale = 1f;
                Debug.Log("���� �簳");
            }
        }
        else if (GameManager.Instance.currentState == GameState.Main)
        {
            uiManager.ToggleExit();
        }
        else uiManager.TogglePauseUI();
    }
}
