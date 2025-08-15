using System;
using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    public bool isAttacked = false;
    public bool isDead = false;

    public event Action<bool> OnAnimationAttack;
    public event Action OnAnimationProjectile;

    public void CreateProjectile()
    {
        OnAnimationProjectile?.Invoke();
    }

    public void EnableAttack()
    {
        OnAnimationAttack?.Invoke(true);
    }
    public void DisableAttack()
    {
        OnAnimationAttack?.Invoke(false);
    }

    public void CompleteDead()
    {
        isDead = true;
    }
}
