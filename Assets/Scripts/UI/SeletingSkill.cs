using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectingSkill : MonoBehaviour
{
    Database database;
    [SerializeField] Button[] skillButtons;
    [SerializeField] GameObject skillPanel;
    [SerializeField] TextMeshProUGUI SkillNameText;
    [SerializeField] TextMeshProUGUI SkillDescriptionText;
    void Start()
    {
        database = Database.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadCharSkill()
    {
        skillPanel.SetActive(true);
        for (int i = 0; i < database.currentCharInfo.skills.Length; i++)
        {
            skillButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = database.currentCharInfo.skills[i].skillname;
            int index = i; 
            skillButtons[i].onClick.AddListener(() => SelectSkill(index));
        }
        SkillNameText.text = database.currentSkillInfo.skillname;
        SkillDescriptionText.text = database.currentSkillInfo.skillDesc;
    }

    void SelectSkill(int index)
    {
        database.currentPlayerSkill = database.currentCharInfo.skills[index].skillname;
        database.LoadSkillData();
        SkillNameText.text = database.currentSkillInfo.skillname;
        SkillDescriptionText.text = database.currentSkillInfo.skillDesc;
    }
}
