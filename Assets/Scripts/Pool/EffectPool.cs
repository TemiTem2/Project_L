

public class EffectPool : PoolManagerBase<Effect>
{
    public static EffectPool Instance { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(transform.root.gameObject);
        Instance = this;
        #endregion
    }
}
