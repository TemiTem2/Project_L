using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerStateUI : MonoBehaviour
{
    private StateManager stateManager;
    private PlayerStatManager playerStatManager;
    [SerializeField] Image[] gauges;
    [SerializeField] TextMeshProUGUI Lvtext;
    private float expNextLevel;
    void Start()
    {
        stateManager = FindFirstObjectByType<StateManager>();
        playerStatManager = PlayerStatManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        expNextLevel = playerStatManager.expToNextLevel;
        gauges[0].fillAmount = stateManager.hp / stateManager.Maxhp;
        gauges[1].fillAmount = playerStatManager.exp / expNextLevel;
        Lvtext.text = playerStatManager.level.ToString();
    }
}
