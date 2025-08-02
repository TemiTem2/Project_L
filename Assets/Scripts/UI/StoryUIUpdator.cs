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
}
