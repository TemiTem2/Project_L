using UnityEngine;

public class smashedGroundFX : MonoBehaviour
{
    public Vector2 moveDir;
    public float moveSpeed = 10f;
    public float lifeTime = 2f;
    private StateManager stateManager;
    void Start()
    {
        stateManager = FindFirstObjectByType<StateManager>();
        Destroy(gameObject, lifeTime);
    }

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
                enemy.TakeDamage(stateManager.skill.damage);
            }
        }
    }
}
