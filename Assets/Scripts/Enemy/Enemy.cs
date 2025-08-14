using System;
using UnityEngine;

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

    }

    public void OnSpawn()
    {
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
            Dead();
        }
        else
        {
            enemyState = EnemyState.hurt;
        }
    }

    public void Dead()
    {
        enemyState = EnemyState.dead;
        OnEnemyExpGained?.Invoke(stats.expReward);
        OnEnemyDeadGlobal?.Invoke(this);

        PoolManager.Instance.ReturnObject(PoolType.Enemy, stats.enemyName, gameObject);
    }
}