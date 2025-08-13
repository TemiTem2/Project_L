using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [SerializeField] private UIRelay uiRelay;

    private Dictionary<string, float> settings = new Dictionary<string, float>
    {
        { "FullScreen", 0.0f },
        { "MainVolume", 1.0f },
        { "BGMVolume", 1.0f },
        { "SFXVolume", 1.0f },
        //{ "Brightness", 1.0f },
        //{ "Sensitivity", 0.5f }
    };

    public Dictionary<string, float> LoadSettings()
    {
        return settings;
    }

    public void SaveSettings(Dictionary<string, float> newSettings)
    {
        settings = newSettings;
        ApplySettings();
    }

    private void ApplySettings()
    {
        GetFullScreen();
    }

    private void GetFullScreen()
    {
        bool isFullScreen = settings["FullScreen"] != 0f;
        Screen.fullScreen = isFullScreen;
    }

    public void ToggleSetting()
    {
        uiRelay.ToggleSettingUI();
    }
}
