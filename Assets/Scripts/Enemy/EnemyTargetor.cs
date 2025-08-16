using System;
using UnityEngine;

public class EnemyTargetor: MonoBehaviour
{   
    private Transform player;
    private Transform protect;
    private float sqrTargetRange;

    private TargetType currentTarget = TargetType.None;

    public event Action<Transform> OnTargetChanged;
    public event Action<bool> OnTargetInRange;

    private void Update()
    {
        CheckTarget();
    }

    public void Initialize(Transform player, Transform protect, float targetRange)
    {
        this.player = player;
        this.protect = protect;
        sqrTargetRange = targetRange * targetRange;
        currentTarget = TargetType.None;
    }

    private void CheckTarget()
    {
        TargetType newTarget = UpdateTarget();
        if (currentTarget != newTarget)
        {
            currentTarget = newTarget;
            OnTargetChanged?.Invoke(GetTargetTransform(currentTarget));
        }
    }

    private void CheckRange(float playerSqr, float protectSqr)
    {
        OnTargetInRange?.Invoke(sqrTargetRange >= playerSqr || sqrTargetRange >= protectSqr);
    }

    private TargetType UpdateTarget()
    {
        float playerSqr = (transform.position - player.position).sqrMagnitude;
        float protectSqr = (transform.position - protect.position).sqrMagnitude;

        CheckRange(playerSqr, protectSqr);

        if (playerSqr < protectSqr && playerSqr <= sqrTargetRange) return TargetType.Player;
        else return TargetType.Protect;
    }

    private Transform GetTargetTransform(TargetType targetType)
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
