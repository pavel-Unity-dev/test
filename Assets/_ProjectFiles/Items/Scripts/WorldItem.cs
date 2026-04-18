using UnityEngine;

public class WorldItem : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private ItemSocket homeSocket;
    [SerializeField] private Transform visualRoot;

    private ItemState currentState = ItemState.InSocket;
    private ItemSocket currentSocket;

    public ItemData ItemData => itemData;
    public ItemSocket HomeSocket => homeSocket;
    public ItemState CurrentState => currentState;
    public Transform VisualRoot => visualRoot;
    public ItemSocket CurrentSocket => currentSocket;

    private void Start()
    {
        currentSocket = homeSocket;
    }

    public void SetState(ItemState newState)
    {
        currentState = newState;
    }

    public void SetHomeSocket(ItemSocket socket)
    {
        homeSocket = socket;
    }

    public void SetCurrentSocket(ItemSocket socket)
    {
        currentSocket = socket;
    }

    public InteractionPromptData GetPromptData()
    {
        if (currentState != ItemState.InSocket)
            return new InteractionPromptData(false, string.Empty);

        return new InteractionPromptData(true, "E Ч осмотреть");
    }

    public void OnInteractPressed()
    {
        if (currentState != ItemState.InSocket)
            return;

        HeldItemController heldItemController = FindAnyObjectByType<HeldItemController>();

        if (heldItemController == null)
            return;

        if (currentSocket != null)
        {
            currentSocket.RemoveItem();
            currentSocket = null;
        }

        heldItemController.StartInspecting(this);
    }

    public void OnInteractHeld(float deltaTime)
    {
    }

    public void OnInteractReleased()
    {
    }

    public bool IsKey()
    {
        if (itemData == null)
            return false;

        return itemData.isKey || itemData.itemKind == ItemKind.Key;
    }
}   