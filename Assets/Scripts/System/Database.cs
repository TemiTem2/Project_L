using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Database : MonoBehaviour
{
    public PlayableCharInfo[] playableCharInfo;
    public string currentPlayCharName;
    public string currentPlayerSkill;

    public PlayableCharInfo currentCharInfo;
    public Skill currentSkillInfo;

    public static Database Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LoadPlayerData();
        if (currentCharInfo != null)
        {
            LoadSkillData();
            return;
        }
    }
    public void LoadPlayerData()
    {
        for (int i = 0; i < playableCharInfo.Length; i++)
        {
            if (playableCharInfo[i].charName == currentPlayCharName)
            {
                currentCharInfo = playableCharInfo[i];
                for (int j = 0; j < playableCharInfo[i].skills.Length; j++)
                {
                    if (playableCharInfo[i].skills[j].skillname == currentPlayerSkill)
                    {
                        currentSkillInfo = playableCharInfo[i].skills[j];
                    }
                }
            }
        }
    }

    public void LoadSkillData()
    {
        for (int j = 0; j < currentCharInfo.skills.Length; j++)
        {
            if (currentCharInfo.skills[j].skillname == currentPlayerSkill)
            {
                currentSkillInfo = currentCharInfo.skills[j];
            }
        }
    }

    public void ResetDatabase()
    {
        currentPlayCharName = string.Empty;
        currentPlayerSkill = string.Empty;

        currentCharInfo = null;
        currentSkillInfo = null;
    }
}
