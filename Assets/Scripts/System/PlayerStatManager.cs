using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    public int level = 1;
    public int exp;
    public int expToNextLevel=20;
    public int skillPoints = 10;
    public float protectedTargetHP = 100f;

    public StatPoint statPoint = new StatPoint();

    public static PlayerStatManager instance = null;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this; 
            DontDestroyOnLoad(gameObject); 
            ResetStats();
        }
        else
        {
            if (instance != this) 
                Destroy(this.gameObject);
        }
    }

    public void Statup(int index)
    {
        if (skillPoints > 0)
        {
            skillPoints--;
            switch (index)
            {
                case 0: // MaxHP
                    statPoint.MaxHP += 1f;
                    break;
                case 1: // Speed
                    statPoint.Speed += 1f;
                    break;
                case 2: // AttackRange
                    statPoint.AttackRange += 1f;
                    break;
                case 3: // AttackDamage
                    statPoint.AttackDamage += 1;
                    break;
                case 4: // AttackSpeed
                    statPoint.AttackSpeed += 1f;
                    break;
                case 5: // RecoverTime
                    statPoint.RecoverTime += 1f;
                    break;
                case 6: // SkillDamage
                    statPoint.SkillDamage += 1;
                    break;
                case 7: // SkillCooldown
                    statPoint.SkillCooldown += 1f;
                    break;
                default:
                    Debug.LogWarning("알수 없는 스탯: " + index);
                    return;
            }
        }
        else
        {
            Debug.LogWarning("스킬 포인트가 부족합니다.");
        }
    }

    public void StatDown(int index)
    {
        switch (index)
        {
            case 0: // MaxHP
                if (statPoint.MaxHP <= 0f)
                {
                    Debug.LogWarning("0이하로 낮출수 없습니다.");
                    return;
                }
                skillPoints++;
                statPoint.MaxHP -= 1f;
                break;
            case 1: // Speed
                if (statPoint.Speed <= 0f)
                {
                    Debug.LogWarning("0이하로 낮출수 없습니다.");
                    return;
                }
                skillPoints++;
                statPoint.Speed -= 1f;
                break;
            case 2: // AttackRange
                if (statPoint.AttackRange <= 0f)
                {
                    Debug.LogWarning("0이하로 낮출수 없습니다.");
                    return;
                }
                skillPoints++;
                statPoint.AttackRange -= 1f;
                break;
            case 3: // AttackDamage
                if (statPoint.AttackDamage <= 0f)
                {
                    Debug.LogWarning("0이하로 낮출수 없습니다.");
                    return;
                }
                skillPoints++;
                statPoint.AttackDamage -= 1;
                break;
            case 4: // AttackSpeed
                if (statPoint.AttackSpeed <= 0f)
                {
                    Debug.LogWarning("0이하로 낮출수 없습니다.");
                    return;
                }
                skillPoints++;
                statPoint.AttackSpeed -= 1f;
                break;
            case 5: // RecoverTime
                if (statPoint.RecoverTime <= 0f)
                {
                    Debug.LogWarning("0이하로 낮출수 없습니다.");
                    return;
                }
                skillPoints++;
                statPoint.RecoverTime -= 1f;
                break;
            case 6: // SkillDamage
                if (statPoint.SkillDamage <= 0f)
                {
                    Debug.LogWarning("0이하로 낮출수 없습니다.");
                    return;
                }
                skillPoints++;
                statPoint.SkillDamage -= 1;
                break;
            case 7: // SkillCooldown
                if (statPoint.SkillCooldown <= 0f)
                {
                    Debug.LogWarning("0이하로 낮출수 없습니다.");
                    return;
                }
                skillPoints++;
                statPoint.SkillCooldown -= 1f;
                break;
            default:
                Debug.LogWarning("알수 없는 스탯: " + index);
                return;
        }
    }

    public void ResetStats()
    {
        statPoint = new StatPoint();
        skillPoints = 10;
        level = 1;
        exp = 0;
        expToNextLevel = 20;
        protectedTargetHP = 100f;
    }
}
