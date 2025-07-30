using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    public int level = 1;
    public int exp;
    public int expToNextLevel=100;
    public int skillPoints;

    public StatPoint statPoint = new StatPoint();

    public static PlayerStatManager instance = null;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this; 
            DontDestroyOnLoad(gameObject); 
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
        Debug.Log("스탯 다운" + index);
    }

}
