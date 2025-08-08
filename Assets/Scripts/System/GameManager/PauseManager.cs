using UnityEngine;

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
        uiManager = FindFirstObjectByType<UIManager>();
        isPaused = false;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
            uiManager.TogglePauseUI();
            Debug.Log("일시 정지");
        }
        else
        {
            uiManager.TogglePauseUI();
            Time.timeScale = 1f;
            Debug.Log("게임 재개");
        }
    }
}
