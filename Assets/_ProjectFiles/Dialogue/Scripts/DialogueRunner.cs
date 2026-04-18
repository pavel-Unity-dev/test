using UnityEngine;

public class DialogueRunner : MonoBehaviour
{
    [SerializeField] private PlayerStateController playerStateController;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private FetchQuestController fetchQuestController;
    [SerializeField] private QuestRequestResolver questRequestResolver;
    [SerializeField] private QuestTrackerUI questTrackerUI;

    private DialogueData currentDialogue;
    private DialogueNodeData currentNode;

    public bool IsDialogueRunning => currentDialogue != null;

    public void StartDialogue(DialogueData dialogueData)
    {
        if (dialogueData == null)
            return;

        currentDialogue = dialogueData;
        currentNode = currentDialogue.GetNodeById(currentDialogue.startNodeId);

        if (currentNode == null)
        {
            EndDialogue();
            return;
        }

        if (playerStateController != null)
            playerStateController.SetMode(PlayerMode.Dialogue);

        ShowCurrentNode();
    }

    private void ShowCurrentNode()
    {
        if (currentNode == null)
        {
            EndDialogue();
            return;
        }

        if (dialogueUI != null)
        {
            dialogueUI.ShowNode(currentNode, OnChoiceSelected);
        }
    }

    private void OnChoiceSelected(int choiceIndex)
    {
        if (currentNode == null)
            return;

        if (choiceIndex < 0 || choiceIndex >= currentNode.choices.Count)
            return;

        DialogueChoiceData selectedChoice = currentNode.choices[choiceIndex];

        if (selectedChoice.endsDialogue)
        {
            bool shouldStartQuest = currentDialogue != null && currentDialogue.startsFetchQuestOnEnd;
            EndDialogue();

            if (shouldStartQuest)
            {
                StartFetchQuestFromDialogue();
            }

            return;
        }

        DialogueNodeData nextNode = currentDialogue.GetNodeById(selectedChoice.nextNodeId);

        if (nextNode == null)
        {
            EndDialogue();
            return;
        }

        currentNode = nextNode;
        ShowCurrentNode();
    }

    public void EndDialogue()
    {
        currentDialogue = null;
        currentNode = null;

        if (dialogueUI != null)
            dialogueUI.Hide();

        if (playerStateController != null)
            playerStateController.SetMode(PlayerMode.Gameplay);
    }

    private void StartFetchQuestFromDialogue()
    {
        if (fetchQuestController == null || questRequestResolver == null)
            return;

        if (fetchQuestController.IsQuestActive && !fetchQuestController.IsQuestCompleted)
            return;

        ItemData requestedItem = questRequestResolver.GetRandomQuestItem();

        if (requestedItem == null)
            return;

        fetchQuestController.StartQuest(requestedItem);

        if (questTrackerUI != null)
        {
            questTrackerUI.ShowQuest(requestedItem);
        }
    }
}