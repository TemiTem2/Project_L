using UnityEngine;

public class DirectProjectile : ProjectileMoveBase
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private float speed;

    public override void Initialize(Vector2 dir, Rigidbody2D rb, float speed)
    {
        direction = dir.normalized;
        this.rb = rb;
        this.speed = speed;
    }


    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
    }
}
