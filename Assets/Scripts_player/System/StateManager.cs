using UnityEngine;

public class StateManager : MonoBehaviour
{
    public PlayableCharInfo[] playableCharInfo;
    public string currentPlayCharName;
    public string currentPlayerSkill;

    public static float Maxhp; // 플레이어의 체력
    public static float hp; // 현재 플레이어의 체력
    public static float speed;
    public static float attackRange;
    public static float attackSpeed; //1초당 공격할수 있는 횟수
    public static float attackDamage;
    public static float recoverTime; // 넉다운 상태에서 회복에 걸리는 시간
    public static float attackAngle; // 공격 각도
    public static Skill skill; // 현재 플레이어의 스킬 정보
    public static float skillDamage; // 스킬 1 쿨타임
    public static float skillCooldown; // 스킬 1 쿨타임
    public static float skillMoveRange; // 스킬 1 이동 범위

    public static bool isKnockDown = false; // 플레이어가 넉다운 상태인지  여부
    public static bool isDefend = false; // 플레이어가 방어 상태인지 여부

    private PlayerMove playerMove;
    void Start()
    {
        for (int i = 0; i < playableCharInfo.Length; i++)
        {
            if (playableCharInfo[i].charName == currentPlayCharName)
            {
                Instantiate(playableCharInfo[i].charPrefab, transform.position, Quaternion.identity);
                playerMove = FindFirstObjectByType<PlayerMove>();
                Maxhp = playableCharInfo[i].maxHealth;
                hp = Maxhp; // 플레이어의 체력을 초기화
                speed = playableCharInfo[i].moveSpeed;
                attackRange = playableCharInfo[i].attackRange;
                attackDamage = playableCharInfo[i].attackDamage;
                attackSpeed = playableCharInfo[i].attackSpeed;
                recoverTime = playableCharInfo[i].recoverTime;
                attackAngle = playableCharInfo[i].attackAngle;
                for (int j = 0; j < playableCharInfo[i].skills.Length; j++)
                {
                    if (playableCharInfo[i].skills[j].skillname == currentPlayerSkill)
                    {
                        skill = playableCharInfo[i].skills[j];
                    }
                }
                //skill1Damage = playableCharInfo[i].skill1Damage;
                //skill1Cooldown = playableCharInfo[i].skill1Cooldown;
                //skill1MoveRange = playableCharInfo[i].skill1MoveRange;
                Debug.Log($"Current Playable Character: {playableCharInfo[i].charName}");
            }
        }
    }

    void Update()
    {
        if (hp > 0)
        {
            isKnockDown = false; // 플레이어가 넉다운 상태가 아니면 false로 설정
        }
        else
        {
            isKnockDown = true; // 플레이어가 넉다운 상태로 전환
        }
    }

    public static void TakeDamage(float damage) //플레이어가 공격 받을떄 호출
    {
        if (isDefend) // 방어 상태일 때
        {
            Debug.Log("방어 중! 피해가 무효되었습니다.");
            return; // 피해를 받지 않음
        }
        PlayerMove playerMove = FindFirstObjectByType<PlayerMove>();
        hp -= damage;
        playerMove.anim.SetTrigger("TakeDamage");
        Debug.Log("플레이어가 " + damage + "의 피해를 받았습니다. 남은 체력: " + hp);
    }
}
