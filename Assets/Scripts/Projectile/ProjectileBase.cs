using System;
using UnityEngine;

public class ProjectileBase : MonoBehaviour, IPoolable
{
    [SerializeField] protected ProjectileStats stats;

    protected Animator animator;
    protected ProjectileAnim anim;

    public static event Action<float> OnEnemyProjectileHitPlayer;
    public static event Action<float> OnEnemyProjectileHitProtect;


    protected Vector2 direction;
    protected Vector2 startPos;
    protected Rigidbody2D rb;
    protected float timer = 0f;
    private bool isReturned = false;

    private float damage;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (stats.haveAnim)
        {
            animator = GetComponent<Animator>();
            anim = gameObject.AddComponent<ProjectileAnim>();
        }
    }

    void Update()
    {
        CheckLife();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnEnemyProjectileHitPlayer?.Invoke(damage);
            OnHit();
        }
        else if (collision.CompareTag("Protect"))
        {
            OnEnemyProjectileHitProtect?.Invoke(damage);
            OnHit();
        }
    }

    public void OnSpawn(Vector3 position, Quaternion rotation, Vector2 dir, float dam)
    {
        ResetProjectile(position, rotation, dir, dam);
        Move();
    }
    private void ResetProjectile(Vector3 position, Quaternion rotation, Vector2 dir, float dam)
    {
        transform.position = position;
        transform.rotation = rotation;
        timer = 0f;
        startPos = position;
        direction = dir;
        damage = dam;
        isReturned = false;
        rb.linearVelocity = Vector2.zero;
        if (stats.haveAnim && animator != null) anim.Initialize(animator);
        gameObject.SetActive(true);
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
                CheckDistanceLife();
                break;
            case LifeType.Time:
                CheckTimeLife();
                break;
            default:
                Debug.LogWarning("LifeType error");
                break;
        }
    }

    private void CheckDistanceLife()
    {
        if (Vector2.Distance(startPos, transform.position) >= stats.maxDistance)
        {
            ReturnToPool();
        }
    }
    private void CheckTimeLife()
    {
        timer += Time.deltaTime;
        if (timer >= stats.lifetime)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        if (isReturned) return;
        isReturned = true;
        ProjectilePool.Instance.ReturnObject(stats.projectileName, this);
    }
    protected virtual void OnHit()
    {
        PlayHitEffect();
        ReturnToPool();
    }

    protected virtual void PlayHitEffect()
    {
        if (stats.hitEffectName != null)
        {
            EffectPool.Instance.GetObject(stats.hitEffectName, transform.position, Quaternion.identity, Vector2.zero, 0);
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
