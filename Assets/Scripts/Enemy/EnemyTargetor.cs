using System;
using UnityEngine;

public class EnemyTargetor: MonoBehaviour
{   
    private Transform player;
    private Transform protect;
    private float sqrTargetRange;

    private TargetType currentTarget = TargetType.None;

    public event Action<Transform> OnTargetChanged;

    private void Update()
    {
        CheckTarget();
    }

    public void Initialize(Transform player, Transform protect, float targetRange)
    {
        this.player = player;
        this.protect = protect;
        sqrTargetRange = targetRange * targetRange;
    }

    public void CheckTarget()
    {
        TargetType newTarget = UpdateTarget();
        if (currentTarget != newTarget)
        {
            currentTarget = newTarget;
            OnTargetChanged?.Invoke(GetTargetTransform(currentTarget));
        }
    }

    public TargetType UpdateTarget()
    {
        float playerSqr = (transform.position - player.position).sqrMagnitude;
        float protectSqr = (transform.position - protect.position).sqrMagnitude;

        if (playerSqr < protectSqr && playerSqr <= sqrTargetRange) return TargetType.Player;
        else return TargetType.Protect;
    }

    public Transform GetTargetTransform(TargetType targetType)
    {
        switch (targetType)
        {
            case TargetType.Player:
                return player;
            case TargetType.Protect:
                return protect;
            default:
                return null;
        }
    }
}
