using Unity.VisualScripting;
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
    private Skill skilltemp; // ��ų ���� �ӽ� �����

    public bool isKnockDown = false; // �÷��̾ �˴ٿ� ��������  ����
    public bool isDefend = false; // �÷��̾ ��� �������� ����

    public PlayerStatManager playerStatManager;

    void Start()
    {
        database = FindFirstObjectByType<Database>();
        playerStatManager = FindFirstObjectByType<PlayerStatManager>();

        Instantiate(database.currentCharInfo.charPrefab, transform.position, Quaternion.identity);


        Maxhp = database.currentCharInfo.maxHealth + (playerStatManager.statPoint.MaxHP * 10);
        hp = Maxhp; // �÷��̾��� ü���� �ʱ�ȭ
        speed = database.currentCharInfo.moveSpeed + (playerStatManager.statPoint.Speed * 0.1f);
        attackRange = database.currentCharInfo.attackRange + (playerStatManager.statPoint.AttackRange * 0.1f);
        attackDamage = database.currentCharInfo.attackDamage + (playerStatManager.statPoint.AttackDamage);
        attackSpeed = database.currentCharInfo.attackSpeed + (playerStatManager.statPoint.AttackSpeed * 0.1f);
        recoverTime = database.currentCharInfo.recoverTime - (playerStatManager.statPoint.RecoverTime * 0.1f);
        attackAngle = database.currentCharInfo.attackAngle;

        skill = new Skill // ��ų ���� �ʱ�ȭ
        {
            skillname = database.currentSkillInfo.skillname,
            damage = database.currentSkillInfo.damage + (playerStatManager.statPoint.SkillDamage * 10),
            cooldown = database.currentSkillInfo.cooldown - (playerStatManager.statPoint.SkillCooldown * 0.1f),
            moveRange = database.currentSkillInfo.moveRange,
            clip = database.currentSkillInfo.clip,
            summonPrefab = database.currentSkillInfo.summonPrefab,
            sound = database.currentSkillInfo.sound
        };
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
}
