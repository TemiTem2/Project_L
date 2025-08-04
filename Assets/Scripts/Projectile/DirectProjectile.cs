using UnityEngine;

public class DirectProjectile : ProjectileBase
{
    protected override void Move()
    {
        if (rb != null)
        {
            rb.linearVelocity = transform.position * stats.speed;
        }
        else
        {
            Debug.LogWarning("Rigidbody2D ¾øÀ½");
        }
    }
}
