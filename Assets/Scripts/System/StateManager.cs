using UnityEngine;

public class StateManager : MonoBehaviour
{
    private Database database;

    public float hp; // ���� �÷��̾��� ü��
    [Header("Player Stats")]
    public float Maxhp; // �÷��̾��� �ִ� ü��
    public float speed;
    public float attackRange;
    public float attackSpeed; //1�ʴ� �����Ҽ� �ִ� Ƚ��
    public float attackDamage;
    public float recoverTime; // �˴ٿ� ���¿��� ȸ���� �ɸ��� �ð�
    public float attackAngle; // ���� ����
    public Skill skill; // ���� �÷��̾��� ��ų ����

    public bool isKnockDown = false; // �÷��̾ �˴ٿ� ��������  ����
    public bool isDefend = false; // �÷��̾ ��� �������� ����

    public PlayerStatManager playerStatManager;

    void Start()
    {
        database = Database.Instance;
        playerStatManager = PlayerStatManager.instance;

        Instantiate(database.currentCharInfo.charPrefab, transform.position, Quaternion.identity);


        Maxhp = database.currentCharInfo.maxHealth + (playerStatManager.statPoint.MaxHP * 10);
        hp = Maxhp; // �÷��̾��� ü���� �ʱ�ȭ
        speed = database.currentCharInfo.moveSpeed + (playerStatManager.statPoint.Speed * 0.1f);
        attackRange = database.currentCharInfo.attackRange + (playerStatManager.statPoint.AttackRange * 0.1f);
        attackDamage = database.currentCharInfo.attackDamage + (playerStatManager.statPoint.AttackDamage);
        attackSpeed = database.currentCharInfo.attackSpeed + (playerStatManager.statPoint.AttackSpeed * 0.1f);
        recoverTime = database.currentCharInfo.recoverTime - (playerStatManager.statPoint.RecoverTime * 0.1f);
        attackAngle = database.currentCharInfo.attackAngle;

        skill = SkillDeepCopy(); // ��ų ���� �ʱ�ȭ
        skill.damage += playerStatManager.statPoint.SkillDamage;
        skill.cooldown -= playerStatManager.statPoint.SkillCooldown * 0.1f;
    }

    void Update()
    {
        if (hp > 0)
        {
            isKnockDown = false; // �÷��̾ �˴ٿ� ���°� �ƴϸ� false�� ����
        }
        else
        {
            isKnockDown = true; // �÷��̾ �˴ٿ� ���·� ��ȯ
        }
    }

    #region Projectile Hit Event
    private void OnEnable()
    {
        ProjectileBase.OnEnemyProjectileHitPlayer += TakeDamage;
    }
    private void OnDisable()
    {
        ProjectileBase.OnEnemyProjectileHitPlayer -= TakeDamage;
    }
    #endregion

    public void TakeDamage(float damage) //�÷��̾ ���� ������ ȣ��
    {
        if (isDefend) // ��� ������ ��
        {
            Debug.Log("��� ��! ���ذ� ��ȿ�Ǿ����ϴ�.");
            return; // ���ظ� ���� ����
        }
        PlayerMove playerMove = FindFirstObjectByType<PlayerMove>();
        hp -= damage;
        playerMove.anim.SetTrigger("TakeDamage");
        Debug.Log("�÷��̾ " + damage + "�� ���ظ� �޾ҽ��ϴ�. ���� ü��: " + hp);
    }

    public void Recover()
    {
        // �˴ٿ� ���¿��� ȸ�� ����
        PlayerMove playerMove = FindFirstObjectByType<PlayerMove>();
        hp = Maxhp; // ü���� �ִ�ġ�� ȸ��
        playerMove.anim.SetTrigger("Recover"); // �˴ٿ� �ִϸ��̼� Ʈ����
    }
    public Skill SkillDeepCopy()
    {
        // ��ų ������ ���� �����Ͽ� ��ȯ
        Skill skillCopy = ScriptableObject.CreateInstance<Skill>();
        skillCopy.skillname = database.currentSkillInfo.skillname;
        skillCopy.damage = database.currentSkillInfo.damage;
        skillCopy.cooldown = database.currentSkillInfo.cooldown;
        skillCopy.moveRange = database.currentSkillInfo.moveRange;
        skillCopy.clip = database.currentSkillInfo.clip;
        skillCopy.summonPrefab = database.currentSkillInfo.summonPrefab;
        skillCopy.sound = database.currentSkillInfo.sound;
        return skillCopy;
    }
}
