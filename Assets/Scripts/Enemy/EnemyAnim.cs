using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    private Animator animator;
    private Enemy enemy;

    public void Initialize(Enemy enemy, Animator animator)
    {
        this.enemy = enemy;
        this.animator = animator;
        enemy.OnStateChanged += UpdateAnimState;
        UpdateAnimState(EnemyState.idle);
    }

    private void OnDisable()
    {
        enemy.OnStateChanged -= UpdateAnimState;
    }

    private void UpdateAnimState(EnemyState state)
    {
        if (animator == null || enemy == null) return;
        switch(state)
        {
            case EnemyState.idle:
                animator.SetBool("isDead", false);
                animator.SetBool("isRun", false);
                break;
            case EnemyState.trace:
                animator.SetBool("isRun", true);
                break;
            case EnemyState.attack:
                animator.SetBool("isRun", false);
                animator.SetTrigger("attack");
                break;
            case EnemyState.hurt:
                animator.SetBool("isRun", false);
                animator.SetTrigger("hurt");
                break;
            case EnemyState.dead:
                animator.SetBool("isRun", false);
                animator.SetBool("isDead", true);
                break;
        }
    }
}
