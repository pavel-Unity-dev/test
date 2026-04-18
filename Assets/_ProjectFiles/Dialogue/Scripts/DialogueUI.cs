using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private TextMeshProUGUI npcText;
    [SerializeField] private Button choiceButtonPrefab;
    [SerializeField] private Transform choicesContainer;

    private readonly List<Button> spawnedButtons = new List<Button>();

    private void Awake()
    {
        Hide();
    }

    public void ShowNode(DialogueNodeData nodeData, Action<int> onChoiceSelected)
    {
        if (nodeData == null)
            return;

        if (root != null)
            root.SetActive(true);

        if (npcText != null)
            npcText.text = nodeData.npcText;

        ClearChoices();

        for (int i = 0; i < nodeData.choices.Count; i++)
        {
            int choiceIndex = i;
            DialogueChoiceData choiceData = nodeData.choices[i];

            Button buttonInstance = Instantiate(choiceButtonPrefab, choicesContainer);
            spawnedButtons.Add(buttonInstance);

            TextMeshProUGUI buttonText = buttonInstance.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
                buttonText.text = choiceData.choiceText;

            buttonInstance.onClick.AddListener(() => onChoiceSelected?.Invoke(choiceIndex));
        }
    }

    public void Hide()
    {
        ClearChoices();

        if (root != null)
            root.SetActive(false);
    }

    private void ClearChoices()
    {
        for (int i = 0; i < spawnedButtons.Count; i++)
        {
            if (spawnedButtons[i] != null)
                Destroy(spawnedButtons[i].gameObject);
        }

        spawnedButtons.Clear();
    }
}