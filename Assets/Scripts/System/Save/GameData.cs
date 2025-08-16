using UnityEngine;

public class GameData
{
    public class PlayerData
    {
        public string currentPlayCharName;
        public string currentPlayerSkill;

        public PlayableCharInfo currentCharInfo;
        public Skill currentSkillInfo;
    }

    public class StatData
    {
        public int level;
        public int exp;
        public int expToNextLevel;
        public int skillPoints;
        public float protectedTargetHP;

        public StatPoint statPoint;
    }

    public class StageData
    {
        public GameState currentState;
        public int currentDayIndex;
    }

    public PlayerData playerData;
    public StageData stageData;
    public StatData statData;
}