using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Enemy enemy;
    private EnemyStats stats;
    private AttackType attackType;

    private Transform target;
    private float attackCooldown = 0f;
    private bool isCollided = false;
    private int collideCount = 0;
    private bool isInRange = false;

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
        attackType = stats.attackType;
        isInRange = false;
        isCollided = false;
        if (enemy != null)
        {
            targetor.OnTargetChanged += SetTarget;
            targetor.OnTargetInRange += CheckRange;
        }
    }

    private void OnDisable()
    {
        if (enemy != null)
        {
            targetor.OnTargetChanged -= SetTarget;
            targetor.OnTargetInRange -= CheckRange;
        }
    }
    private void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    private void CheckRange(bool newRange)
    {
        isInRange = newRange;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Protect"))
        {
            collideCount++;
            isCollided = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Protect"))
        {
            collideCount = Mathf.Max(0, collideCount - 1);
            if (collideCount == 0)
                isCollided = false;
        }
    }

    private void EnemyBehavior()
    {
        if (target == null) return;
        if (attackType == AttackType.melee)
        {
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
        else if (attackType == AttackType.ranged)
        {
            if (isInRange)
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
}
