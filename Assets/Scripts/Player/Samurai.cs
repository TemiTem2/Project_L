using UnityEngine;

public class Samurai : Player
{
    [SerializeField] private float skill1Damage = 5f; // 스킬 1의 피해량
    [SerializeField] private float skill1MoveRange = 5f; // 스킬 1의 최대 이동 거리

    protected override void Skill1()
    {
        // 1. 마우스 위치 구하기
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z; // 2D 환경 기준

        // 2. 플레이어와 마우스 위치 사이 방향 및 거리 계산
        Vector2 direction = (mouseWorldPos - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, mouseWorldPos);

        // 3. 실제 이동 거리 결정 (최대 skill1MoveRange까지만 이동)
        float moveDistance = Mathf.Min(distance, skill1MoveRange);
        Vector3 targetPos = transform.position + (Vector3)(direction * moveDistance);

        // 4. Raycast로 사이에 있는 적 모두 탐지 및 피해
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, moveDistance, enemyLayerMask);
        bool hitAny = false;
        foreach (var hit in hits)
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(skill1Damage);
                Debug.Log($"스킬1: 적({hit.collider.name})에게 피해를 주었습니다!");
                hitAny = true;
            }
        }
        if (!hitAny)
        {
            Debug.Log("스킬1: 경로상에 적이 없습니다.");
        }

        // 5. 플레이어를 targetPos로 이동
        transform.position = targetPos;

        // 6. 디버그용 Ray 시각화
        Debug.DrawRay(transform.position, direction * moveDistance, Color.cyan, 0.5f);

        // 7. 애니메이션 트리거 등 추가 가능
        if (anim != null)
        {
            anim.SetTrigger("Skill1End");
        }
    }
}
