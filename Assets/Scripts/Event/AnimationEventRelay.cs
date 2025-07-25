using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    public bool canAttack = false;

    public void EnableAttack()
    {
        canAttack = true;
    }
    public void DisableAttack()
    {
        canAttack = false;
    }
}
