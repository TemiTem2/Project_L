using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // 재개 설정 저장 로드 타이틀 종료
    public bool isPaused;

    public static PauseManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
            Debug.Log("일시 정지");
        }
        else
        {
            Time.timeScale = 1f;
            Debug.Log("게임 재개");
        }
    }
}
