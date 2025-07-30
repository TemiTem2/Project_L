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
                    Debug.LogWarning("�˼� ���� ����: " + index);
                    return;
            }
        }
        else
        {
            Debug.LogWarning("��ų ����Ʈ�� �����մϴ�.");
        }
    }

    public void StatDown(int index)
    {
        Debug.Log("���� �ٿ�" + index);
    }

}
