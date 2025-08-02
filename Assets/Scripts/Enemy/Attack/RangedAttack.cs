using UnityEngine;

public class RangedAttack : EnemyAttackBase
{
    public override void TryAttack(TargetType targetType, Vector2 direct,GameObject projectile, float damage)
    {
        Instantiate(projectile, direct, Quaternion.identity);
    }

}
