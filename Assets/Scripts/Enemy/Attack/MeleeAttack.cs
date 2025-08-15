using System;
using UnityEngine;

public class MeleeAttack : EnemyAttackBase
{
    private bool isAttacking = false;
    private bool isPlayer = false;
    private bool isProtect = false;

    public static event Action<float> OnPlayerAttacked;
    public static event Action<float> OnProtectAttacked;

    #region Events
    public override void Initialize(Enemy enemy, EnemyMover mover, AnimationEventRelay animEvent)
    {
        base.Initialize(enemy, mover, animEvent);
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
        if (!isAttacking)
        {
            haveAttacked = false;
        }
    }
    #endregion

    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isPlayer = true;
        else if (collision.CompareTag("Protect")) isProtect = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) isPlayer = false;
        else if (collision.CompareTag("Protect")) isProtect = false;
    }
    #endregion

    private void Update()
    {
        if (!isAttacking || haveAttacked) return;
        if (isPlayer)
        {
            OnPlayerAttacked?.Invoke(damage);
            haveAttacked = true;
        }
        else if (isProtect)
        {
            OnProtectAttacked?.Invoke(damage);
            haveAttacked = true;
        }
    }
}
