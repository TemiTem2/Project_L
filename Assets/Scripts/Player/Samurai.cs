using UnityEngine;

public class Samurai : Player
{
    [SerializeField] private float skill1Damage = 5f; // ��ų 1�� ���ط�
    [SerializeField] private float skill1MoveRange = 5f; // ��ų 1�� �ִ� �̵� �Ÿ�

    protected override void Skill1()
    {
        // 1. ���콺 ��ġ ���ϱ�
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z; // 2D ȯ�� ����

        // 2. �÷��̾�� ���콺 ��ġ ���� ���� �� �Ÿ� ���
        Vector2 direction = (mouseWorldPos - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, mouseWorldPos);

        // 3. ���� �̵� �Ÿ� ���� (�ִ� skill1MoveRange������ �̵�)
        float moveDistance = Mathf.Min(distance, skill1MoveRange);
        Vector3 targetPos = transform.position + (Vector3)(direction * moveDistance);

        // 4. Raycast�� ���̿� �ִ� �� ��� Ž�� �� ����
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, moveDistance, enemyLayerMask);
        bool hitAny = false;
        foreach (var hit in hits)
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(skill1Damage);
                Debug.Log($"��ų1: ��({hit.collider.name})���� ���ظ� �־����ϴ�!");
                hitAny = true;
            }
        }
        if (!hitAny)
        {
            Debug.Log("��ų1: ��λ� ���� �����ϴ�.");
        }

        // 5. �÷��̾ targetPos�� �̵�
        transform.position = targetPos;

        // 6. ����׿� Ray �ð�ȭ
        Debug.DrawRay(transform.position, direction * moveDistance, Color.cyan, 0.5f);

        // 7. �ִϸ��̼� Ʈ���� �� �߰� ����
        if (anim != null)
        {
            anim.SetTrigger("Skill1End");
        }
    }
}
