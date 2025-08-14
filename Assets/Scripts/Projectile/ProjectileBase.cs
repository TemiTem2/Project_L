using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileBase : MonoBehaviour, IPoolable
{
    [SerializeField]
    protected ProjectileStats stats;


    protected Vector2 direction;
    protected Vector2 startPos;
    protected Rigidbody2D rb;
    protected float timer = 0f;

    void FixedUpdate()
    {
        CheckLife();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Protect"))
        {
            OnHit(collision.gameObject);
        }
    }

    public void OnSpawn(Vector3 position, Quaternion rotation, Vector2 dir, float damage)
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        transform.position = position;
        transform.rotation = rotation;
        timer = 0f;
        startPos = position;
        direction = dir;
        gameObject.SetActive(true);
        Move();
    }

    public void OnDespawn()
    {
        rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void CheckLife()
    { 
        switch(stats.lifeType)
        {
            case LifeType.Distance:
                if (Vector2.Distance(startPos, transform.position) >= stats.maxDistance)
                {
                    ProjectilePool.Instance.ReturnObject(stats.projectileName, this);
                }
                break;
            case LifeType.Time:
                timer += Time.deltaTime;
                if (timer >= stats.lifetime)
                {
                    ProjectilePool.Instance.ReturnObject(stats.projectileName, this);
                }
                break;
            default:
                Debug.LogWarning("LifeType error");
                break;
        }
    }

    protected virtual void OnHit(GameObject target)
    {
        Debug.Log("Hit: " + target.name);
        PlayHitEffect();
        ProjectilePool.Instance.ReturnObject(stats.projectileName, this);
    }

    protected virtual void PlayHitEffect()
    {
        if (stats.hitEffect != null)
        {
            Instantiate(stats.hitEffect, transform.position, Quaternion.identity);
        }
        //play hit sound
    }

    protected virtual void Move()
    {

    }

    public virtual void SetDirection(Vector2 dir)
    {
    }
}
