using UnityEngine;

public class ProjectileAnim : MonoBehaviour
{
    private Animator animator;

    public void Initialize(Animator animator)
    {
        this.animator = animator;
        animator.SetBool("isHit", false);
    }

    private void PlayHit()
    {
        animator.SetBool("isHit", true);
    }

}
