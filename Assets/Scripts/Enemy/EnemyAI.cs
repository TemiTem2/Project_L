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
    private bool isCooldown = false;
    public float currentHP;
    private float currentCooldown;



    void Start()
    {
        enemyAnim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        stats = enemy.stats;
        currentCooldown = 0f;


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

        if (stats.enemyState != EnemyState.dead)
        {
            if (canAttack && !isCooldown)
            {
                stats.enemyState = EnemyState.attack;
                TryAttack();
            }
            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime * stats.attackSpeed;
                isCooldown = true;
            }
            else
            {
                currentCooldown = 0f;
                isCooldown = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (stats.enemyState != EnemyState.dead)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (target != null && stats.enemyState != EnemyState.dead)
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

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        Hurt();
    }

    private void Hurt()
    {
        if (stats.enemyState == EnemyState.dead) return;
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
        currentCooldown = 1f;
        enemyAnim.SetTrigger("attack");
        attackScript.TryAttack(targetDirection, stats.projectilePrefab, stats.damage);
        
        stats.enemyState = EnemyState.idle;
    }
    
}
