using UnityEngine;

public class UIManager : MonoBehaviour
{
    void CheckCurrentUI()
    {
        switch(GameManager.Instance.currentState)
        {
            case GameState.Main:
                Debug.Log("���� UI Ȱ��ȭ");
                break;
            case GameState.CharSelect:
                Debug.Log("���� �÷��� UI Ȱ��ȭ");
                break;
            case GameState.Stat:
                Debug.Log("���� UI Ȱ��ȭ");
                break;
            case GameState.Day:
                Debug.Log("�� UI Ȱ��ȭ");
                break;
            case GameState.Night:
                Debug.Log("�� UI Ȱ��ȭ");
                break;
            case GameState.GameOver:
                Debug.Log("���� ���� UI Ȱ��ȭ");
                break;
        }
    }
}
