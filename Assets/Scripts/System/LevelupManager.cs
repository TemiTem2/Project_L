using UnityEngine;

public class LevelupManager : MonoBehaviour
{
    private PlayerStatManager playerstat;

    private void Start()
    {
        playerstat = PlayerStatManager.instance;
    }

    void Update()
    {
        if (playerstat.exp >= playerstat.expToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        playerstat.level++;
        playerstat.exp -= playerstat.expToNextLevel;
        playerstat.expToNextLevel = Mathf.RoundToInt(playerstat.expToNextLevel * 1.2f);
        Debug.Log("레벨업! 현재 레벨: " + playerstat.level + ", 다음 레벨업까지 필요한 경험치: " + playerstat.expToNextLevel);
        playerstat.skillPoints++;
    }

    public void AddExp(int amount)
    {
        playerstat.exp += amount;
        Debug.Log("경험치 추가: " + amount + ", 현재 경험치: " + playerstat.exp);
    }
}
