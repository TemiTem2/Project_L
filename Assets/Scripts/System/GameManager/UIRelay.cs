using UnityEngine;
using UnityEngine.SceneManagement;

public class UIRelay
{
    private UIManager uiManager;
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //uiManager = FindFirstObjectByType<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("UIManager ¾øÀ½");
        }
    }
}
