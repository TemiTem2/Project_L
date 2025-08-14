using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private StageTemplate[] stages;
    [SerializeField]
    private int currentStageIndex = 1;
    [SerializeField]
    private int totalStages;
    [SerializeField]
    private EnemySpawner enemySpawner;

    private List<Enemy> enemies = new();

    public bool isOver = false;
    public bool isStageEnd = false;

    private int totalEnemiesToSpawn;
    private int enemiesDefeated;


    private StageTemplate currentStage;
    private int currentWaveIndex = 0;

    void Start()
    {
        isStageEnd = false;
        isOver = false;
        currentStage = stages[currentStageIndex];
        totalEnemiesToSpawn = currentStage.stageData.totalEnemies;
        StartCoroutine(RoadStage());
    }

    private void Update()
    {
        if (isStageEnd)
        {
            GameManager.Instance.ChangeState(GameState.Day);
        }
        else if (isOver)
        {
            GameManager.Instance.ChangeState(GameState.GameOver);
        }
    }

    private IEnumerator RoadStage()
    {
        while (currentWaveIndex <= currentStage.stageData.maxWave-1)
        {
            StageTemplate.WaveData wave = currentStage.waves[currentWaveIndex];
            for (int i = 0; i < wave.enemyNames.Length; i++)
            {
                string enemyName = wave.enemyNames[i];
                int enemyCount = wave.enemyCounts[i];
                for (int j = 0; j < enemyCount; j++)
                {
                    enemySpawner.SpawnEnemy(enemyName);
                    yield return new WaitForSeconds(currentStage.stageData.spawnInterval);
                }
            }
            currentWaveIndex++;
        }
    }


    private void OnEnable()
    {
        Enemy.OnEnemyDeadGlobal += HandleEnemyDead;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDeadGlobal -= HandleEnemyDead;
    }

    private void HandleEnemyDead(Enemy enemy)
    {
        enemiesDefeated++;
        if (enemiesDefeated >= totalEnemiesToSpawn)
            isStageEnd = true;
    }
} 
