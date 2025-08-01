using UnityEngine;

public class RangedAttack : EnemyAttackBase
{
    public override void AttackPlayer(Vector2 direct,GameObject projectile, float damage)
    {
        Instantiate(projectile, direct, Quaternion.identity);
    }
    public override void AttackProtectedTarget(Vector2 direct, GameObject projectile, float damage)
    {
        Instantiate(projectile, direct, Quaternion.identity);
    }

}
