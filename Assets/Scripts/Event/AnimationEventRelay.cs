using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    public bool canAttack = false;
    public bool isAttacked = false;
    public bool isDead = false;

    public void EnableAttack()
    {
        canAttack = true;
    }
    public void DisableAttack()
    {
        canAttack = false;
        isAttacked = false;
    }

    public void CompleteDead()
    {
        isDead = true;
    }
}
