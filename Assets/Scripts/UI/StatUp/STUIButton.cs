using UnityEngine;
using UnityEngine.SceneManagement;

public class STUIButton : MonoBehaviour
{
    [SerializeField]
    int buttonIndex = 0;
    PlayerStatManager playerStatManager;
    [SerializeField]
    StstupWindow statWindow;
    SoundManager soundManager;
    [SerializeField] AudioClip[] buttonSound;
    private void Start()
    {
        playerStatManager = PlayerStatManager.instance;
        soundManager = FindAnyObjectByType<SoundManager>();
    }
    public void StatUp()
    {
        playerStatManager.Statup(buttonIndex);
        statWindow.UIUpdate();
        soundManager.PlaySFX(buttonSound[0]);

    }
    public void StatDown()
    {
        playerStatManager.StatDown(buttonIndex);
        statWindow.UIUpdate();
        soundManager.PlaySFX(buttonSound[1]);
    }
}
