using UnityEngine;

public enum AttackType
{
    melee,
    ranged
}

public enum EnemyState
{
    idle,
    attack,
    hurt,
    dead
}

[System.Serializable]
public class EnemyStats
{
    [Header("Enemy Stats")]
    public string enemyName;
    public int maxHP;
    public int currentHP;
    public int damage;
    public float attackSpeed;
    public float attackRange;
    public float moveSpeed;
    public AttackType attackType;
    public EnemyState enemyState;

    [Header("Rewards")]
    public int expReward;
    public int goldReward;

    [Header("Enemy Prefab")]
    public GameObject enemyPrefab;

    public void AwakeEnemy()
    {
        currentHP = maxHP;
    }
}
