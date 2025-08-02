using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyTargetor
{   
    private GameObject player;
    private GameObject protect;

    public void InitializeEnemyTargetor()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        protect = GameObject.FindGameObjectWithTag("Protect");
    }

    public TargetType UpdateTarget(EnemyStats stats, Transform transform)
    {
        float playerDistance = Vector2.Distance(transform.position, player.transform.position);
        float protectDistance = Vector2.Distance(transform.position, protect.transform.position);
        float attackRange = stats.attackRange;

        if (playerDistance <= attackRange && protectDistance > attackRange)
        {
            return TargetType.Player;
        }
        else
        {
            return TargetType.Protect;
        }
    }

    public Transform GetTargetTransform(TargetType targetType)
    {
        switch (targetType)
        {
            case TargetType.Player:
                return player.transform;
            case TargetType.Protect:
                return protect.transform;
            default:
                return null;
        }
    }
}
