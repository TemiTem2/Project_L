using UnityEngine;

public class RangedAttack : EnemyAttackBase
{
    public override void TryAttack(TargetType targetType, Vector2 direct,GameObject projectile, float damage)
    {
        float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        GameObject proj = Instantiate(projectile, transform.position, rotation);
        ProjectileBase projectileBase = proj.GetComponent<ProjectileBase>();
        if (projectileBase != null)
        {
            projectileBase.SetDirection(direct);
        }
    }

}
