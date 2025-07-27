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

    public override void AttackPlayer(Vector2 direct, GameObject projectile, int damage)
    {
        isAttacking = true;
        if (animEvent.canAttack && isCollisionPlayer)
            StateManager.TakeDamage(damage);
        isAttacking = false;
    }
    public override void AttackProtectedTarget(Vector2 direct, GameObject projectile, int damage)
    {
        isAttacking = true;
        if (animEvent.canAttack && isCollisionProtect)
        {
            protect.TakeDamage(damage);
        }
        isAttacking = false;
    }
}
