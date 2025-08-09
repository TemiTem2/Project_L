using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public enum ButtonType
    {
        ChangeToMain,
        ChangeToCharSelect,
        ChangeToStat,
        ChangeToDay,
        Exit
    }

    public ButtonType buttonType;
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    private void OnEnable()
    {
        btn.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        btn.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        switch (buttonType)
        {
            case ButtonType.ChangeToMain:
                GameManager.Instance.ChangeState(GameState.Main);
                break;
            case ButtonType.ChangeToCharSelect:
                GameManager.Instance.ChangeState(GameState.CharSelect);
                break;
            case ButtonType.ChangeToStat:
                GameManager.Instance.ChangeState(GameState.Stat);   
                break;
            case ButtonType.ChangeToDay:
                GameManager.Instance.ChangeState(GameState.Day);
                break;
            case ButtonType.Exit:
                GameManager.Instance.ExitGame();
                break;
        }
    }
}
