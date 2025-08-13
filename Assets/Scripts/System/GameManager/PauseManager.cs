using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public bool isPaused;

    public static PauseManager Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }



    [SerializeField] private UIRelay uiRelay;



    private void Start()
    {
        isPaused = false;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (GameManager.Instance.currentState == GameState.Night)
        {
            if (isPaused)
            {
                Time.timeScale = 0f;
                uiRelay.TogglePauseUI();
                Debug.Log("일시 정지");
            }
            else
            {
                uiRelay.TogglePauseUI();
                Time.timeScale = 1f;
                Debug.Log("게임 재개");
            }
        }
        else ToggleExit();
    }

    public void ToggleExit()
    {
        if (GameManager.Instance.currentState == GameState.Main)
        {
            uiRelay.ToggleExitUI();
        }
        else
        {
            uiRelay.TogglePauseUI();
        }
    }
}
