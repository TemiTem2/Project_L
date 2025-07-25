using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStats stats = new EnemyStats();
    private Animator enemyAnim;
    private AttackType attackType;

    public float currentHP;


    void Start()
    {
        currentHP = stats.maxHP;
        enemyAnim = GetComponentInChildren<Animator>();
        attackType = stats.attackType;
    }

    void Update()
    {
        if (stats.enemyState == EnemyState.attack)
        {
            //AttackToPlayer();
            enemyAnim.SetTrigger("attack");
            stats.enemyState = EnemyState.idle;
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
        Destroy(gameObject, 2f);
    }
}
