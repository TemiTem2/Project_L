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
        Debug.Log(currentState + "·Î ÀüÈ¯");
        InitializeByState(currentState);
    }

    private void InitializeByState(GameState state)
    {
        switch (state)
        {
            case GameState.Main:
                // Main state initialization logic
                break;
            case GameState.CharSelect:
                // Character selection initialization logic
                break;
            case GameState.Stat:
                // Stat screen initialization logic
                break;
            case GameState.Day:
                // Daytime initialization logic
                break;
            case GameState.Night:
                // Nighttime initialization logic
                break;
            case GameState.GameOver:
                currentDayIndex = 0;
                //load game over scene
                break;
        }
    }

    public void RoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    
}
