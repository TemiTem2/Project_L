using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryUIUpdator : MonoBehaviour
{
    [SerializeField]
    private Image imageCharacter;
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI storyText;
    [SerializeField]
    private GameObject panelChoiceTwo;
    [SerializeField]
    private TextMeshProUGUI textChoice0;
    [SerializeField]
    private TextMeshProUGUI textChoice1;

    [SerializeField]
    private GameObject panelReward;
    [SerializeField]
    private TextMeshProUGUI textReward;

    [SerializeField]
    private TextMeshProUGUI textProgress;

    private void Start()
    {
        textProgress.text = GameManager.Instance.currentDayIndex + "ÀÏÂ÷ ³·";
    }

    public void UpdateUI(string name, string text, Sprite Image)
    {
        nameText.text = name;
        storyText.text = text;
        imageCharacter.sprite = Image;
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
