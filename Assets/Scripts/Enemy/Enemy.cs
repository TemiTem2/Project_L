using System;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IPoolable
{
    public EnemyStats stats = new();
    public EnemyState currentState;

    private Rigidbody2D rb;
    private Animator animator;

    private Transform player;
    private Transform protect;

    private EnemyAttackBase attackScript;
    private EnemyTargetor targetor;
    private EnemyMover mover;
    private EnemyAnim anim;
    private EnemyAI ai;

    private AnimationEventRelay animEvent;

    public static event Action<Enemy> OnEnemyDeadGlobal;
    public static event Action<int> OnEnemyExpGained;
    public event Action<EnemyState> OnStateChanged;


    public float currentHP;

    public void SetTargets(Transform player, Transform protect)
    {
        this.player = player;
        this.protect = protect;
    }
    
    #region IPoolable
    public void OnSpawn(Vector3 position, Quaternion rotation, Vector2 direction, float damage)
    {
        ChangeState(EnemyState.idle);
        transform.position = position;
        currentHP = stats.maxHP;

        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
        if (animEvent == null) animEvent = GetComponentInChildren<AnimationEventRelay>();

        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (protect == null) protect = GameObject.FindGameObjectWithTag("Protect").transform;

        if (targetor == null) targetor = gameObject.AddComponent<EnemyTargetor>();
        targetor.Initialize(player, protect, stats.attackRange);
        if (ai == null) ai = gameObject.AddComponent<EnemyAI>();
        ai.Initialize(this, stats, targetor);
        if (mover == null) mover = gameObject.AddComponent<EnemyMover>();
        mover.Initialize(this, targetor, rb, stats.moveSpeed);
        if (attackScript == null) GetAttackScript();
        attackScript.Initialize(this, mover, animEvent);
        if (anim == null) anim = gameObject.AddComponent<EnemyAnim>();
        anim.Initialize(this, animator);

        gameObject.SetActive(true);
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }
    #endregion

    private void GetAttackScript()
    {
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
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
        OnStateChanged?.Invoke(newState);
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        Hurt();
    }

    public void Hurt()
    {
        if (currentHP <= 0)
        {
            StartDead();
        }
        else
        {
            ChangeState(EnemyState.hurt);
        }
    }

    public void StartDead()
    {
        if(currentState == EnemyState.dead) return;
        ChangeState(EnemyState.dead);
        if (animEvent != null) StartCoroutine(DeadCoroutine());
        else Dead();
    }

    private IEnumerator DeadCoroutine()
    {
        while (!animEvent.isDead) yield return null;
        Dead();
    }

    public void Dead()
    {
        animEvent.isDead = false;

        OnEnemyExpGained?.Invoke(stats.expReward);
        OnEnemyDeadGlobal?.Invoke(this);

        EnemyPool.Instance.ReturnObject(stats.enemyName, this);
    }
}