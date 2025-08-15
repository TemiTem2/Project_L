using UnityEngine;

public abstract class EnemyAttackBase : MonoBehaviour
{
    protected Enemy enemy;
    protected EnemyMover mover;
    protected AnimationEventRelay animEvent;
    protected EnemyStats stats;
    protected float damage;

    protected bool haveAttacked = false;

    public virtual void Initialize(Enemy enemy, EnemyMover mover, AnimationEventRelay animEvent)
    {
        this.enemy = enemy;
        this.mover = mover;
        this.animEvent = animEvent;
        stats = enemy.stats;
        damage = stats.damage;
        enemy.OnStateChanged += InitAttack;
    }

    protected virtual void OnDisable()
    {
        enemy.OnStateChanged -= InitAttack;
    }
    protected void InitAttack(EnemyState state)
    {
        if (state == EnemyState.attack) haveAttacked = false;
    }
}
