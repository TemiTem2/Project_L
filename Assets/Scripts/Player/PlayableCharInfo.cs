using UnityEngine;

[CreateAssetMenu(fileName = "New Playable Character", menuName = "New Playable Character/Character")]
public class PlayableCharInfo : ScriptableObject
{
    public string charName; // 캐릭터 이름
    public Sprite charIcon; // 캐릭터 아이콘
    public GameObject charPrefab; // 캐릭터 프리팹
    [TextArea]
    public string charDesc; //캐릭터 설명
    public float maxHealth; // 최대 체력
    public float moveSpeed; // 이동 속도
    public float attackRange; // 공격 범위
    public float attackDamage; // 공격 피해량
    public float attackSpeed; // 공격 쿨타임
    public float attackAngle; // 공격 각도
    public float recoverTime; // 넉다운 회복 시간
    public Skill[] skills; // 캐릭터 스킬 목록
    //public float skill1Damage; // 스킬 1 데미지
    //public float skill1Cooldown; // 스킬 1 쿨타임
    //public float skill1MoveRange; // 스킬 1 이동 범위
}


