using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    // 플레이어의 속성
    [SerializeField] protected float Maxhp = 100f; // 플레이어의 체력
    [SerializeField] protected float hp; // 현재 플레이어의 체력
    [SerializeField] protected float speed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackSpeed; //1초당 공격할수 있는 횟수
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float recoverTime; // 넉다운 상태에서 회복에 걸리는 시간
    [SerializeField] protected float attackAngle = 90f; // 공격 각도
    [SerializeField] protected LayerMask enemyLayerMask; // Inspector에서 Enemy 레이어만 선택

    protected bool isKnockDown = false; // 플레이어가 넉다운 상태인지   여부
    protected float currentAttackCooldown;
    protected Animator anim;
    protected SpriteRenderer spriteRenderer;


    void Start()
    {
        hp = Maxhp; // 플레이어의 체력을 초기화
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
        if (!isKnockDown) 
        {
            if (currentAttackCooldown > 0) //공격 쿨타임 계산
            {
                currentAttackCooldown -= attackSpeed * Time.deltaTime;
            }
            else
            {
                currentAttackCooldown = 0;
            }
            Move();
            TryAttack();
            if (hp <= 0)
            {
                StartCoroutine(KnockDownRoutine());
            }
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

            // 이동 방향에 따라 바라보는 방향 변경
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
            Attack();
            anim.SetTrigger("Attack");

        }
    }

    protected virtual void Attack()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 attackDir = (mouseWorldPos - transform.position).normalized;

        // 마우스 위치에 따라 바라보는 방향 변경
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = mouseWorldPos.x < transform.position.x;
        }

        // 디버그용 Ray 시각화
        Debug.DrawRay(transform.position, attackDir * attackRange, Color.red, 0.5f);



        // 공격 범위 내의 모든 적 탐색
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayerMask);
        bool hitAny = false;
        foreach (var col in hits)
        {
            Vector2 toEnemy = (col.transform.position - transform.position).normalized;
            float angle = Vector2.Angle(attackDir, toEnemy);
            if (angle <= attackAngle * 0.5f) // 각도의 절반 기준
            {
                Enemy enemy = col.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(attackDamage);
                    Debug.Log($"적({col.name})을 공격했습니다! (각도: {angle:F1}도)");
                    hitAny = true;
                }
            }
        }
        if (!hitAny)
        {
            Debug.Log("공격 범위 내에 적이 없습니다.");
        }
    }

    protected virtual IEnumerator KnockDownRoutine()
    {
        anim.SetTrigger("Knockdown");
        Debug.Log("플레이어가 넉다운 상태입니다. 회복 중...");
        isKnockDown = true; // 플레이어가 넉다운 상태로 전환
        yield return new WaitForSeconds(recoverTime); // 3초 대기
        Recover(); // 넉다운 상태에서 회복 함수 호출
    }
    protected virtual void Recover()
    {
        // 넉다운 상태에서 회복 로직
        hp = Maxhp; // 체력을 최대치로 회복
        isKnockDown = false; // 넉다운 상태 해제
        anim.SetTrigger("Recover"); // 넉다운 애니메이션 트리거
    }

    public void TakeDamage(float damage) //플레이어가 공격 받을떄 호출
    {
        hp -= damage;
        Debug.Log("플레이어가 " + damage + "의 피해를 받았습니다. 남은 체력: " + hp);
    }
}
