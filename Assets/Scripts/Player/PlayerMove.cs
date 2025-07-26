using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 플레이어의 속성
    [SerializeField] protected LayerMask enemyLayerMask; // Inspector에서 Enemy 레이어만 선택

    protected bool canMove = true; // 플레이어가 이동 가능한지 여부
    
    protected float currentAttackCooldown;
    protected float currentSkill1Cooldown;
    public Animator anim;
    protected SpriteRenderer spriteRenderer;
    protected StateManager stateManager;


    void Start()
    {
        currentAttackCooldown = 1f;
        currentSkill1Cooldown = 0f; // 스킬 1 쿨타임 초기화
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stateManager = FindFirstObjectByType<StateManager>();
        if (anim == null)
        {
            Debug.LogError("Animator component not found on Player object.");
        }
    }

    void Update()
    {
        if (!stateManager.isKnockDown) 
        {
            
            TryMove();
            TryAttack();
            TrySkill1();
        }
        else
        {
            StartCoroutine(KnockDownRoutine());
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
            transform.position += direction * stateManager.speed * Time.deltaTime;
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
        if (currentAttackCooldown > 0) //공격 쿨타임 계산
        {
            currentAttackCooldown -= stateManager.attackSpeed * Time.deltaTime;
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

        // 마우스 위치에 따라 바라보는 방향 변경
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = mouseWorldPos.x < transform.position.x;
        }

        // 공격 각도 시각화 (부채꼴 형태로 여러 Ray 그리기)
        int rayCount = 2; // 부채꼴을 구성할 Ray 개수
        float halfAngle = stateManager.attackAngle * 0.5f;
        for (int i = 0; i <= rayCount; i++)
        {
            float angle = -halfAngle + (stateManager.attackAngle * i / rayCount);
            float rad = angle * Mathf.Deg2Rad;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * attackDir;
            Debug.DrawRay(transform.position, dir * stateManager.attackRange, Color.yellow, 0.5f);
        }

        // 중앙 방향 Ray (빨간색)
        Debug.DrawRay(transform.position, attackDir * stateManager. attackRange, Color.red, 0.5f);

        // 공격 범위 내의 모든 적 탐색
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, stateManager.attackRange, enemyLayerMask);
        bool hitAny = false;
        foreach (var col in hits)
        {
            Vector2 toEnemy = (col.transform.position - transform.position).normalized;
            float angle = Vector2.Angle(attackDir, toEnemy);
            if (angle <= halfAngle)
            {
                EnemyAI enemy = col.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    enemy.TakeDamage(stateManager.attackDamage);
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

    protected virtual void TrySkill1()
    {
        if (currentSkill1Cooldown > 0) // 스킬 1 쿨타임 계산
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
                currentSkill1Cooldown = stateManager.skill.cooldown; // 스킬 쿨타임 초기화
            }
            else
            {
                Debug.Log("스킬 쿨다운중");
            }
        }
    }

    protected virtual void Skill1()
    {
        Debug.Log("스킬 사용!");
    }



    protected virtual IEnumerator KnockDownRoutine()
    {
        anim.SetTrigger("Knockdown");
        Debug.Log("플레이어가 넉다운 상태입니다. 회복 중...");
        yield return new WaitForSeconds(stateManager.recoverTime); // 3초 대기
        stateManager.Recover(); // 넉다운 상태에서 회복 함수 호출
    }
    
}
