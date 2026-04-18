using TMPro;
using UnityEngine;

public class InspectItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject root;

    private void Start()
    {
        HideImmediate();
    }

    public void Show(ItemData itemData)
    {
        if (itemData == null)
            return;

        if (root != null)
            root.SetActive(true);

        if (titleText != null)
            titleText.text = itemData.displayName;

        if (descriptionText != null)
            descriptionText.text = itemData.description;
    }

    public void Hide()
    {
        if (titleText != null)
            titleText.text = string.Empty;

        if (descriptionText != null)
            descriptionText.text = string.Empty;

        if (root != null)
            root.SetActive(false);
    }

    private void HideImmediate()
    {
        if (root != null)
            root.SetActive(false);
    }
}