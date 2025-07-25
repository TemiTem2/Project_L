using UnityEngine;

public abstract class EnemyAttackBase : MonoBehaviour
{
    protected Enemy enemy;

    public virtual void Initialize(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public abstract void Attack(Vector2 direct, GameObject projectile, int damage);
}
