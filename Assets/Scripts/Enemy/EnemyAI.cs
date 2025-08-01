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
    private Transform target;
    private GameObject player;
    private GameObject protect;
    private TargetType currentTarget;

    private Rigidbody2D rb;
    private Enemy enemy;

    private EnemyStats stats;


    private Vector2 targetDirection;
    private bool canAttack = false;

    private EnemyTargetor enemyTargetor;

    private void Awake()
    {
        enemyTargetor = new EnemyTargetor();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        stats = enemy.stats;

        player = GameObject.FindGameObjectWithTag("Player");
        protect = GameObject.FindGameObjectWithTag("Protect");
        target = protect.transform;
        currentTarget = TargetType.Protect;
    }

    private void Update()
    {
        if (enemy.enemyState != EnemyState.dead)
        {
            enemyTargetor.UpdateTarget(this.transform);
            if (canAttack)
            {
                enemy.enemyState = EnemyState.attack;
                TryAttack();
            }
        }
    }

    private void FixedUpdate()
    {
        if (enemy.enemyState != EnemyState.dead)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (target != null && enemy.enemyState != EnemyState.dead)
            {
                if (distanceToTarget <= stats.attackRange)
                {
                    canAttack = true;
                }
                else
                {
                    targetDirection = (target.position - transform.position).normalized;
                    rb.MovePosition(rb.position + targetDirection * stats.moveSpeed * Time.fixedDeltaTime);
                    canAttack = false;
                }
            }
        }
    }

    private void TryAttack()
    {
        if (enemy.attackScript != null && enemy.enemyState == EnemyState.attack)
        {
            AttackToPlayer();
        }
        else
        {
            Debug.LogWarning("attackSript is null");
        }
        
    }

    private void AttackToPlayer()
    {

        switch (currentTarget)
        {
            case TargetType.Player:
                enemy.attackScript.AttackPlayer(targetDirection, stats.projectilePrefab, stats.damage);
                break;
            case TargetType.Protect:
                enemy.attackScript.AttackProtectedTarget(targetDirection, stats.projectilePrefab, stats.damage);
                break;
        }
        
        enemy.enemyState = EnemyState.idle;
    }
}
