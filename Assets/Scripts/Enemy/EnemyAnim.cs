using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    private Animator enemyAnim;
    private Enemy enemy;
    private EnemyState prevState;

    private void Start()
    {
        enemyAnim = GetComponent<Animator>();
        enemy = GetComponentInParent<Enemy>();
        prevState = enemy.enemyState;
    }

    private void Update()
    {
        if (enemy.enemyState != prevState)
        {
            UpdateAnimState();
            prevState = enemy.enemyState;
        }
    }

    public void DisableAttack()
    {
        UpdateAnimState();
    }

    public void UpdateAnimState()
    {
        switch(enemy.enemyState)
        {
            case EnemyState.idle:
                break;
            case EnemyState.attack:
                enemyAnim.SetTrigger("attack");
                break;
            case EnemyState.hurt:
                enemyAnim.SetTrigger("hurt");
                break;
            case EnemyState.dead:
                enemyAnim.SetBool("isDead", true);
                this.enabled = false;
                break;
        }
    }
}
