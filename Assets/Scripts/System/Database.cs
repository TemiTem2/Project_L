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
    public void Playerdataload()
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

}
