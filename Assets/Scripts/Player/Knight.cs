using UnityEngine;

public class Knight : Player
{
    private bool isDefence = false; // ��� ���� ����
    private float defendDuration = 2f; // ��� ���� �ð�


    protected override void Skill1()
    {
        base.Skill1();
        Defend();
        Debug.Log("Knight�� ��ų1�� �ߵ��Ǿ����ϴ�!");
    }

    private void Defend()
    {
        isDefence = true;
        canMove = false; // ��� �߿��� �̵� �Ұ�
        Debug.Log("Knight�� ��� �ڼ��� ���߽��ϴ�!");
        Invoke(nameof(Defend_End), defendDuration); // ���� �ð� �� ��� �ڼ� ����
    }

    private void Defend_End()
    {
        isDefence = false;
        canMove = true; // ��� �ڼ� ���� �� �̵� ����
        Debug.Log("Knight�� ��� �ڼ��� �����Ǿ����ϴ�");
        anim.SetTrigger("Skill1End"); // ��� ���� �ִϸ��̼� Ʈ����
    }

    public override void TakeDamage(float damage)
    {
        if (isDefence)
        {
            Debug.Log("��� ���̹Ƿ� ���ظ� ���� �ʽ��ϴ�.");
            return;
        }
        hp -= damage;
        anim.SetTrigger("TakeDamage");
        Debug.Log($"Knight�� {damage}�� ���ظ� �޾ҽ��ϴ�. ���� ü��: {hp}");
    }
}
