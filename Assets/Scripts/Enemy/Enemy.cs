using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStats stats = new EnemyStats();

    void Start()
    {
        stats.AwakeEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
