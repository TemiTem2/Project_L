using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private StageTemplate[] stages;
    [SerializeField]
    private int currentStageIndex = 2;
    [SerializeField]
    private int totalStages;
    [SerializeField]
    private EnemySpawner enemySpawner;

    private List<Enemy> enemies = new();

    public bool isOver = false;
    public bool isStageEnd = false;


    private StageTemplate currentStage;
    private int currentWaveIndex = 0;

    void Start()
    {
        isStageEnd = false;
        isOver = false;
        currentStage = stages[currentStageIndex];
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
                    Vector2 spawnPos = enemySpawner.GenerateSpwanPos();
                    Enemy enemy = enemySpawner.SpawnEnemy(enemyName, new Vector3(spawnPos.x, spawnPos.y, 0));
                    RegisterEnemy(enemy);
                    yield return new WaitForSeconds(currentStage.stageData.spawnInterval);
                }
            }
            currentWaveIndex++;
        }
    }

    private void RegisterEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
        enemy.OnDeathCallBack = UnregisterEnemy;
    }

    private void UnregisterEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0)
        {
            isStageEnd = true;
        }
    }
} 
