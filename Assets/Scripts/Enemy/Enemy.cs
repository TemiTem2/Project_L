using UnityEngine;

public enum EnemyState
{
    idle,
    attack,
    hurt,
    dead
}
public class Enemy : MonoBehaviour
{
    public EnemyStats stats = new EnemyStats();
    public EnemyState enemyState;
    public EnemyAttackBase attackScript;
    public float currentHP;

    private void Start()
    {
        currentHP = stats.maxHP;

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

        attackScript.Initialize(this);
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
        LevelupManager levelupManager = FindFirstObjectByType<LevelupManager>();
        levelupManager.AddExp(stats.expReward);
        Destroy(gameObject, 3f);
    }
}