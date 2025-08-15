using UnityEngine;

public class RangedAttack : EnemyAttackBase
{
    protected string projectileTag;

    protected Vector2 attackDirection;

    public override void Initialize(Enemy enemy, EnemyMover mover, AnimationEventRelay animEvent)
    {
        base.Initialize(enemy, mover, animEvent);
        tag = stats.projectileName;
    }

    #region Events
    protected override void OnEnable()
    {
        base.OnEnable();
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
    #endregion

    protected void TryAttack()
    {
        float angle = Mathf.Atan2(attackDirection.x, attackDirection.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        ProjectilePool.Instance.GetObject(projectileTag, transform.position, rotation, attackDirection, damage);
    }
}
