using UnityEngine;

public class TimeLife : ProjectileLifeBase
{
    private float lifeTime;
    private float timer;

    public override void Initialize(float life, Vector2 v)
    {
        lifeTime = life;
        timer = 0f;
    }


    private void CheckLife()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            LifeEnd();
        }
    }
}
