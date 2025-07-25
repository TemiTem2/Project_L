using System.Xml.Serialization;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform target;
    private Animator enemyAnim;
    private Rigidbody2D rb;
    private Enemy enemy;
    private EnemyStats stats;
    private EnemyAttackBase attackScript;


    private Vector2 targetDirection;
    private bool canAttack = false;
    public float currentHP;

    

    void Start()
    {
        enemyAnim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        stats = enemy.stats;


        currentHP = stats.maxHP;

        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }

        switch (stats.attackType)
        {
            case AttackType.melee:
                attackScript = gameObject.AddComponent<MeleeAttack>();
                break;
            case AttackType.ranged:
                attackScript = gameObject.AddComponent<RangedAttack>();
                break;
            default:
                Debug.LogWarning("attackType error");
                break;
        }

        attackScript.Initialize(enemy);
    }

    private void Update()
    {
        if( canAttack && stats.enemyState == EnemyState.idle)
        {
            stats.enemyState = EnemyState.attack;
            TryAttack();
        }
    }

    private void FixedUpdate()
    {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        if ( target != null && stats.enemyState != EnemyState.dead)
        {
            if (distanceToTarget <= stats.attackRange)
            {
                canAttack = true;
            }
            else
            {
                targetDirection = (target.position - transform.position).normalized;
                rb.MovePosition(rb.position + targetDirection * stats.moveSpeed * Time.fixedDeltaTime);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        Hurt();
    }

    private void Hurt()
    {
        if (currentHP <= 0)
        {
            enemyAnim.SetTrigger("hurt");
            Dead();
        }
        else
        {
            enemyAnim.SetTrigger("hurt");
        }
    }

    private void Dead()
    {
        stats.enemyState = EnemyState.dead;
        enemyAnim.SetBool("isDead", true);
        Destroy(gameObject, 3f);
    }

    private void TryAttack()
    {
        if (attackScript != null && stats.enemyState == EnemyState.attack)
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
        enemyAnim.SetTrigger("attack");
        attackScript.TryAttack(targetDirection, stats.projectilePrefab, stats.damage);
        stats.enemyState = EnemyState.idle;
    }
    
}
