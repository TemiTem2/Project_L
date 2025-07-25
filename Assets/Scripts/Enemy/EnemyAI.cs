using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public EnemyStats enemyStats;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
