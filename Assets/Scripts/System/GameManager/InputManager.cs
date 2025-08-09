using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if(GameManager.Instance.currentState != GameState.GameOver && GameManager.Instance.currentState != GameState.GameClear && GameManager.Instance.currentState != GameState.Main)
        {
            CheckEscape();
        }
        else if (GameManager.Instance.currentState == GameState.Main)
        {
            GameManager.Instance.ExitGame();
        }
    }

    private void CheckEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseManager.Instance.TogglePause();
        }
    }
}
