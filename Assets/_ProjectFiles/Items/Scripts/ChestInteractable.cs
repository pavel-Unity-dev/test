using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform lidTransform;
    [SerializeField] private Vector3 openedLidEulerAngles = new Vector3(-110f, 0f, 0f);
    [SerializeField] private string promptOpenText = "E — открыть";
    [SerializeField] private string promptLockedText = "Нужен ключ";

    private bool isOpened;
    private Quaternion closedRotation;
    private Quaternion openedRotation;

    private void Awake()
    {
        if (lidTransform != null)
        {
            closedRotation = lidTransform.localRotation;
            openedRotation = Quaternion.Euler(openedLidEulerAngles);
        }
    }

    public InteractionPromptData GetPromptData()
    {
        if (isOpened)
            return new InteractionPromptData(false, string.Empty);

        HeldItemController heldItemController = FindAnyObjectByType<HeldItemController>();

        if (heldItemController == null)
            return new InteractionPromptData(false, string.Empty);

        if (heldItemController.IsHoldingKey())
            return new InteractionPromptData(true, promptOpenText);

        return new InteractionPromptData(true, promptLockedText);
    }

    public void OnInteractPressed()
    {
        if (isOpened)
            return;

        HeldItemController heldItemController = FindAnyObjectByType<HeldItemController>();

        if (heldItemController == null)
            return;

        if (!heldItemController.IsHoldingKey())
            return;

        OpenChest();
        heldItemController.ConsumeHeldItem();
    }

    public void OnInteractHeld(float deltaTime)
    {
    }

    public void OnInteractReleased()
    {
    }

    private void OpenChest()
    {
        isOpened = true;

        if (lidTransform != null)
        {
            lidTransform.localRotation = openedRotation;
        }
    }
}