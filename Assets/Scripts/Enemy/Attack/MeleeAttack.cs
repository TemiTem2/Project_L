using System.Collections;
using UnityEngine;

public class MeleeAttack : EnemyAttackBase
{
    private AnimationEventRelay animEvent;

    private void Start()
    {
        animEvent = GetComponentInChildren<AnimationEventRelay>();
    }

    public override void Attack(Vector2 direct, GameObject projectile, int damage)
    {
        if (animEvent.canAttack)
            StateManager.TakeDamage(damage);
    }
}
