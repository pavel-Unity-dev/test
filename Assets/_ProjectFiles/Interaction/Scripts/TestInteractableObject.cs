using UnityEngine;

public class TestInteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private string promptText = "E — нажать";

    public InteractionPromptData GetPromptData()
    {
        return new InteractionPromptData(true, promptText);
    }

    public void OnInteractPressed()
    {
        Debug.Log("Вы взаимодействовали с объектом: " + gameObject.name);
    }

    public void OnInteractHeld(float deltaTime)
    {
    }

    public void OnInteractReleased()
    {
    }
}