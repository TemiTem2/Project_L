using UnityEngine;

public enum LifeType
{
    Distance,
    Time
}
[CreateAssetMenu(fileName = "ProjectileStats", menuName = "Projectile/ProjectileStats")]
public class ProjectileStats: ScriptableObject
{
    [Header("Projectile Stats")]
    public string projectileName;
    public LifeType lifeType;
    public float speed;
    public float maxDistance;
    public float lifetime;
    public bool haveAnim;
    public string hitEffectName;
    public AudioClip hitSound;
}
