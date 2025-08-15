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
    }

    #region Events
    protected virtual void OnEnable()
    {
        enemy.OnStateChanged += InitAttack;
    }
    protected virtual void OnDisable()
    {
        enemy.OnStateChanged -= InitAttack;
    }
    private void InitAttack(EnemyState state)
    {
        if (state == EnemyState.attack) haveAttacked = false;
    }
    #endregion
}
