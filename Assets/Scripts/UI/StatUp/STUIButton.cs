using UnityEngine;
using UnityEngine.SceneManagement;

public class STUIButton : MonoBehaviour
{
    [SerializeField]
    int buttonIndex = 0;
    public void StatUp()
    {
        
    }
    public void StatDown()
    {
        
    }
    public void StartFight()
    {
        SceneManager.LoadScene("Test_Stage(07.27)");
    }
}
