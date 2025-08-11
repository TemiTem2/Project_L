using UnityEngine;

[CreateAssetMenu(fileName = "New Playable Character", menuName = "New Playable Character/Character")]
public class PlayableCharInfo : ScriptableObject
{
    [Header("ĳ���� �⺻ ���")]
    public string charName; // ĳ���� �̸�
    public Sprite charIcon; // ĳ���� ������
    public GameObject charPrefab; // ĳ���� ������
    public AudioClip attackSound; // ĳ���� ���� ����
    [TextArea]
    [Header("ĳ���� ����")]
    public string charDesc; //ĳ���� ����
    [Header("ĳ���� ����")]
    public float maxHealth; // �ִ� ü��
    public float moveSpeed; // �̵� �ӵ�
    public float attackRange; // ���� ����
    public float attackDamage; // ���� ���ط�
    public float attackSpeed; // ���� ��Ÿ��
    public float attackAngle; // ���� ����
    public float recoverTime; // �˴ٿ� ȸ�� �ð�
    public Skill[] skills; // ĳ���� ��ų ���
}


