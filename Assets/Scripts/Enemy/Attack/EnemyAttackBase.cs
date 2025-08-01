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

    public abstract void AttackPlayer(Vector2 direction, GameObject projectile, float damage);
    public abstract void AttackProtectedTarget(Vector2 direction, GameObject projectile, float damage);
}
