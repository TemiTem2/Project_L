using UnityEngine;

public class Knight : Player
{
    private bool isDefence = false; // 방어 상태 여부
    private float defendDuration = 2f; // 방어 지속 시간


    protected override void Skill1()
    {
        base.Skill1();
        Defend();
        Debug.Log("Knight의 스킬1이 발동되었습니다!");
    }

    private void Defend()
    {
        isDefence = true;
        canMove = false; // 방어 중에는 이동 불가
        Debug.Log("Knight가 방어 자세를 취했습니다!");
        Invoke(nameof(Defend_End), defendDuration); // 일정 시간 후 방어 자세 해제
    }

    private void Defend_End()
    {
        isDefence = false;
        canMove = true; // 방어 자세 해제 후 이동 가능
        Debug.Log("Knight의 방어 자세가 해제되었습니다");
        anim.SetTrigger("Skill1End"); // 방어 해제 애니메이션 트리거
    }

    public override void TakeDamage(float damage)
    {
        if (isDefence)
        {
            Debug.Log("방어 중이므로 피해를 받지 않습니다.");
            return;
        }
        hp -= damage;
        anim.SetTrigger("TakeDamage");
        Debug.Log($"Knight가 {damage}의 피해를 받았습니다. 남은 체력: {hp}");
    }
}
