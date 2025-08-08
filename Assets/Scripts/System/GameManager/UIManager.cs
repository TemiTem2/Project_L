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
    [SerializeField] private GameObject exitUI;
    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject pauseUI;


    private void Start()
    {
        InitializeUI();
    }

    void InitializeUI()
    {
        pauseUI?.SetActive(false);
        settingsUI?.SetActive(false);
    }

    public void TogglePauseUI()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
    }
}
