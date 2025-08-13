using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Lists")]
    //[SerializeField] private GameObject mainUI;
    //[SerializeField] private GameObject charSelectUI;
    //[SerializeField] private GameObject statUI;
    //[SerializeField] private GameObject dayUI;
    //[SerializeField] private GameObject nightUI;
    //[SerializeField] private GameObject gameClearUI;
    //[SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject exitUI;


    private void Start()
    {
        InitializeUI();
    }

    void InitializeUI()
    {
        if (pauseUI != null) pauseUI.SetActive(false);
        if (settingsUI != null) settingsUI.SetActive(false);
        if (exitUI != null) exitUI.SetActive(false);
    }

    public void TogglePauseUI()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
    }
    public void ToggleSettingUI()
    {
        settingsUI.SetActive(!settingsUI.activeSelf);
    }
    public void ToggleExitUI()
    {
        exitUI.SetActive(!exitUI.activeSelf);
    }
}
