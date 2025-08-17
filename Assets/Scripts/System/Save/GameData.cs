using UnityEngine;

[System.Serializable]
public class GameData
{
    public PlayerData playerData;
    public StageData stageData;
    public StatData statData;

    [System.Serializable]
    public class PlayerData
    {
        public string currentPlayCharName;
        public string currentPlayerSkill;

        public PlayableCharInfo currentCharInfo;
        public Skill currentSkillInfo;
    }

    [System.Serializable]
    public class StatData
    {
        public int level;
        public int exp;
        public int expToNextLevel;
        public int skillPoints;
        public float protectedTargetHP;

        public StatPoint statPoint;
    }

    [System.Serializable]
    public class StageData
    {
        public GameState currentState;
        public int currentDayIndex;
        public int highScore;
    }

}