using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoadUIUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] slots;
    [SerializeField] private Button[] slotButtons;
    [SerializeField] private GameObject loadCheckUI;

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
        if (!SaveManager.Instance.HasSave(slot)) return;
        else
        {
            slotSelected = slot;
            ToggleLoadCheckUI();
        }
    }

    private void UpdateUI(int slot)
    {
        Tuple<GameData.StageData, string> previewData = SaveManager.Instance.GetPreview(slot);
        if (previewData == null)
        {
            slots[slot].text = "µ•¿Ã≈Õ æ¯¿Ω";
            return;
        }

        string state = "≥∑";
        if (previewData.Item1.currentState == GameState.Night) state = "π„";

        string text = $"{previewData.Item1.currentDayIndex}¿œ¬˜ {state}\nPlayer: {previewData.Item2}";
        slots[slot].text = text;
    }

    public void ToggleLoadCheckUI()
    {
        loadCheckUI.SetActive(!loadCheckUI.activeSelf);
    }
    public void ConfirmLoad()
    {
        ToggleLoadCheckUI();
        SaveManager.Instance.Load(slotSelected);
    }
}
