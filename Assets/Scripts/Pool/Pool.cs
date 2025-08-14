using UnityEngine;
public enum PoolType
{
    Enemy,
    Projectile,
    Skill,
    Effect
}

[System.Serializable]
public class Pool
{
    public PoolType category;
    public string tag;
    public GameObject prefab;
    public int size;
    public int maxExpandSize;
}
