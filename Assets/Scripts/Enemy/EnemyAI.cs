using System.Xml.Serialization;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform target;
    private Enemy enemy;
    private EnemyStats enemyStats;

    private Rigidbody2D rb;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        enemyStats = enemy.stats;
        rb = GetComponent<Rigidbody2D>();
        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }
    }

    private void FixedUpdate()
    {
        if ( target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * enemyStats.moveSpeed * Time.fixedDeltaTime);
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (distanceToTarget <= enemyStats.attackRange)
            {
                TryAttack();
            }
        }
    }

    private void TryAttack()
    {
        if (enemyStats.enemyState == EnemyState.idle)
        {
            enemyStats.enemyState = EnemyState.attack;
        }
    }
}
