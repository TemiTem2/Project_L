using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SelectingChar : MonoBehaviour
{
    [SerializeField] string charName;

    Database database;
    SelectingSkill selectingSkill;

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
        charImage.sprite = database.currentCharInfo.charIcon;
        charNameText.text = database.currentCharInfo.charName;
        charDescriptionText.text = database.currentCharInfo.charDesc;
        selectingSkill.LoadCharSkill();
    }
}
