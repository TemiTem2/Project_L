using UnityEngine;
using UnityEngine.UIElements;

public class Knight : PlayerMove
{
    //[SerializeField] private float defendDuration = 2f; // ��� ���� �ð�


    protected override void Skill1()
    {
        base.Skill1();
        switch(stateManager.skill.skillname)
        {
            case "Defend":
                Defend();
                break;
            case "SmashingGround":
                SmashingGround();
                break;
            default:
                Debug.LogWarning("Unknown skill name: " + stateManager.skill.skillname);
                break;
        }
        Debug.Log("Knight�� ��ų1�� �ߵ��Ǿ����ϴ�!");
    }

    private void Defend()
    {
        stateManager.isDefend = true;
        canMove = false; // ��� �߿��� �̵� �Ұ�
        Debug.Log("Knight�� ��� �ڼ��� ���߽��ϴ�!");
        Invoke(nameof(Defend_End), stateManager.skill.damage); // ���� �ð� �� ��� �ڼ� ����
    }

    private void Defend_End()
    {
        stateManager.isDefend = false;
        canMove = true; // ��� �ڼ� ���� �� �̵� ����
        Debug.Log("Knight�� ��� �ڼ��� �����Ǿ����ϴ�");
        anim.SetTrigger("Skill1End"); // ��� ���� �ִϸ��̼� Ʈ����
    }

    private void SmashingGround()
    {
        // ���콺 ��ġ ���ϱ�
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;

        // ���� ���
        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        // ������ ����
        SkillPool.Instance.GetObject(stateManager.skill.skillname, transform.position, Quaternion.identity, direction, stateManager.skill.damage);

        Debug.Log("Knight�� ���� �����ƽ��ϴ�!");
        anim.SetTrigger("Skill1End"); // ��ų ��� �� �ִϸ��̼� Ʈ����
    }
}
