using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �÷��̾��� �Ӽ�
    [SerializeField] protected float Maxhp = 100f; // �÷��̾��� ü��
    [SerializeField] protected float hp; // ���� �÷��̾��� ü��
    [SerializeField] protected float speed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackSpeed; //1�ʴ� �����Ҽ� �ִ� Ƚ��
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float recoverTime; // �˴ٿ� ���¿��� ȸ���� �ɸ��� �ð�
    [SerializeField] protected float attackAngle = 90f; // ���� ����
    [SerializeField] protected LayerMask enemyLayerMask; // Inspector���� Enemy ���̾ ����
    [SerializeField] protected float skill1Cooldown = 5f; // ��ų 1 ��Ÿ��

    protected bool canMove = true; // �÷��̾ �̵� �������� ����
    protected bool isKnockDown = false; // �÷��̾ �˴ٿ� ��������   ����
    protected float currentAttackCooldown;
    protected float currentSkill1Cooldown;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;


    void Start()
    {
        hp = Maxhp; // �÷��̾��� ü���� �ʱ�ȭ
        currentAttackCooldown = 1f;
        currentSkill1Cooldown = skill1Cooldown; // ��ų 1 ��Ÿ�� �ʱ�ȭ
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (anim == null)
        {
            Debug.LogError("Animator component not found on Player object.");
        }
    }

    void Update()
    {
        if (!isKnockDown) 
        {
            
            TryMove();
            TryAttack();
            TrySkill1();
            if (hp <= 0)
            {
                StartCoroutine(KnockDownRoutine());
            }
        }
        
    }

    protected virtual void TryMove()
    {
        if (canMove)
        {
            Move();
        }
    }
    protected virtual void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, vertical, 0).normalized;
        if (direction.magnitude >= 0.1f)
        {
            transform.position += direction * speed * Time.deltaTime;
            anim.SetBool("Running", true);

            // �̵� ���⿡ ���� �ٶ󺸴� ���� ����
            if (horizontal != 0)
            {
                spriteRenderer.flipX = horizontal < 0;
            }
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }

    protected virtual void TryAttack()
    {
        if (currentAttackCooldown > 0) //���� ��Ÿ�� ���
        {
            currentAttackCooldown -= attackSpeed * Time.deltaTime;
        }
        else
        {
            currentAttackCooldown = 0;
        }
        if (currentAttackCooldown == 0 && Input.GetButtonDown("Fire1"))
        {
            currentAttackCooldown = 1f;
            Attack();
            anim.SetTrigger("Attack");

        }
    }

    protected virtual void Attack()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 attackDir = (mouseWorldPos - transform.position).normalized;

        // ���콺 ��ġ�� ���� �ٶ󺸴� ���� ����
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = mouseWorldPos.x < transform.position.x;
        }

        // ���� ���� �ð�ȭ (��ä�� ���·� ���� Ray �׸���)
        int rayCount = 2; // ��ä���� ������ Ray ����
        float halfAngle = attackAngle * 0.5f;
        for (int i = 0; i <= rayCount; i++)
        {
            float angle = -halfAngle + (attackAngle * i / rayCount);
            float rad = angle * Mathf.Deg2Rad;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * attackDir;
            Debug.DrawRay(transform.position, dir * attackRange, Color.yellow, 0.5f);
        }

        // �߾� ���� Ray (������)
        Debug.DrawRay(transform.position, attackDir * attackRange, Color.red, 0.5f);

        // ���� ���� ���� ��� �� Ž��
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayerMask);
        bool hitAny = false;
        foreach (var col in hits)
        {
            Vector2 toEnemy = (col.transform.position - transform.position).normalized;
            float angle = Vector2.Angle(attackDir, toEnemy);
            if (angle <= halfAngle)
            {
                Enemy enemy = col.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(attackDamage);
                    Debug.Log($"��({col.name})�� �����߽��ϴ�! (����: {angle:F1}��)");
                    hitAny = true;
                }
            }
        }
        if (!hitAny)
        {
            Debug.Log("���� ���� ���� ���� �����ϴ�.");
        }
    }

    protected virtual void TrySkill1()
    {
        if (currentSkill1Cooldown > 0) // ��ų 1 ��Ÿ�� ���
        {
            currentSkill1Cooldown -= Time.deltaTime;
        }
        else
        {
            currentSkill1Cooldown = 0;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if ((currentSkill1Cooldown == 0))
            {
                anim.SetTrigger("Skill1");
                Skill1();
                currentSkill1Cooldown = skill1Cooldown; // ��ų 1 ��Ÿ�� �ʱ�ȭ
            }
            else
            {
                Debug.Log("��ų1 ��ٿ���");
            }
        }
    }

    protected virtual void Skill1()
    {
        Debug.Log("��ų 1 ���!");
    }



    protected virtual IEnumerator KnockDownRoutine()
    {
        anim.SetTrigger("Knockdown");
        Debug.Log("�÷��̾ �˴ٿ� �����Դϴ�. ȸ�� ��...");
        isKnockDown = true; // �÷��̾ �˴ٿ� ���·� ��ȯ
        yield return new WaitForSeconds(recoverTime); // 3�� ���
        Recover(); // �˴ٿ� ���¿��� ȸ�� �Լ� ȣ��
    }
    protected virtual void Recover()
    {
        // �˴ٿ� ���¿��� ȸ�� ����
        hp = Maxhp; // ü���� �ִ�ġ�� ȸ��
        isKnockDown = false; // �˴ٿ� ���� ����
        anim.SetTrigger("Recover"); // �˴ٿ� �ִϸ��̼� Ʈ����
    }

    public virtual void TakeDamage(float damage) //�÷��̾ ���� ������ ȣ��
    {
        hp -= damage;
        anim.SetTrigger("TakeDamage");
        Debug.Log("�÷��̾ " + damage + "�� ���ظ� �޾ҽ��ϴ�. ���� ü��: " + hp);
    }
}
