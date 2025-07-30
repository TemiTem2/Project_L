using UnityEngine;
using UnityEngine.SceneManagement;

public class STUIButton : MonoBehaviour
{
    [SerializeField]
    int buttonIndex = 0;
    PlayerStatManager playerStatManager;
    private void Start()
    {
        playerStatManager = FindFirstObjectByType<PlayerStatManager>();
    }
    public void StatUp()
    {
        playerStatManager.Statup(buttonIndex);
    }
    public void StatDown()
    {
        playerStatManager.StatDown(buttonIndex);
    }
}
