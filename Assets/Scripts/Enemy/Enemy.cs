using System;
using UnityEngine;
using System.Collections;

public enum EnemyState
{
    idle,
    attack,
    hurt,
    dead
}
public class Enemy : MonoBehaviour, IPoolable
{
    public EnemyStats stats = new();
    public EnemyState enemyState;
    public EnemyAttackBase attackScript;

    private AnimationEventRelay animEvent;

    public static event Action<Enemy> OnEnemyDeadGlobal;
    public static event Action<int> OnEnemyExpGained;


    public float currentHP;

    private void Awake()
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
        animEvent = GetComponentInChildren<AnimationEventRelay>();

    }

    public void OnSpawn(Vector3 position, Quaternion rotation, Vector2 direction, float damage)
    {
        enemyState = EnemyState.idle;
        transform.position = position;
        currentHP = stats.maxHP;
        attackScript.Initialize(this);
        gameObject.SetActive(true);
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
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
            PlayDead();
        }
        else
        {
            enemyState = EnemyState.hurt;
        }
    }

    public void PlayDead()
    {
        enemyState = EnemyState.dead;
        if (animEvent != null) StartCoroutine(DeadCoroutine());
        else Dead();
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
        OnEnemyExpGained?.Invoke(stats.expReward);
        OnEnemyDeadGlobal?.Invoke(this);

        EnemyPool.Instance.ReturnObject(stats.enemyName, this);
    }
}