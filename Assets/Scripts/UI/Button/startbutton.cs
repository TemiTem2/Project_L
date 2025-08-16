using UnityEngine;

public class startbutton : MonoBehaviour
{
    public PlayerStatManager playerStat;
    [SerializeField] float protectedTargetHP = 100f;
    void Start()
    {
        playerStat = PlayerStatManager.instance;
    }

    public void ResetProtectedTargetHP()
    {
        playerStat.protectedTargetHP = protectedTargetHP;
    }
}
