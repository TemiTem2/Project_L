using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    private Animator animator;
    private Enemy enemy;

    public void Initialize(Enemy enemy, Animator animator)
    {
        this.enemy = enemy;
        this.animator = animator;
        UpdateAnimState(EnemyState.idle);
    }

    private void OnEnable()
    {
        enemy.OnStateChanged += UpdateAnimState;
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
                break;
            case EnemyState.trace:
                animator.SetTrigger("run");
                break;
            case EnemyState.attack:
                animator.SetTrigger("attack");
                break;
            case EnemyState.hurt:
                animator.SetTrigger("hurt");
                break;
            case EnemyState.dead:
                animator.SetBool("isDead", true);
                break;
        }
    }
}
