using UnityEngine;
using UnityEngine.SceneManagement;

public class tempbutton : MonoBehaviour
{
    public void StartFight()
    {
        Database database = FindFirstObjectByType<Database>();
        database.LoadPlayerData();
    }
}
