using System.Collections.Generic;
using UnityEngine;


public class PoolManagerBase<T> : MonoBehaviour where T : MonoBehaviour, IPoolable
{
    public List<Pool<T>> pools;

    private Dictionary<string, Queue<T>> poolDictionary;
    private Dictionary<string, Transform> poolParents;
    private Dictionary<string, Pool<T>> poolSettings;

    protected virtual void Awake()
    {

        poolDictionary = new Dictionary<string, Queue<T>>();
        poolParents = new Dictionary<string, Transform>();
        poolSettings = new Dictionary<string, Pool<T>>();

        InitializePools();
    }

    private void InitializePools()
    {
        foreach (var pool in pools)
        {
            if (!poolDictionary.ContainsKey(pool.tag))
            {
                poolDictionary[pool.tag] = new Queue<T>();
                var parent = new GameObject($"{pool.tag}_Pool").transform;
                parent.SetParent(transform);
                poolParents[pool.tag] = parent;
            }
            MakeQueue(pool);
            poolSettings[pool.tag] = pool;
        }
    }

    private void MakeQueue(Pool<T> pool)
    {
        for (int i = 0; i < pool.size; i++)
        {
            var obj = Instantiate(pool.prefab, poolParents[pool.tag]);
            obj.gameObject.SetActive(false);
            poolDictionary[pool.tag].Enqueue(obj);
        }
    }


    public void GetObject(string tag, Vector3 position, Quaternion rotation, Vector2 direction, float damage)
    {
        if (!poolDictionary.ContainsKey(tag)) return;
        T obj;
        if (poolDictionary[tag].Count > 0)  obj = poolDictionary[tag].Dequeue();
        else
        {
            Pool<T> setting = GetPoolSetting(tag);
            obj = Instantiate(setting.prefab, poolParents[tag]);

            setting.size++;
            poolSettings[tag] = setting;
        }
        obj.OnSpawn(position, rotation, direction, damage);
    }

    public void ReturnObject(string tag, T obj)
    {
        obj.OnDespawn();
        obj.transform.SetParent(poolParents[tag]);
        poolDictionary[tag].Enqueue(obj);
    }

    private Pool<T> GetPoolSetting(string tag)
    {
        return poolSettings[tag];
    }
}
