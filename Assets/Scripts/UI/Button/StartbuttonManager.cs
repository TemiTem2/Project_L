using UnityEngine;

public class StartbuttonManager : MonoBehaviour
{
    Database database => Database.Instance;
    [SerializeField] GameObject startButton;
    void Start()
    {
        startButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckCanStart();
    }

    public void CheckCanStart()
    {
        if (database.currentCharInfo)
        {
            if(database.currentSkillInfo&&database.currentSkillInfo.skillname!="None")
            {
                startButton.SetActive(true);
                return;
            }
            else
            {
                startButton.SetActive(false);
            }
        }
        else
        {
            startButton.SetActive(false);
        }
    }
}
