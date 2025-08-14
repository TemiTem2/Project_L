using System.Collections;
using UnityEngine;

public class MeleeAttack : EnemyAttackBase
{
    private AnimationEventRelay animEvent;
    private bool isCollisionPlayer = false;
    private bool isCollisionProtect = false;
    private ProtectedTarget protect;

    private void Start()
    {
        animEvent = GetComponentInChildren<AnimationEventRelay>();
        protect = FindFirstObjectByType<ProtectedTarget>();
        player = GameObject.FindGameObjectWithTag("PlayerManager");
        stateManager = player.GetComponent<StateManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollisionPlayer = true;
        }
        if (collision.gameObject.CompareTag("Protect"))
        {
            isCollisionProtect = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollisionPlayer = false;
        }
        if (collision.gameObject.CompareTag("Protect"))
        {
            isCollisionProtect = false;
        }
    }

    public override void TryAttack(TargetType targetType, Vector2 direct, string tag, float damage)
    {
        switch (targetType)
        {
            case TargetType.Player:
                if (animEvent.canAttack && isCollisionPlayer && !animEvent.isAttacked)
                {
                    stateManager.TakeDamage(damage);
                    animEvent.isAttacked = true;
                }
                break;
            case TargetType.Protect:
                if (animEvent.canAttack && isCollisionProtect && !animEvent.isAttacked)
                {
                    protect.TakeDamage(damage);
                    animEvent.isAttacked = true;
                }
                break;
            default:
                Debug.LogWarning("Unknown target type");
                break;
        }
    }
}
