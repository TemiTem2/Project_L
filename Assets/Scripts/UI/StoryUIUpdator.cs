using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryUIUpdator : MonoBehaviour
{
    [Header("Story")]
    [SerializeField] private Image imageCharacter;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI storyText;

    [Header("Choice Panel")]
    [SerializeField] private GameObject panelChoiceTwo;
    [SerializeField] private TextMeshProUGUI textChoice0;
    [SerializeField] private TextMeshProUGUI textChoice1;

    [Header("Reward Panel")]
    [SerializeField] private GameObject panelReward;
    [SerializeField] private TextMeshProUGUI textReward;

    [Header("Progress")]
    [SerializeField] private TextMeshProUGUI textProgress;

    private Database database => Database.Instance;

    private void Start()
    {
        textProgress.text = GameManager.Instance.currentDayIndex + "ÀÏÂ÷ ³·";
    }

    public void UpdateUI(string name, string text, Sprite Image)
    {
        nameText.text = name;
        storyText.text = text;
        if(Image != null)
        {
            imageCharacter.sprite = Image;
            imageCharacter.color = new Color(255, 255, 255, 255);
            return;
        }
        else
        {
            if(database.currentCharInfo == null)
            {
                imageCharacter.color = new Color(0, 0, 0, 0);
                return;
            }
            else
            {
                imageCharacter.sprite = database.currentCharInfo.charIcon;
                imageCharacter.color = new Color(255,255,255,255);
                return;
            }
                
        }
    }

    public void EnableChoice(string choice0, string choice1)
    {
        panelChoiceTwo.SetActive(true);
        textChoice0.text = choice0;
        textChoice1.text = choice1;
    }

    public void DisableChoice()
    {
        panelChoiceTwo.SetActive(false);
    }

    public void ShowRewardPanel(RewardType reward, int amount)
    {
        panelReward.SetActive(true);
        textReward.text = $"{reward}À» {amount}¸¸Å­ È¹µæÇß½À´Ï´Ù!";
    }
    public void HideRewardPanel()
    {
        panelReward.SetActive(false);
    }
}
