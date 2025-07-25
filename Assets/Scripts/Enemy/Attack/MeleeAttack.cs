using System.Collections;
using UnityEngine;

public class MeleeAttack : EnemyAttackBase
{
    private AnimationEventRelay animEvent;
    private bool isCollision = false;

    private void Start()
    {
        animEvent = GetComponentInChildren<AnimationEventRelay>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollision = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollision = false;
        }
    }

    public override void Attack(Vector2 direct, GameObject projectile, int damage)
    {
        isAttacking = true;
        if (animEvent.canAttack && isCollision)
            StateManager.TakeDamage(damage);
        isAttacking = false;
    }
}
