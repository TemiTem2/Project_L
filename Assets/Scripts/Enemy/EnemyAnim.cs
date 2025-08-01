using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    private Animator enemyAnim;
    private Enemy enemy;

    private void Start()
    {
        enemyAnim = GetComponentInChildren<Animator>();
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        UpdateAnimState();
    }

    private void UpdateAnimState()
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
                break;
        }
    }
}
