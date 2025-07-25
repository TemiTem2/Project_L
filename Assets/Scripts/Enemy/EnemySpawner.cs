using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Enemy[] enemies;

    private Dictionary<string, Enemy> enemyDictionary = new Dictionary<string, Enemy>();

    private void Awake()
    {
        GenerateDict();
    }

    private void GenerateDict()
    {
        enemyDictionary.Clear();
        foreach (Enemy enemy in enemies)
        {
            if (!enemyDictionary.ContainsKey(enemy.stats.enemyName))
            {
                enemyDictionary.Add(enemy.stats.enemyName, enemy);
            }
            else
            {
                Debug.LogWarning("enemyName 중복: " + enemy.stats.enemyName);
            }
        }
    }

    public void SpawnEnemy(string name, Vector3 position)
    {
        if (enemyDictionary.TryGetValue(name, out Enemy enemyPrefab))
        {
            Enemy newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("해당 이름 없음: " + name);
        }
    }

    private void GenerateSpwanPos()
    {

    }
}
