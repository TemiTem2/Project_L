using UnityEngine;
using UnityEngine.UI;
public class CoolDownUI : MonoBehaviour
{
    PlayerMove player;
    [SerializeField] StateManager stateManager;
    [SerializeField] Image[] cooldownIcon;

    void Update()
    {
        if (player == null)
        {
            Debug.Log("PlayerMove not found, searching...");
            player = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        }
        else
        {
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        cooldownIcon[0].fillAmount = player.currentAttackCooldown;
        cooldownIcon[1].fillAmount = player.currentSkill1Cooldown/stateManager.skill.cooldown;
    }
}
