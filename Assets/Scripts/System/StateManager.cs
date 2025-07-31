using UnityEngine;

public class StateManager : MonoBehaviour
{
    private Database database;

    public float hp; // 현재 플레이어의 체력
    [Header("Player Stats")]
    public float Maxhp; // 플레이어의 최대 체력
    public float speed;
    public float attackRange;
    public float attackSpeed; //1초당 공격할수 있는 횟수
    public float attackDamage;
    public float recoverTime; // 넉다운 상태에서 회복에 걸리는 시간
    public float attackAngle; // 공격 각도
    public Skill skill; // 현재 플레이어의 스킬 정보

    public bool isKnockDown = false; // 플레이어가 넉다운 상태인지  여부
    public bool isDefend = false; // 플레이어가 방어 상태인지 여부

    public PlayerStatManager playerStatManager;

    void Start()
    {
        database = FindFirstObjectByType<Database>();
        playerStatManager = FindFirstObjectByType<PlayerStatManager>();

        Instantiate(database.currentCharInfo.charPrefab, transform.position, Quaternion.identity);


        Maxhp = database.currentCharInfo.maxHealth;
        hp = Maxhp; // 플레이어의 체력을 초기화
        speed = database.currentCharInfo.moveSpeed;
        attackRange = database.currentCharInfo.attackRange;
        attackDamage = database.currentCharInfo.attackDamage;
        attackSpeed = database.currentCharInfo.attackSpeed;
        recoverTime = database.currentCharInfo.recoverTime;
        attackAngle = database.currentCharInfo.attackAngle;

        skill = database.currentSkillInfo;

        //for (int i = 0; i < database.playableCharInfo.Length; i++)
        // {
        //     if (database.playableCharInfo[i].charName == database.currentPlayCharName)
        //     {
        //         Instantiate(database.playableCharInfo[i].charPrefab, transform.position, Quaternion.identity);


        //         Maxhp = database.playableCharInfo[i].maxHealth;
        //         hp = Maxhp; // 플레이어의 체력을 초기화
        //         speed = database.playableCharInfo[i].moveSpeed;
        //         attackRange = database.playableCharInfo[i].attackRange;
        //         attackDamage = database.playableCharInfo[i].attackDamage;
        //         attackSpeed = database.playableCharInfo[i].attackSpeed;
        //         recoverTime = database.playableCharInfo[i].recoverTime;
        //         attackAngle = database.playableCharInfo[i].attackAngle;
        //         for (int j = 0; j < database.playableCharInfo[i].skills.Length; j++)
        //         {
        //             if (database.playableCharInfo[i].skills[j].skillname == database.currentPlayerSkill)
        //             {
        //                 skill = database.playableCharInfo[i].skills[j];
        //             }
        //         }
        //         //skill1Damage = playableCharInfo[i].skill1Damage;
        //         //skill1Cooldown = playableCharInfo[i].skill1Cooldown;
        //         //skill1MoveRange = playableCharInfo[i].skill1MoveRange;
        //         Debug.Log($"Current Playable Character: {database.playableCharInfo[i].charName}");
        //     }
        // }
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

    public void TakeDamage(float damage) //플레이어가 공격 받을떄 호출
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

    public void Recover()
    {
        // 넉다운 상태에서 회복 로직
        PlayerMove playerMove = FindFirstObjectByType<PlayerMove>();
        hp = Maxhp; // 체력을 최대치로 회복
        playerMove.anim.SetTrigger("Recover"); // 넉다운 애니메이션 트리거
    }
}
