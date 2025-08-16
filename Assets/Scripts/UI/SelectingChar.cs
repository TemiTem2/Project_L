using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SelectingChar : MonoBehaviour
{
    [SerializeField] string charName;

    Database database;
    SelectingSkill selectingSkill;

    // UI Elements
    [SerializeField] GameObject charSelectPanel;
    [SerializeField] Image charImage;
    [SerializeField] TextMeshProUGUI charNameText;
    [SerializeField] TextMeshProUGUI charDescriptionText;


    void Start()
    {
        database = Database.Instance;
        selectingSkill = FindFirstObjectByType<SelectingSkill>();
    }

    public void SelectChar()
    {
        database.currentPlayCharName = charName;
        database.LoadPlayerData();
        database.currentPlayerSkill = database.currentCharInfo.skills[0].skillname;
        database.LoadSkillData();
        charImage.sprite = database.currentCharInfo.charIcon;
        charNameText.text = database.currentCharInfo.charName;
        charDescriptionText.text = database.currentCharInfo.charDesc;
        charSelectPanel.SetActive(true);
        selectingSkill.LoadCharSkill();
    }
}
