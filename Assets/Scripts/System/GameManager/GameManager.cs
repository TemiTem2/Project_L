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
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState = GameState.Main;
    private int maxDayIndex = 10;
    public int currentDayIndex;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {

    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log(currentState + "로 전환");
        InitializeByState(newState);
    }

    public void ChangeToMain()
    {
        ChangeState(GameState.Main);
    }

    public void ChangeToCharSelect()
    {
        ChangeState(GameState.CharSelect);
    }

    private void InitializeByState(GameState state)
    {
        switch (state)
        {
            case GameState.Main:
                RoadScene("Test_main");
                break;
            case GameState.CharSelect:
                RoadScene("SelectChar");
                break;
            case GameState.Stat:
                RoadScene("Test_statup");
                break;
            case GameState.Day:
                currentDayIndex++;
                if (currentDayIndex > maxDayIndex) ChangeState(GameState.GameClear);
                else RoadScene("Test_RandomEvent");
                break;
            case GameState.Night:
                RoadScene("Test_Stage");
                break;
            case GameState.GameClear:
                currentDayIndex = 0;
                RoadScene("GameClear");
                break;
            case GameState.GameOver:
                currentDayIndex = 0;
                RoadScene("GameOver");
                break;
        }
    }

    private void RoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
