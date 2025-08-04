using System.Xml.Serialization;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public enum TargetType
{
    Player,
    Protect
}
public class EnemyAI : MonoBehaviour
{
    private Enemy enemy;
    private EnemyStats stats;


    private TargetType targetType;
    private Transform target;
    private Vector2 targetDirection;
    private bool canAttack = false;
    private float attackCooldown = 0f;

    private EnemyTargetor enemyTargetor;
    private EnemyMover enemyMover;

    private void Awake()
    {
        enemyTargetor = new EnemyTargetor();
        enemyMover = new EnemyMover();
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
        stats = enemy.stats;

        enemyTargetor.InitializeEnemyTargetor();
        enemyMover.InitializeEnemyMover(enemy);
    }

    private void Update()
    {
        if (enemy.enemyState == EnemyState.dead)
        {
            this.enabled = false;
        }
        else
        {
            if (attackCooldown >= 0f) attackCooldown -= Time.deltaTime;
            UpdateTargetInformation();
            EnemyBehavior();
        }
    }

    private void EnemyBehavior()
    {
        if (canAttack)
        {
            if (attackCooldown <= 0f)
            {
                attackCooldown = stats.attackSpeed;
                enemy.enemyState = EnemyState.attack;
                TryAttack();
            }
        }
        else
        {
            enemyMover.MoveEnemy(targetDirection, stats.moveSpeed);
        }
    }
    private void UpdateTargetInformation()
    {
        targetType = enemyTargetor.UpdateTarget(stats, transform);
        target = enemyTargetor.GetTargetTransform(targetType);
        targetDirection = enemyMover.GetTargetDirection(transform, target);
        canAttack = enemyMover.IsTargetInRange(transform, target, stats.attackRange);
    }

    private void TryAttack()
    {
            enemy.attackScript.TryAttack(targetType, targetDirection, stats.projectilePrefab, stats.damage);
    }

}
