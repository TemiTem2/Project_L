using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Enemy enemy;
    private EnemyStats stats;

    private Transform target;
    private float attackCooldown = 0f;
    private bool isCollided = false;

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
        isCollided = false;
        if (enemy != null) targetor.OnTargetChanged += SetTarget;
    }

    private void OnDisable()
    {
        if (enemy != null) targetor.OnTargetChanged -= SetTarget;
    }
    private void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Protect")) isCollided = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Protect")) isCollided = false;
    }

    private void EnemyBehavior()
    {
        if (target == null) return;
        if (isCollided)
        {
            if (attackCooldown > 0f)
            {
                attackCooldown -= Time.deltaTime;
                enemy.ChangeState(EnemyState.idle);
            }
            else
            {
                attackCooldown = stats.attackSpeed;
                enemy.ChangeState(EnemyState.attack);
            }
        }
        else enemy.ChangeState(EnemyState.trace);
    }
}
