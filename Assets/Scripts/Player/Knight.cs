using UnityEngine;
using UnityEngine.UIElements;

public class Knight : PlayerMove
{
    //[SerializeField] private float defendDuration = 2f; // 방어 지속 시간


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
        Debug.Log("Knight의 스킬1이 발동되었습니다!");
    }

    private void Defend()
    {
        stateManager.isDefend = true;
        canMove = false; // 방어 중에는 이동 불가
        Debug.Log("Knight가 방어 자세를 취했습니다!");
        Invoke(nameof(Defend_End), stateManager.skill.damage); // 일정 시간 후 방어 자세 해제
    }

    private void Defend_End()
    {
        stateManager.isDefend = false;
        canMove = true; // 방어 자세 해제 후 이동 가능
        Debug.Log("Knight의 방어 자세가 해제되었습니다");
        anim.SetTrigger("Skill1End"); // 방어 해제 애니메이션 트리거
    }

    private void SmashingGround()
    {
        // 마우스 위치 구하기
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = transform.position.z;

        // 방향 계산
        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        // 프리팹 생성
        SkillPool.Instance.GetObject(stateManager.skill.skillname, transform.position, Quaternion.identity, direction, stateManager.skill.damage);

        Debug.Log("Knight가 땅을 내리쳤습니다!");
        anim.SetTrigger("Skill1End"); // 스킬 사용 후 애니메이션 트리거
    }
}
