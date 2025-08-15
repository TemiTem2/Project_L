using UnityEngine;

public class DirectProjectile : ProjectileMoveBase
{
    private float speed;

    public override void Initialize(ProjectileBase projectile, Vector2 dir, Rigidbody2D rb, float speed)
    {
        base.Initialize(projectile, dir, rb, speed);
        this.speed = speed;
        Move(dir.normalized);
    }


    private void Move(Vector2 direction)
    {
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
    }
}
