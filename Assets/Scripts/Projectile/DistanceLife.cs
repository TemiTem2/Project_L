using System;
using UnityEngine;

public class DistanceLife : ProjectileLifeBase
{
    private float maxDistance;
    private Vector2 startPos;



    public override void Initialize(float life, Vector2 startPos)
    {
        maxDistance = life;
        this.startPos = startPos;
    }

    private void Update()
    {
        CheckLife();
    }

    private void CheckLife()
    {
        if (Vector2.Distance(startPos, transform.position) >= maxDistance)
        {
            LifeEnd();
        }
    }
}
