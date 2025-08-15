using UnityEngine;

public class SkillBase : ProjectileBase
{

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            // 적에게 데미지 적용
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    protected override void OnHit()
    { }
}
