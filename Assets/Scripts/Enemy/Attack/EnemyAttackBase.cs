using System.Collections;
using UnityEngine;

public abstract class EnemyAttackBase : MonoBehaviour
{
    protected Enemy enemy;
    protected bool isAttacking = false;
    protected bool canAttack = false;

    public virtual void Initialize(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void EnableAttack()
    {
        canAttack = true;
    }
    public void DisableAttack()
    {
        canAttack = false;
    }

    public void TryAttack(Vector2 direct, GameObject projectile, int damage)
    {
        if (!isAttacking)
        {
            StartCoroutine(AttackRoutine(direct, projectile, damage));
        }
    }

    protected virtual IEnumerator AttackRoutine(Vector2 direction, GameObject projectile, int damage)
    {
        isAttacking = true;
        Attack(direction, projectile, damage);

        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }

    public abstract void Attack(Vector2 direction, GameObject projectile, int damage);
}
