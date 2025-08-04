using UnityEngine;
using UnityEngine.Rendering;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField]
    protected ProjectileStats stats = new();


    protected Vector2 startPos;
    protected Rigidbody2D rb;
    protected float timer = 0f;

    protected virtual void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        Move();
    }
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

    private void CheckLife()
    { 
        switch(stats.lifeType)
        {
            case LifeType.Distance:
                if (Vector2.Distance(startPos, transform.position) >= stats.maxDistance)
                {
                    Destroy(gameObject);
                }
                break;
            case LifeType.Time:
                timer += Time.deltaTime;
                if (timer >= stats.lifetime)
                {
                    Destroy(gameObject);
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
        Destroy(gameObject);
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
}
