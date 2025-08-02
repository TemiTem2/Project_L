using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private StageTemplate[] stages;
    [SerializeField]
    private int currentStageIndex = 0;
    [SerializeField]
    private int totalStages;
    [SerializeField]
    private EnemySpawner enemySpawner;

    public bool isOver = false;
    public bool isWaveEnd = false;


    private StageTemplate currentStage;
    private int currentWaveIndex = 0;

    void Start()
    {
        currentStage = stages[currentStageIndex];
        StartCoroutine(RoadStage());
        isWaveEnd = true;
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
                    enemySpawner.SpawnEnemy(enemyName, new Vector3(spawnPos.x, spawnPos.y, 0));
                    yield return new WaitForSeconds(currentStage.stageData.spawnInterval);
                }
            }
            currentWaveIndex++;
        }
    }
} 
