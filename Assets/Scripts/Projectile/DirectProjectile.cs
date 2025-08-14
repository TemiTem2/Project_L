using UnityEngine;

public class DirectProjectile : ProjectileBase
{
    public override void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    protected override void Move()
    {
        if (rb != null)
        {
            rb.linearVelocity = direction * stats.speed;
            Debug.Log($"Direction: {direction}, rb.velocity: {rb.linearVelocity}");
        }
    }
}
