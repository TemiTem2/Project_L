using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Enemy enemy;
    private EnemyStats stats;

    private Transform target;
    private float attackCooldown = 0f;

    private EnemyTargetor targetor;


    private void Update()
    {
        EnemyBehavior();
    }

    public void Initialize(Enemy enemy, EnemyStats stats, EnemyTargetor targetor)
    {
        this.enemy = enemy;
        this.stats = stats;
        this.targetor = targetor;
    }

    #region TargetChange Event
    private void OnEnable()
    {
        targetor.OnTargetChanged += SetTarget;
    }
    private void OnDisable()
    {
        targetor.OnTargetChanged -= SetTarget;
    }
    private void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    #endregion

    private void EnemyBehavior()
    {
        if (target == null) return;
        if (attackCooldown >= 0f) attackCooldown -= Time.deltaTime;
        if (targetor.IsTargetInRange(target))
        {
            if (attackCooldown <= 0f)
            {
                attackCooldown = stats.attackSpeed;
                enemy.ChangeState(EnemyState.attack);
                return;
            }
            enemy.ChangeState(EnemyState.idle);
        }
        else
        {
            enemy.ChangeState(EnemyState.trace);
        }
    }
}
