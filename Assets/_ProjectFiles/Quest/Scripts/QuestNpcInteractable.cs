using UnityEngine;

public class QuestNpcInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private FetchQuestController fetchQuestController;
    [SerializeField] private HeldItemController heldItemController;

    [SerializeField] private string talkPromptText = "E Ч поговорить";
    [SerializeField] private string giveItemPromptText = "E Ч отдать предмет";

    public InteractionPromptData GetPromptData()
    {
        if (dialogueRunner != null && dialogueRunner.IsDialogueRunning)
            return new InteractionPromptData(false, string.Empty);

        if (CanGiveRequestedItem())
            return new InteractionPromptData(true, giveItemPromptText);

        return new InteractionPromptData(true, talkPromptText);
    }

    public void OnInteractPressed()
    {
        if (CanGiveRequestedItem())
        {
            GiveRequestedItem();
            return;
        }

        if (dialogueRunner == null || dialogueData == null)
            return;

        if (dialogueRunner.IsDialogueRunning)
            return;

        dialogueRunner.StartDialogue(dialogueData);
    }

    public void OnInteractHeld(float deltaTime)
    {
    }

    public void OnInteractReleased()
    {
    }

    private bool CanGiveRequestedItem()
    {
        if (fetchQuestController == null || heldItemController == null)
            return false;

        if (!fetchQuestController.IsQuestActive || fetchQuestController.IsQuestCompleted)
            return false;

        WorldItem heldItem = heldItemController.GetHeldItem();

        if (heldItem == null)
            return false;

        return fetchQuestController.IsRequestedItem(heldItem);
    }

    private void GiveRequestedItem()
    {
        WorldItem heldItem = heldItemController.GetHeldItem();

        if (heldItem == null)
            return;

        heldItemController.ConsumeHeldItem();
        fetchQuestController.CompleteQuest();
    }
}