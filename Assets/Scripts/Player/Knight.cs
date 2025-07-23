using UnityEngine;

public class Knight : PlayerMove
{
    [SerializeField] private float defendDuration = 2f; // 방어 지속 시간


    protected override void Skill1()
    {
        base.Skill1();
        Defend();
        Debug.Log("Knight의 스킬1이 발동되었습니다!");
    }

    private void Defend()
    {
        StateManager.isDefend = true;
        canMove = false; // 방어 중에는 이동 불가
        Debug.Log("Knight가 방어 자세를 취했습니다!");
        Invoke(nameof(Defend_End), defendDuration); // 일정 시간 후 방어 자세 해제
    }

    private void Defend_End()
    {
        StateManager.isDefend = false;
        canMove = true; // 방어 자세 해제 후 이동 가능
        Debug.Log("Knight의 방어 자세가 해제되었습니다");
        anim.SetTrigger("Skill1End"); // 방어 해제 애니메이션 트리거
    }
}
