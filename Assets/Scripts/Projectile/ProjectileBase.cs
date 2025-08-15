using System;
using UnityEngine;
using System.Collections;

public enum ProjState
{
    None,
    Idle,
    Hit
}
public class ProjectileBase : MonoBehaviour, IPoolable
{
    [SerializeField] protected ProjectileStats stats;

    private Animator animator;
    private ProjectileAnim anim;
    private ProjectileMoveBase move;
    private ProjectileLifeBase life;
    private AnimationEventRelay animEvent;

    public static event Action<float> OnEnemyProjectileHitPlayer;
    public static event Action<float> OnEnemyProjectileHitProtect;
    public event Action<ProjState> OnProjStateChange;

    private ProjState state;
    private Vector2 direction;
    private Rigidbody2D rb;
    private float lifeFloat;

    private float damage;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (stats.haveAnim)
        {
            animator = GetComponent<Animator>();
            anim = gameObject.AddComponent<ProjectileAnim>();
            animEvent = GetComponent<AnimationEventRelay>();
        }
        AddMove();
        AddLife();
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

    private void AddMove()
    {
        switch(stats.moveType)
        {
            case (MoveType.DirectProjectile):
                move = gameObject.AddComponent<DirectProjectile>();
                break;
            case (MoveType.Laser):
                break;
        }
    }
    private void AddLife()
    {
        switch(stats.lifeType)
        {
            case (LifeType.Distance):
                life = gameObject.AddComponent<DistanceLife>();
                lifeFloat = stats.maxDistance;
                return;
            case (LifeType.Time):
                life = gameObject.AddComponent<TimeLife>();
                lifeFloat = stats.lifetime;
                return;
        }
    }

    #region IPoolable
    public void OnSpawn(Vector3 position, Quaternion rotation, Vector2 dir, float dam)
    {
        state = ProjState.None;
        ChangeState(ProjState.Idle);
        transform.position = position;
        transform.rotation = rotation;
        direction = dir;
        damage = dam;
        if (stats.haveAnim && animator != null) anim.Initialize(animator, this);
        move.Initialize(direction, rb, stats.speed);
        life.Initialize(lifeFloat, position);
        life.OnLifeEnd += StartHit;
        gameObject.SetActive(true);

    }
    public void OnDespawn()
    {
        rb.linearVelocity = Vector2.zero;
        life.OnLifeEnd -= StartHit;
        gameObject.SetActive(false);
    }
    #endregion

    private void ChangeState(ProjState newState)
    {
        if (state != newState && state != ProjState.Hit)
        {
            state = newState;
            OnProjStateChange?.Invoke(newState);
        }
    }

    public void StartHit()
    {
        ChangeState(ProjState.Hit);
        if (animEvent != null) StartCoroutine(HitCoroutine());
        else ReturnToPool();
    }

    private IEnumerator HitCoroutine()
    {
        while (!animEvent.isDead) yield return null;
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        animEvent.isDead = false;
        ProjectilePool.Instance.ReturnObject(stats.projectileName, this);
    }
    protected void OnHit()
    {
        PlayHitEffect();
        StartHit();
    }

    protected void PlayHitEffect()
    {
        if (stats.hitEffectName != null)
        {
            EffectPool.Instance.GetObject(stats.hitEffectName, transform.position, Quaternion.identity, Vector2.zero, 0);
        }
        //play hit sound
    }

}
