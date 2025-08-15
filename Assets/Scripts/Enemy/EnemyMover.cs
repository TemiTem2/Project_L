using UnityEngine;

public class EnemyMover: MonoBehaviour
{
    private Enemy enemy;
    private EnemyTargetor targetor;
    private Rigidbody2D rb;
    private Transform target;
    private float moveSpeed;

    public event System.Action<Vector2> OnDirectionUpdated;

    public void Initialize(Enemy enemy, EnemyTargetor targetor, Rigidbody2D rb, float moveSpeed)
    {
        this.enemy = enemy;
        this.targetor = targetor;
        this.rb = rb;
        this.moveSpeed = moveSpeed;

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
        Vector2 targetDirection = GetTargetDirection(transform, target);
        if (state == EnemyState.trace) rb.linearVelocity = targetDirection * moveSpeed;
        else rb.linearVelocity = Vector2.zero;

        if (targetDirection.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (targetDirection.x > 0 ? 1 : -1);
            transform.localScale = scale;
        }

        OnDirectionUpdated?.Invoke(targetDirection);
    }

    public Vector2 GetTargetDirection(Transform transform, Transform target)
    {
        return (target.position - transform.position).normalized;
    }
}
