using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] stages;
    [SerializeField]
    private int currentStageIndex = 0;
    [SerializeField]
    private int totalStages;
    [SerializeField]
    private EnemySpawner enemySpawner;

    void Start()
    {
        enemySpawner.SpawnEnemy("SlimeBlue", new Vector3(0, 0, 0));    //test
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
