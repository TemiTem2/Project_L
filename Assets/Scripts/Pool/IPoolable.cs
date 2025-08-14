using UnityEngine;

public interface IPoolable
{
    void OnSpawn(Vector3 position, Quaternion rotation, Vector2 direction, float damage);
    void OnDespawn();
}
