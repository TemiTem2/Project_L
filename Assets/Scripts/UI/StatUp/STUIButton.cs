using UnityEngine;
using UnityEngine.SceneManagement;

public class STUIButton : MonoBehaviour
{
    [SerializeField]
    int buttonIndex = 0;
    PlayerStatManager playerStatManager;
    StstupWindow statWindow;
    private void Start()
    {
        playerStatManager = FindFirstObjectByType<PlayerStatManager>();
        statWindow = FindFirstObjectByType<StstupWindow>();
    }
    public void StatUp()
    {
        playerStatManager.Statup(buttonIndex);
        statWindow.UIUpdate();
    }
    public void StatDown()
    {
        playerStatManager.StatDown(buttonIndex);
        statWindow.UIUpdate();
    }
}
