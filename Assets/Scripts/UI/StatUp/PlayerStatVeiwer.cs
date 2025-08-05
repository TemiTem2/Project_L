using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatVeiwer : MonoBehaviour
{
    private PlayerStatManager playerStatManager;
    private Database database;
    [SerializeField]
    private TextMeshProUGUI currentCharName;
    [SerializeField]
    private Image charImage;
    [SerializeField]
    [Tooltip("순서 맞추기 MaxHP, Speed, AttackRange, AttackDamage, AttackSpeed,RecoverTime, SkillDamage, SkillCooldown")]
    private TextMeshProUGUI[] statText;
    void Awake()
    {
        database = Database.Instance;
        playerStatManager = PlayerStatManager.instance;
    }
    void Start()
    {
        
    }

    private void Update()
    {
        UIupdate();
    }

    public void UIupdate()
    {
        currentCharName.text = database.currentPlayCharName;
        charImage.sprite = database.currentCharInfo.charIcon;
        statText[0].text = database.currentCharInfo.maxHealth.ToString() + "+" + (playerStatManager.statPoint.MaxHP*10).ToString();
        statText[1].text = database.currentCharInfo.moveSpeed.ToString() + "+" + (playerStatManager.statPoint.Speed * 0.1f).ToString();
        statText[2].text = database.currentCharInfo.attackRange.ToString() + "+" + (playerStatManager.statPoint.AttackRange * 0.1f).ToString();
        statText[3].text = database.currentCharInfo.attackDamage.ToString() + "+" + (playerStatManager.statPoint.AttackDamage).ToString();
        statText[4].text = database.currentCharInfo.attackSpeed.ToString() + "+" + (playerStatManager.statPoint.AttackSpeed * 0.1f).ToString();
        statText[5].text = database.currentCharInfo.recoverTime.ToString() + "-" + (playerStatManager.statPoint.RecoverTime * 0.1f).ToString();
        statText[6].text = database.currentSkillInfo.damage.ToString() + "+" + (playerStatManager.statPoint.SkillDamage).ToString();
        statText[7].text = database.currentSkillInfo.cooldown.ToString() + "-" + (playerStatManager.statPoint.SkillCooldown * 0.1f).ToString();
    }
}
