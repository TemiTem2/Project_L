using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyTargetor
{
    private Transform target;
    private GameObject player;
    private GameObject protect;

    private TargetType currentTarget;
    private EnemyStats stats;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        protect = GameObject.FindGameObjectWithTag("Protect");
        target = protect.transform;
        currentTarget = TargetType.Protect;
    }
    public void UpdateTarget(Transform transform)
    {
        float playerDistance = Vector2.Distance(transform.position, player.transform.position);
        float protectDistance = Vector2.Distance(transform.position, protect.transform.position);
        float attackRange = stats.attackRange;

        if (playerDistance <= attackRange && protectDistance > attackRange)
        {
            target = player.transform;
            currentTarget = TargetType.Player;
        }
        else
        {
            target = protect.transform;
            currentTarget = TargetType.Protect;
        }
    }
}
