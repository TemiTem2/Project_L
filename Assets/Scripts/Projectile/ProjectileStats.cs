using UnityEngine;

public enum LifeType
{
    Distance,
    Time
}
public enum MoveType
{
    DirectProjectile,
    Laser
}
[CreateAssetMenu(fileName = "ProjectileStats", menuName = "Projectile/ProjectileStats")]
public class ProjectileStats: ScriptableObject
{
    [Header("Projectile Stats")]
    public string projectileName;
    public LifeType lifeType;
    public MoveType moveType;
    public float speed;
    public float maxDistance;
    public float lifetime;
    public bool haveAnim = false;
    public string hitEffectName;
    public AudioClip hitSound;
}
