using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    [Header("풀 설정")]
    public List<Pool> pools;

    private Dictionary<PoolType, Dictionary<string, Queue<GameObject>>> poolDictionary;
    private Dictionary<(PoolType, string), GameObject> poolParents;
    private Dictionary<(PoolType, string), Pool> poolSettings;

    void Awake()
    {
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

            // 오브젝트 미리 생성
            var objectPool = poolDictionary[pool.category][pool.tag];
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, poolParents[(pool.category, pool.tag)].transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
        }
    }

    public GameObject GetObject(PoolType category, string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(category) || !poolDictionary[category].ContainsKey(tag))
        {
            Debug.LogWarning($"풀에 없음: {category}/{tag}");
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
                Debug.LogWarning($"{category}/{tag} 최대 풀 크기 초과");
                return null;
            }

            obj = Instantiate(setting.prefab, poolParents[(category, tag)].transform);
        }

        obj.SetActive(true);
        obj.transform.SetPositionAndRotation(position, rotation);

        if (obj.TryGetComponent<IPoolable>(out var poolable))
            poolable.OnSpawn();

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

        obj.SetActive(false);
        obj.transform.SetParent(poolParents[(category, tag)].transform);
        poolDictionary[category][tag].Enqueue(obj);
    }
}
