using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Main,
    CharSelect,
    Stat,
    Day,
    Night,
    GameClear,
    GameOver,
    Opening,
    Ending,
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState = GameState.Main;
    private int maxDayIndex = 7;
    public int currentDayIndex;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void ChangeState(GameState newState)
    {
        if (currentState == GameState.Night)
        {
            EnemyPool.Instance.ReturnAllObjects();
            ProjectilePool.Instance.ReturnAllObjects();
        }
        currentState = newState;
        Debug.Log(currentState + "로 전환");
        InitializeByState(newState);
    }

    private void InitializeByState(GameState state)
    {
        switch (state)
        {
            case GameState.Main:
                LoadScene("Test_main");
                break;
            case GameState.Opening:
                LoadScene("Test_Opening");
                break;
            case GameState.CharSelect:
                LoadScene("Test_SelectChar");
                break;
            case GameState.Stat:
                LoadScene("Test_statup");
                break;
            case GameState.Day:
                currentDayIndex++;
                if (currentDayIndex > maxDayIndex) ChangeState(GameState.Ending);
                else LoadScene("Test_RandomEvent");
                break;
            case GameState.Night:
                LoadScene("Test_Stage");
                break;
            case GameState.Ending:
                LoadScene("Test_Ending");
                break;
            case GameState.GameClear:
                currentDayIndex = 0;
                LoadScene("GameClear");
                break;
            case GameState.GameOver:
                currentDayIndex = 0;
                LoadScene("GameOver");
                break;
        }
    }

    private void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
