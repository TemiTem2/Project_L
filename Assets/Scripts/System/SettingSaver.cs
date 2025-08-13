using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingSaver : MonoBehaviour
{
    private Dictionary<string, float> prevSettings;

    private UIManager uiManager;
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Slider mainVolumeSlider;
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    //[SerializeField] private Slider brightnessSlider;
    //[SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button returnButton;


    private void OnEnable()
    {
        LoadSettings();
        saveButton.onClick.AddListener(SaveSettings);
    }

    private void OnDisable()
    {
        saveButton.onClick.RemoveListener(SaveSettings);
    }

    private void LoadSettings()
    {
        prevSettings = SettingManager.Instance.LoadSettings();

        fullScreenToggle.isOn = FloatToToggle(prevSettings["FullScreen"]);
        mainVolumeSlider.value = prevSettings["MainVolume"];
        bgmVolumeSlider.value = prevSettings["BGMVolume"];
        sfxVolumeSlider.value = prevSettings["SFXVolume"];
        //brightnessSlider.value = prevSettings["Brightness"];
        //sensitivitySlider.value = prevSettings["Sensitivity"];
    }

    private void SaveSettings()
    {
        Dictionary<string, float> newSettings = new Dictionary<string, float>
        {
            { "FullScreen", ToggleToFloat(fullScreenToggle.isOn) },
            { "MainVolume", mainVolumeSlider.value },
            { "BGMVolume", bgmVolumeSlider.value },
            { "SFXVolume", sfxVolumeSlider.value },
            //{ "Brightness", brightnessSlider.value },
            //{ "Sensitivity", sensitivitySlider.value }
        };
        
        SettingManager.Instance.SaveSettings(newSettings);
    }

    public bool FloatToToggle(float toggleFloat)
    {
        return toggleFloat != 0f;
    }

    public float ToggleToFloat(bool toggleValue)
    {
        return toggleValue ? 1f : 0f;
    }
}
