public interface IInteractable
{
    InteractionPromptData GetPromptData();

    void OnInteractPressed();
    void OnInteractHeld(float deltaTime);
    void OnInteractReleased();
}