using UnityEngine;
using UnityEngine.Rendering;

public class ProjectileBase : MonoBehaviour
{
    [Header("Projectile Stats")]
    public float speed;
    public float maxDistance;
    public int lifetime;

    protected Vector2 direction;
    protected Vector2 startPos;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
        else
        {
            Debug.LogWarning("Rigidbody2D ¾øÀ½");
        }
    }
}
