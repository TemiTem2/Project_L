using UnityEngine;

public enum RewardType
{
    None,
    SkillPoint,
    HP,
    Exp
}
[System.Serializable]
public class RewardData
{
    public RewardType rewardType;
    public int amount;
}
