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

        player = GameObject.FindGameObjectWithTag("Player");
        protect = GameObject.FindGameObjectWithTag("Protect");
        target = protect.transform;
        currentTarget = TargetType.Protect;


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
        UpdateTarget();

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
        LevelupManager levelupManager = FindFirstObjectByType<LevelupManager>();
        levelupManager.AddExp(stats.expReward);
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

        switch (currentTarget)
        {
            case TargetType.Player:
                attackScript.TryAttackPlayer(targetDirection, stats.projectilePrefab, stats.damage);
                break;
            case TargetType.Protect:
                attackScript.TryAttackProtectedTarget(targetDirection, stats.projectilePrefab, stats.damage);
                break;
        }
        
        stats.enemyState = EnemyState.idle;
    }
    
    private void UpdateTarget()
    {
        float playerDistance = Vector2.Distance(transform.position, player.transform.position);
        float protectDistance = Vector2.Distance(transform.position, protect.transform.position);
        float attackRange = stats.attackRange;

        if (playerDistance <= attackRange && protectDistance > attackRange)
        {
            target = player.transform;
            currentTarget = TargetType.Player;
        }
        else
        {
            target = protect.transform;
            currentTarget = TargetType.Protect;
        }
    }
}
