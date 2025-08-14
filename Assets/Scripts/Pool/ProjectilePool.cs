using UnityEngine;

public class ProjectilePool : PoolManagerBase<ProjectileBase>
{
    public static ProjectilePool Instance { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
        #endregion
    }
}

