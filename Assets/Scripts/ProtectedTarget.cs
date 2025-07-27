using UnityEngine;

public class ProtectedTarget : MonoBehaviour
{
    [System.Serializable]
    public class ProtectedObjectStats
    {
        public int maxHP;
    }

    public ProtectedObjectStats stats = new ProtectedObjectStats();

    [SerializeField]
    private int currentHP;
    private StageManager stage;

    private void Start()
    {
        currentHP = stats.maxHP;
    }

    void Update()
    {
        if (currentHP <= 0)
        {
            stage.isOver = true;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
    }
}
