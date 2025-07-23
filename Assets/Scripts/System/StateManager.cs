using UnityEngine;

public class StateManager : MonoBehaviour
{
    public PlayableCharInfo[] playableCharInfo;
    public string currentPlayCharName;

    public static float Maxhp; // �÷��̾��� ü��
    public static float hp; // ���� �÷��̾��� ü��
    public static float speed;
    public static float attackRange;
    public static float attackSpeed; //1�ʴ� �����Ҽ� �ִ� Ƚ��
    public static float attackDamage;
    public static float recoverTime; // �˴ٿ� ���¿��� ȸ���� �ɸ��� �ð�
    public static float attackAngle; // ���� ����
    public static float skill1Damage; // ��ų 1 ��Ÿ��
    public static float skill1Cooldown; // ��ų 1 ��Ÿ��
    public static float skill1MoveRange; // ��ų 1 �̵� ����

    public static bool isKnockDown = false; // �÷��̾ �˴ٿ� ��������  ����
    public static bool isDefend = false; // �÷��̾ ��� �������� ����

    private PlayerMove playerMove;
    void Start()
    {
        for (int i = 0; i < playableCharInfo.Length; i++)
        {
            if (playableCharInfo[i].charName == currentPlayCharName)
            {
                Instantiate(playableCharInfo[i].charPrefab, transform.position, Quaternion.identity);
                playerMove = FindFirstObjectByType<PlayerMove>();
                Maxhp = playableCharInfo[i].maxHealth;
                hp = Maxhp; // �÷��̾��� ü���� �ʱ�ȭ
                speed = playableCharInfo[i].moveSpeed;
                attackRange = playableCharInfo[i].attackRange;
                attackDamage = playableCharInfo[i].attackDamage;
                attackSpeed = playableCharInfo[i].attackSpeed;
                recoverTime = playableCharInfo[i].recoverTime;
                attackAngle = playableCharInfo[i].attackAngle;
                skill1Damage = playableCharInfo[i].skill1Damage;
                skill1Cooldown = playableCharInfo[i].skill1Cooldown;
                skill1MoveRange = playableCharInfo[i].skill1MoveRange;
                Debug.Log($"Current Playable Character: {playableCharInfo[i].charName}");
            }
        }
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

    public static void TakeDamage(float damage) //�÷��̾ ���� ������ ȣ��
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
}
