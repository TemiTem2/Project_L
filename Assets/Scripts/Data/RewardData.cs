using UnityEngine;

public enum RewardType
{
    None,
    Item,
    Gold,
    Exp
}
[System.Serializable]
public class RewardData
{
    public RewardType rewardType;
    public int amount;
}
