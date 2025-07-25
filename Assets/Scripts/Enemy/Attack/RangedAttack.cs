using UnityEngine;

public class RangedAttack : EnemyAttackBase
{
    public override void Attack(Vector2 direct,GameObject projectile, int damage)
    {
        Instantiate(projectile, direct, Quaternion.identity);
    }
}
