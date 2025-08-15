using UnityEngine;

public class startbutton : MonoBehaviour
{
    public PlayerStatManager playerStat;
    void Start()
    {
        playerStat = PlayerStatManager.instance;
    }

    public void ResetProtectedTargetHP()
    {
        playerStat.protectedTargetHP = 100f;
    }
}
