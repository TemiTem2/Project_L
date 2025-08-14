using UnityEngine;

[CreateAssetMenu(fileName = "StageTemplate", menuName = "Stage/StageTemplate")]
public class StageTemplate : ScriptableObject
{
    [System.Serializable]
    public class StageData
    {
        public int stageNumber;
        public int maxWave;
        public int totalEnemies;
        public float spawnInterval;
    }
    [System.Serializable]
    public class WaveData
    {
        public int waveNumber;
        public string[] enemyNames;
        public int[] enemyCounts;
    }

    public StageData stageData;
    public WaveData[] waves;

}
