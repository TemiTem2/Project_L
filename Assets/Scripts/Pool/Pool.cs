using UnityEngine;
public enum PoolType
{
    Enemy,
    Projectile,
    Skill,
    Effect
}

[System.Serializable]
public class Pool<T> where T : MonoBehaviour
{
    public string tag;
    public T prefab;
    public int size;
    public int maxExpandSize;
}
