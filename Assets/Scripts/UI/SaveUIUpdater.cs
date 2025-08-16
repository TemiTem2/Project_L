using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveUIUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] slots;
    [SerializeField] private Button[] slotButtons;
    [SerializeField] private GameObject saveCheckUI;

    private int slotSelected;

    private void OnEnable()
    {
        for (int slot = 0; slot < slots.Length; slot++)
        {
            int slotIndex = slot;
            slotButtons[slot].onClick.AddListener(() => OnSlotClicked(slotIndex));
            UpdateUI(slot);
        }
    }

    private void OnSlotClicked(int slot)
    {
        CheckData(slot);
    }

    private void CheckData(int slot)
    {
        if (!SaveManager.Instance.HasSave(slot)) UpdateUI(slot);
        else
        {
            slotSelected = slot;
            ToggleSaveCheckUI();
        }
    }

    //private void CheckData(int slot)
    //{
    //    Tuple<GameData.StageData, string> previewData = SaveManager.Instance.GetPreview(slot);
    //    if (previewData == null)
    //    {
    //        slots[slot].text = "������ ����";
    //        return;
    //    }
    //}

    private void UpdateUI(int slot)
    {
        SaveManager.Instance.Save(slot);

        Tuple<GameData.StageData, string> previewData = SaveManager.Instance.GetPreview(slot);
        if (previewData == null)
        {
            slots[slot].text = "������ ����";
            return;
        }

        string state = "��";
        if (previewData.Item1.currentState == GameState.Night) state = "��";

        string text = $"{previewData.Item1.currentDayIndex}���� {state}\nPlayer: {previewData.Item2}";
        slots[slot].text = text;
    }

    public void ToggleSaveCheckUI()
    {
        saveCheckUI.SetActive(!saveCheckUI.activeSelf);
    }
    public void ConfirmUpdate()
    {
        UpdateUI(slotSelected);
    }
}
