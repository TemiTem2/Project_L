using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected LayerMask enemyLayerMask; // Inspector���� Enemy ���̾ ����

    protected float currentAttackCooldown;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;


    void Start()
    {
        currentAttackCooldown = 1f;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (anim == null)
        {
            Debug.LogError("Animator component not found on Player object.");
        }
    }

    void Update()
    {
        if (currentAttackCooldown > 0)
        {
            currentAttackCooldown -= attackSpeed * Time.deltaTime;
        }
        else
        {
            currentAttackCooldown = 0;
        }
        Move();
        TryAttack();
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
        if (currentAttackCooldown == 0 && Input.GetButtonDown("Fire1"))
        {
            currentAttackCooldown = 1f;
            anim.SetTrigger("Attack");
            Attack();
        }
    }

    protected virtual void Attack()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        // ���콺 ��ġ�� ���� �ٶ󺸴� ���� ����
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = mouseWorldPos.x < transform.position.x;
        }

        // ����׿� Ray �ð�ȭ (������, 0.5�ʰ� ǥ��)
        Debug.DrawRay(transform.position, direction * attackRange, Color.red, 0.5f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, attackRange, enemyLayerMask);

        if (hit.collider != null)
        {
            Debug.Log($"��({hit.collider.name})�� �����߽��ϴ�!");
        }
        else
        {
            Debug.Log("���� ���� ���� ���� �����ϴ�.");
        }
    }
}
