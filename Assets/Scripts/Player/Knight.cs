using UnityEngine;

public class Knight : PlayerMove
{
    [SerializeField] private float defendDuration = 2f; // ��� ���� �ð�


    protected override void Skill1()
    {
        base.Skill1();
        Defend();
        Debug.Log("Knight�� ��ų1�� �ߵ��Ǿ����ϴ�!");
    }

    private void Defend()
    {
        StateManager.isDefend = true;
        canMove = false; // ��� �߿��� �̵� �Ұ�
        Debug.Log("Knight�� ��� �ڼ��� ���߽��ϴ�!");
        Invoke(nameof(Defend_End), defendDuration); // ���� �ð� �� ��� �ڼ� ����
    }

    private void Defend_End()
    {
        StateManager.isDefend = false;
        canMove = true; // ��� �ڼ� ���� �� �̵� ����
        Debug.Log("Knight�� ��� �ڼ��� �����Ǿ����ϴ�");
        anim.SetTrigger("Skill1End"); // ��� ���� �ִϸ��̼� Ʈ����
    }
}
