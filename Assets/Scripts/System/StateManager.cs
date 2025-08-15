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
        database = Database.Instance;
        playerStatManager = PlayerStatManager.instance;

        Instantiate(database.currentCharInfo.charPrefab, transform.position, Quaternion.identity);


        Maxhp = database.currentCharInfo.maxHealth + (playerStatManager.statPoint.MaxHP * 10);
        hp = Maxhp; // 플레이어의 체력을 초기화
        speed = database.currentCharInfo.moveSpeed + (playerStatManager.statPoint.Speed * 0.1f);
        attackRange = database.currentCharInfo.attackRange + (playerStatManager.statPoint.AttackRange * 0.1f);
        attackDamage = database.currentCharInfo.attackDamage + (playerStatManager.statPoint.AttackDamage);
        attackSpeed = database.currentCharInfo.attackSpeed + (playerStatManager.statPoint.AttackSpeed * 0.1f);
        recoverTime = database.currentCharInfo.recoverTime - (playerStatManager.statPoint.RecoverTime * 0.1f);
        attackAngle = database.currentCharInfo.attackAngle;

        skill = SkillDeepCopy(); // 스킬 정보 초기화
        skill.damage += playerStatManager.statPoint.SkillDamage;
        skill.cooldown -= playerStatManager.statPoint.SkillCooldown * 0.1f;
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

    #region Projectile Hit Event
    private void OnEnable()
    {
        ProjectileBase.OnEnemyProjectileHitPlayer += TakeDamage;
    }
    private void OnDisable()
    {
        ProjectileBase.OnEnemyProjectileHitPlayer -= TakeDamage;
    }
    #endregion

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
    public Skill SkillDeepCopy()
    {
        // 스킬 정보를 깊은 복사하여 반환
        Skill skillCopy = ScriptableObject.CreateInstance<Skill>();
        skillCopy.skillname = database.currentSkillInfo.skillname;
        skillCopy.damage = database.currentSkillInfo.damage;
        skillCopy.cooldown = database.currentSkillInfo.cooldown;
        skillCopy.moveRange = database.currentSkillInfo.moveRange;
        skillCopy.clip = database.currentSkillInfo.clip;
        skillCopy.summonPrefab = database.currentSkillInfo.summonPrefab;
        skillCopy.sound = database.currentSkillInfo.sound;
        return skillCopy;
    }
}
