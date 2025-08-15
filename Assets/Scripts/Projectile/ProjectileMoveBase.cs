using UnityEngine;

public class ProjectileMoveBase : MonoBehaviour
{
    private ProjectileBase projectile;
    protected Rigidbody2D rb;

    protected void OnDisable()
    {
        if (projectile != null)  projectile.OnProjStateChange -= CheckState;
    }

    private void CheckState(ProjState state)
    {
        if (state == ProjState.Dead) StopMove();
    }
    private void StopMove()
    {
        rb.linearVelocity = Vector3.zero;
    }

    public virtual void Initialize(ProjectileBase projectile, Vector2 dir, Rigidbody2D rb, float speed)
    { 
        projectile.OnProjStateChange += CheckState;
        this.projectile = projectile;
        this.rb = rb;
    }
}
