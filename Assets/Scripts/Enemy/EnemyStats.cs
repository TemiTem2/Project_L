using UnityEngine;

public enum AttackType
{
    melee,
    ranged
}

[Tooltip("Collider√ﬂ∞°« ")]
[System.Serializable]
public class EnemyStats
{
    [Header("Enemy Stats")]
    public string enemyName;
    public float maxHP;
    public float damage;
    public float attackSpeed;
    public float attackRange;
    public float moveSpeed;
    public AttackType attackType;
    public string projectileName;

    [Header("Rewards")]
    public int expReward;
    public int goldReward;

    [Header("Enemy Prefab")]
    public GameObject enemyPrefab;
    public GameObject projectilePrefab;
}
