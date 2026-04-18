using UnityEngine;

public class DialogueInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private string promptText = "E Ч поговорить";

    public InteractionPromptData GetPromptData()
    {
        if (dialogueRunner != null && dialogueRunner.IsDialogueRunning)
            return new InteractionPromptData(false, string.Empty);

        return new InteractionPromptData(true, promptText);
    }

    public void OnInteractPressed()
    {
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
}