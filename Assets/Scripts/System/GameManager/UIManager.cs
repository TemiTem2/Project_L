using UnityEngine;

public class UIManager : MonoBehaviour
{
    void CheckCurrentUI()
    {
        switch(GameManager.Instance.currentState)
        {
            case GameState.Main:
                Debug.Log("메인 UI 활성화");
                break;
            case GameState.CharSelect:
                Debug.Log("게임 플레이 UI 활성화");
                break;
            case GameState.Stat:
                Debug.Log("스탯 UI 활성화");
                break;
            case GameState.Day:
                Debug.Log("낮 UI 활성화");
                break;
            case GameState.Night:
                Debug.Log("밤 UI 활성화");
                break;
            case GameState.GameOver:
                Debug.Log("게임 오버 UI 활성화");
                break;
        }
    }
}
