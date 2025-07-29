using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class StstupWindow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI skillPt;
    [SerializeField]
    [Tooltip("순서 맞추기 MaxHP, Speed, AttackRange, AttackDamage, AttackSpeed,RecoverTime, SkillDamage, SkillCooldown")]
    private TextMeshProUGUI[] statText; // 

    private PlayerStatManager playerStatManager;
    void Start()
    {
        playerStatManager = PlayerStatManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        skillPt.text = playerStatManager.skillPoints.ToString();
        statText[0].text = playerStatManager.statPoint.MaxHP.ToString();
        statText[1].text = playerStatManager.statPoint.Speed.ToString();
        statText[2].text = playerStatManager.statPoint.AttackRange.ToString();
        statText[3].text = playerStatManager.statPoint.AttackDamage.ToString();
        statText[4].text = playerStatManager.statPoint.AttackSpeed.ToString();
        statText[5].text = playerStatManager.statPoint.RecoverTime.ToString();
        statText[6].text = playerStatManager.statPoint.SkillDamage.ToString();
        statText[7].text = playerStatManager.statPoint.SkillCooldown.ToString();
    }
}
