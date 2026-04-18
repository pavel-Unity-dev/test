using TMPro;
using UnityEngine;

public class QuestTrackerUI : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private TextMeshProUGUI questText;

    private void Awake()
    {
        Hide();
    }

    public void ShowQuest(ItemData itemData)
    {
        if (itemData == null)
            return;

        if (root != null)
            root.SetActive(true);

        if (questText != null)
            questText.text = "[ ] Принести: " + itemData.displayName;
    }

    public void MarkCompleted(ItemData itemData)
    {
        if (itemData == null)
            return;

        if (root != null)
            root.SetActive(true);

        if (questText != null)
            questText.text = "[x] Принести: " + itemData.displayName;
    }

    public void Hide()
    {
        if (questText != null)
            questText.text = string.Empty;

        if (root != null)
            root.SetActive(false);
    }
}