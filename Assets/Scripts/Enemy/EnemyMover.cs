using UnityEngine;

public class EnemyMover: MonoBehaviour
{
    private Enemy enemy;
    private EnemyTargetor targetor;
    private Rigidbody2D rb;
    private Transform target;
    private float moveSpeed;

    public void Initialize(Enemy enemy, EnemyTargetor targetor, Rigidbody2D rb, float moveSpeed)
    {
        this.enemy = enemy;
        this.targetor = targetor;
        this.rb = rb;
        this.moveSpeed = moveSpeed;
    }

    #region Events
    private void OnEnable()
    {
        targetor.OnTargetChanged += ChangeTarget;
        enemy.OnStateChanged += ChangeState;
    }
    private void OnDisable()
    {
        targetor.OnTargetChanged -= ChangeTarget;
        enemy.OnStateChanged -= ChangeState;
    }

    private void ChangeTarget(Transform target)
    {
        this.target = target;
    }

    private void ChangeState(EnemyState state)
    {
        if (target == null) return;
        if (state == EnemyState.idle) rb.linearVelocity = Vector2.zero;
        else if (state == EnemyState.trace)
        {
            Vector2 targetDirection = GetTargetDirection(transform, target);
            rb.linearVelocity = targetDirection * moveSpeed;
        }
    }
    #endregion

    public Vector2 GetTargetDirection(Transform transform, Transform target)
    {
        return (target.position - transform.position).normalized;
    }
}
