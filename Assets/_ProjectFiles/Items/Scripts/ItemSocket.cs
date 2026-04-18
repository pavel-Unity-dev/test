using UnityEngine;

public class ItemSocket : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform socketPoint;
    [SerializeField] private WorldItem currentItem;
    [SerializeField] private SocketType socketType = SocketType.Home;

    public Transform SocketPoint => socketPoint;
    public WorldItem CurrentItem => currentItem;
    public SocketType SocketType => socketType;

    public bool IsEmpty()
    {
        return currentItem == null;
    }

    public void SetItem(WorldItem item)
    {
        currentItem = item;
    }

    public void RemoveItem()
    {
        currentItem = null;
    }

    public InteractionPromptData GetPromptData()
    {
        HeldItemController heldItemController = FindAnyObjectByType<HeldItemController>();

        if (heldItemController == null)
            return new InteractionPromptData(false, string.Empty);

        WorldItem heldItem = heldItemController.CurrentHeldItem;

        if (heldItem == null)
            return new InteractionPromptData(false, string.Empty);

        if (!IsEmpty())
            return new InteractionPromptData(false, string.Empty);

        if (!CanAcceptItem(heldItem))
            return new InteractionPromptData(false, string.Empty);

        return new InteractionPromptData(true, "E Ч положить");
    }

    public void OnInteractPressed()
    {
        HeldItemController heldItemController = FindAnyObjectByType<HeldItemController>();

        if (heldItemController == null)
            return;

        WorldItem heldItem = heldItemController.CurrentHeldItem;

        if (heldItem == null)
            return;

        if (!IsEmpty())
            return;

        if (!CanAcceptItem(heldItem))
            return;

        heldItemController.PlaceHeldItemIntoSocket(this);
    }

    public void OnInteractHeld(float deltaTime)
    {
    }

    public void OnInteractReleased()
    {
    }

    public bool CanAcceptItem(WorldItem item)
    {
        if (item == null)
            return false;

        if (socketType == SocketType.Universal)
            return true;

        if (socketType == SocketType.Home)
            return item.HomeSocket == this;

        return false;
    }
}