using System;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IPoolable
{
    public EnemyStats stats = new();
    public EnemyState currentState;

    private Rigidbody2D rb;

    private Transform player;
    private Transform protect;

    public EnemyAttackBase attackScript;
    public EnemyTargetor targetor;
    private EnemyMover mover;
    private EnemyAI ai;

    private AnimationEventRelay animEvent;

    public static event Action<Enemy> OnEnemyDeadGlobal;
    public static event Action<int> OnEnemyExpGained;
    public event Action<EnemyState> OnStateChanged;


    public float currentHP;

    private void Awake()
    {

        animEvent = GetComponentInChildren<AnimationEventRelay>();

    }
    #region IPoolable
    public void OnSpawn(Vector3 position, Quaternion rotation, Vector2 direction, float damage)
    {
        ChangeState(EnemyState.idle);
        transform.position = position;
        currentHP = stats.maxHP;

        if (rb == null) rb = GetComponent<Rigidbody2D>();

        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (protect == null) protect = GameObject.FindGameObjectWithTag("Protect").transform;

        if (targetor == null) targetor = gameObject.AddComponent<EnemyTargetor>();
        targetor.Initialize(player, protect, stats.attackRange);
        if (ai == null) ai = gameObject.AddComponent<EnemyAI>();
        ai.Initialize(this, stats, targetor);
        if (mover == null) mover = gameObject.AddComponent<EnemyMover>();
        mover.Initialize(this, targetor, rb, stats.moveSpeed);
        if (attackScript == null) GetAttackScript();
        attackScript.Initialize(this);

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
            Dead();
        }
        else
        {
            ChangeState(EnemyState.hurt);
        }
    }


    private IEnumerator DeadCoroutine()
    {
        while (!animEvent.isDead)
        {
            yield return null;
        }
        animEvent.isDead = false;
        Dead();
    }

    public void Dead()
    {
        ChangeState(EnemyState.dead);
        if (animEvent != null) StartCoroutine(DeadCoroutine());
        else Dead();
        OnEnemyExpGained?.Invoke(stats.expReward);
        OnEnemyDeadGlobal?.Invoke(this);

        EnemyPool.Instance.ReturnObject(stats.enemyName, this);
    }
}