using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // �簳 ���� ���� �ε� Ÿ��Ʋ ����
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
            Debug.Log("�Ͻ� ����");
        }
        else
        {
            Time.timeScale = 1f;
            Debug.Log("���� �簳");
        }
    }
}
