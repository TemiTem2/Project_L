using UnityEngine;

public class SkillBase : ProjectileBase
{

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            // ������ ������ ����
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    protected override void OnHit()
    { }

    protected override void ReturnToPool()
    {
        if (stats.haveAnim) animEvent.isDead = false;
        SkillPool.Instance.ReturnObject(stats.projectileName, this);
    }
}
