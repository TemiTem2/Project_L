using UnityEngine;
using UnityEngine.SceneManagement;

public class tempbutton : MonoBehaviour
{
    public void StartFight()
    {
        Database database = FindFirstObjectByType<Database>();
        database.LoadPlayerData();
        SceneManager.LoadScene("Test_Stage(07.27)");
    }
}
