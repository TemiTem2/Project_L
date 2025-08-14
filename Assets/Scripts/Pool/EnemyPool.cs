using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : PoolManagerBase<Enemy>
{
    public static EnemyPool Instance { get; private set; }

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
