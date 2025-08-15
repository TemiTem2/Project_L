using UnityEngine;

public class ProjectileAnim : MonoBehaviour
{
    private Animator animator;
    private ProjectileBase projectile;

    public void Initialize(Animator animator, ProjectileBase projectile)
    {
        this.animator = animator;
        this.projectile = projectile;
        animator.SetBool("isHit", false);
    }

    private void OnEnable()
    {
        if (projectile != null)  projectile.OnProjStateChange += CheckHit;
    }
    private void OnDiasble()
    {
        if (projectile != null)  projectile.OnProjStateChange -= CheckHit;
    }

    private void CheckHit(ProjState state)
    {
        if (state == ProjState.Hit) animator.SetBool("isHit", true);
    }

}
