using UnityEngine;

public class HeldItemController : MonoBehaviour
{
    [SerializeField] private PlayerStateController playerStateController;
    [SerializeField] private InspectItemUI inspectItemUI;
    [SerializeField] private InspectionConfig inspectionConfig;
    [SerializeField] private Transform inspectAnchor;
    [SerializeField] private Transform heldAnchor;

    private WorldItem inspectingItem;
    private WorldItem currentHeldItem;

    private bool waitingForInteractRelease;

    public WorldItem CurrentHeldItem => currentHeldItem;
    public WorldItem InspectingItem => inspectingItem;

    private void Update()
    {
        if (playerStateController == null)
            return;

        if (playerStateController.CurrentMode == PlayerMode.InspectingItem && inspectingItem != null)
        {
            KeepInspectItemLockedToAnchor();

            if (waitingForInteractRelease)
            {
                if (!Input.GetKey(KeyCode.E))
                {
                    waitingForInteractRelease = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ConfirmTakeInspectingItem();
                    return;
                }
            }

            RotateInspectingItem();
        }
    }

    public void StartInspecting(WorldItem item)
    {
        if (item == null)
            return;

        if (inspectingItem != null || currentHeldItem != null)
            return;

        inspectingItem = item;
        item.SetState(ItemState.Inspecting);

        waitingForInteractRelease = true;

        if (inspectAnchor != null && inspectionConfig != null)
        {
            item.transform.SetParent(inspectAnchor);
            item.transform.localPosition = inspectionConfig.inspectLocalPosition;
            item.transform.localRotation = Quaternion.Euler(inspectionConfig.inspectLocalEulerAngles);
            item.transform.localScale = Vector3.one;
        }

        ResetItemVisualRotation(item);

        if (inspectItemUI != null)
        {
            inspectItemUI.Show(item.ItemData);
        }

        if (playerStateController != null)
        {
            playerStateController.SetMode(PlayerMode.InspectingItem);
        }
    }

    public void ConfirmTakeInspectingItem()
    {
        if (inspectingItem == null)
            return;

        currentHeldItem = inspectingItem;
        inspectingItem = null;

        currentHeldItem.SetState(ItemState.Held);

        if (heldAnchor != null && inspectionConfig != null)
        {
            currentHeldItem.transform.SetParent(heldAnchor);
            currentHeldItem.transform.localPosition = inspectionConfig.heldLocalPosition;
            currentHeldItem.transform.localRotation = Quaternion.Euler(inspectionConfig.heldLocalEulerAngles);
            currentHeldItem.transform.localScale = Vector3.one;
        }

        ResetItemVisualRotation(currentHeldItem);

        if (inspectItemUI != null)
        {
            inspectItemUI.Hide();
        }

        if (playerStateController != null)
        {
            playerStateController.SetMode(PlayerMode.Gameplay);
        }
    }

    public void PlaceHeldItemIntoSocket(ItemSocket socket)
    {
        if (currentHeldItem == null || socket == null)
            return;

        currentHeldItem.transform.SetParent(socket.SocketPoint);
        currentHeldItem.transform.localPosition = Vector3.zero;
        currentHeldItem.transform.localRotation = Quaternion.identity;
        currentHeldItem.transform.localScale = Vector3.one;

        ResetItemVisualRotation(currentHeldItem);

        currentHeldItem.SetState(ItemState.InSocket);
        currentHeldItem.SetCurrentSocket(socket);
        socket.SetItem(currentHeldItem);

        currentHeldItem = null;
    }

    private void RotateInspectingItem()
    {
        if (inspectingItem == null || inspectionConfig == null)
            return;

        if (!Input.GetMouseButton(0))
            return;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Transform target = inspectingItem.VisualRoot != null
            ? inspectingItem.VisualRoot
            : inspectingItem.transform;

        target.Rotate(Vector3.up, -mouseX * inspectionConfig.inspectRotationSpeed * Time.deltaTime, Space.Self);
        target.Rotate(Vector3.right, mouseY * inspectionConfig.inspectRotationSpeed * Time.deltaTime, Space.Self);
    }

    private void KeepInspectItemLockedToAnchor()
    {
        if (inspectingItem == null || inspectAnchor == null || inspectionConfig == null)
            return;

        inspectingItem.transform.SetParent(inspectAnchor);
        inspectingItem.transform.localPosition = inspectionConfig.inspectLocalPosition;
        inspectingItem.transform.localRotation = Quaternion.Euler(inspectionConfig.inspectLocalEulerAngles);
        inspectingItem.transform.localScale = Vector3.one;
    }

    private void ResetItemVisualRotation(WorldItem item)
    {
        if (item == null)
            return;

        if (item.VisualRoot != null)
        {
            item.VisualRoot.localRotation = Quaternion.identity;
        }
    }

    public bool HasHeldItem()
    {
        return currentHeldItem != null;
    }

    public bool IsHoldingKey()
    {
        if (currentHeldItem == null)
            return false;

        if (currentHeldItem.ItemData == null)
            return false;

        return currentHeldItem.ItemData.isKey || currentHeldItem.ItemData.itemKind == ItemKind.Key;
    }

    public WorldItem GetHeldItem()
    {
        return currentHeldItem;
    }

    public void ConsumeHeldItem()
    {
        if (currentHeldItem == null)
            return;

        currentHeldItem.SetState(ItemState.Consumed);
        currentHeldItem.gameObject.SetActive(false);
        currentHeldItem = null;
    }
}