using UnityEngine;

public class ProtectedTarget : MonoBehaviour
{
    [System.Serializable]
    public class ProtectedObjectStats
    {
        public float maxHP;
    }

    public ProtectedObjectStats stats = new ProtectedObjectStats();

    public float currentHP;
    private StageManager stage;
    private PlayerStatManager playerStatManager;
    private Animator anim;

    private void Start()
    {
        stage = FindFirstObjectByType<StageManager>();
        playerStatManager = PlayerStatManager.instance;
        //stats.maxHP = database.protectedTargetHP;
        currentHP = playerStatManager.protectedTargetHP;
        anim = GetComponent<Animator>();
    }

    #region Hit Event
    private void OnEnable()
    {
        MeleeAttack.OnProtectAttacked += TakeDamage;
        ProjectileBase.OnEnemyProjectileHitProtect += TakeDamage;
    }
    private void OnDisable()
    {
        MeleeAttack.OnProtectAttacked -= TakeDamage;
        ProjectileBase.OnEnemyProjectileHitProtect -= TakeDamage;
    }
    #endregion

    void Update()
    {
        if (currentHP <= 0)
        {
            stage.isOver = true;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        anim.SetTrigger("TakingDamage");
    }
}
