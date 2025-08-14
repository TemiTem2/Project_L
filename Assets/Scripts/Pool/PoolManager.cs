using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    public List<Pool> pools;

    private Dictionary<PoolType, Dictionary<string, Queue<GameObject>>> poolDictionary;
    private Dictionary<(PoolType, string), GameObject> poolParents;
    private Dictionary<(PoolType, string), Pool> poolSettings;

    void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion

        poolDictionary = new Dictionary<PoolType, Dictionary<string, Queue<GameObject>>>();
        poolParents = new Dictionary<(PoolType, string), GameObject>();
        poolSettings = new Dictionary<(PoolType, string), Pool>();

        InitializePools();
    }

    private void InitializePools()
    {
        foreach (var pool in pools)
        {
            if (!poolDictionary.ContainsKey(pool.category))
                poolDictionary[pool.category] = new Dictionary<string, Queue<GameObject>>();

            if (!poolDictionary[pool.category].ContainsKey(pool.tag))
            {
                poolDictionary[pool.category][pool.tag] = new Queue<GameObject>();

                GameObject cateParent = GameObject.Find(pool.category + "_Pool") ?? new GameObject(pool.category + "_Pool");
                cateParent.transform.SetParent(transform);

                GameObject poolParent = new GameObject(pool.tag + "_Pool");
                poolParent.transform.SetParent(cateParent.transform);

                poolParents[(pool.category, pool.tag)] = poolParent;
                poolSettings[(pool.category, pool.tag)] = pool;
            }
            Make_Queue(pool);
        }
    }

    private void Make_Queue(Pool pool)
    {
        var objectPool = poolDictionary[pool.category][pool.tag];
        for (int i = 0; i < pool.size; i++)
        {
            GameObject obj = Instantiate(pool.prefab, poolParents[(pool.category, pool.tag)].transform);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }


    public GameObject GetObject(PoolType category, string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(category) || !poolDictionary[category].ContainsKey(tag))
        {
            Debug.LogWarning($"Ǯ�� ����: {category}/{tag}");
            return null;
        }

        Queue<GameObject> objectPool = poolDictionary[category][tag];
        GameObject obj;

        if (objectPool.Count > 0)
        {
            obj = objectPool.Dequeue();
        }
        else
        {
            Pool setting = poolSettings[(category, tag)];
            int currentCount = poolParents[(category, tag)].transform.childCount;

            if (setting.maxExpandSize > 0 && currentCount >= setting.maxExpandSize)
            {
                Debug.LogWarning($"{category}/{tag} �ִ� Ǯ ũ�� �ʰ�");
                return null;
            }

            obj = Instantiate(setting.prefab, poolParents[(category, tag)].transform);
        }


        if (obj.TryGetComponent<IPoolable>(out var poolable))
            poolable.OnSpawn();
        obj.transform.SetPositionAndRotation(position, rotation);

        return obj;
    }

    public void ReturnObject(PoolType category, string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(category) || !poolDictionary[category].ContainsKey(tag))
        {
            Destroy(obj);
            return;
        }

        if (obj.TryGetComponent<IPoolable>(out var poolable))
            poolable.OnDespawn();
        obj.transform.SetParent(poolParents[(category, tag)].transform);

        poolDictionary[category][tag].Enqueue(obj);
    }
}
