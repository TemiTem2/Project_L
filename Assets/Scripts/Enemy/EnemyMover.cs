using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMover
{
    private Rigidbody2D rb;

    public void InitializeEnemyMover(Enemy enemy)
    {
        rb = enemy.GetComponent<Rigidbody2D>();
    }

    public bool IsTargetInRange(Transform self, Transform target, float attackRange)
    {
        float distance = Vector2.Distance(self.position, target.position);
        return distance <= attackRange;
    }

    public Vector2 GetTargetDirection(Transform transform, Transform target)
    {
        return (target.position - transform.position).normalized;
    }

    public void MoveEnemy(Vector2 targetDirection, float moveSpeed)
    {
        rb.MovePosition(rb.position + targetDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
