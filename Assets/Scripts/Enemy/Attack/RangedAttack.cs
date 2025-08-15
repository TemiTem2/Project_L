using UnityEngine;

public class RangedAttack : EnemyAttackBase
{
    protected string projectileTag;

    protected Vector2 attackDirection;

    public override void Initialize(Enemy enemy, EnemyMover mover, AnimationEventRelay animEvent)
    {
        base.Initialize(enemy, mover, animEvent);
        projectileTag = stats.projectileName;
        mover.OnDirectionUpdated += UpdateDirection;
        animEvent.OnAnimationProjectile += TryAttack;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        mover.OnDirectionUpdated -= UpdateDirection;
        animEvent.OnAnimationProjectile -= TryAttack;
    }
    private void UpdateDirection(Vector2 direction)
    {
        attackDirection = direction;
    }

    protected void TryAttack()
    {
        float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        ProjectilePool.Instance.GetObject(projectileTag, transform.position, rotation, attackDirection, damage);
    }
}
