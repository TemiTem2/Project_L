using System;
using UnityEngine;

public class MeleeAttack : EnemyAttackBase
{
    private bool isAttacking = false;

    public static event Action<float> OnPlayerAttacked;
    public static event Action<float> OnProtectAttacked;

    #region Events
    protected override void OnEnable()
    {
        base.OnEnable();
        animEvent.OnAnimationAttack += CheckAttack;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        animEvent.OnAnimationAttack -= CheckAttack;
    }
    private void CheckAttack(bool isAttacking)
    {
        this.isAttacking = isAttacking;
    }
    #endregion

    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollide(collision);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        CheckCollide(other);
    }
    #endregion

    private void CheckCollide(Collider2D other)
    {
        if (!isAttacking || haveAttacked) return;

        if (other.CompareTag("Player"))
        {
            OnPlayerAttacked?.Invoke(damage);
            haveAttacked = true;
        }
        else if (other.CompareTag("Protect"))
        {
            OnProtectAttacked?.Invoke(damage);
            haveAttacked = true;
        }
    }
}
