using System;
using UnityEngine;

public class ProjectileLifeBase : MonoBehaviour
{
    public event Action OnLifeEnd;

    public virtual void Initialize(float life, Vector2 startPos)
    { }

    protected void LifeEnd()
    {
        OnLifeEnd.Invoke();
    }
}
