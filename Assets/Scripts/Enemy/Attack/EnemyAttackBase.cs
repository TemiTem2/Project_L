using System.Collections;
using UnityEngine;

public abstract class EnemyAttackBase : MonoBehaviour
{
    protected Enemy enemy;
    protected bool canAttack = false;

    protected GameObject player;
    protected StateManager stateManager;

    public virtual void Initialize(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public abstract void TryAttack(TargetType targetType, Vector2 direction, GameObject projectile, float damage);
}
