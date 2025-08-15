using UnityEngine;

public class SkillBase : MonoBehaviour, IPoolable
{
    [SerializeField] private Skill stats;

    public void OnSpawn(Vector3 position, Quaternion rotation, Vector2 direction, float f)
    {

    }
    public void OnDespawn()
    {

    }
}
