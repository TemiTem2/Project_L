using UnityEngine;

public class ProtectedTarget : MonoBehaviour
{
    [System.Serializable]
    public class ProtectedObjectStats
    {
        public float maxHP;
    }

    public ProtectedObjectStats stats = new ProtectedObjectStats();

    [SerializeField]
    private float currentHP;
    private StageManager stage;

    private void Start()
    {
        stage = FindFirstObjectByType<StageManager>();
        currentHP = stats.maxHP;
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
    }
}
