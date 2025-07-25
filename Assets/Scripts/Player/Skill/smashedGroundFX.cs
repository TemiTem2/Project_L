using UnityEngine;

public class smashedGroundFX : MonoBehaviour
{
    public Vector2 moveDir;
    public float moveSpeed = 10f;
    public float lifeTime = 2f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)(moveDir * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            // 적에게 데미지 적용
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(StateManager.skill.damage);
            }
        }
    }
}
