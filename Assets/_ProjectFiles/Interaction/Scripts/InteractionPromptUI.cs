using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;

    private void Awake()
    {
        Hide();
    }

    public void Show(string text)
    {
        if (promptText == null)
            return;

        promptText.text = text;
        promptText.gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (promptText == null)
            return;

        promptText.text = string.Empty;
        promptText.gameObject.SetActive(false);
    }
}