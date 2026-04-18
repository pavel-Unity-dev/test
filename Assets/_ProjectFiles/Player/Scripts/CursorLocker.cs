using UnityEngine;

public class CursorLocker : MonoBehaviour
{
    [SerializeField] private PlayerStateController playerStateController;

    private void Start()
    {
        ApplyCursorState();
    }

    private void OnEnable()
    {
        if (playerStateController != null)
        {
            playerStateController.OnModeChanged += HandleModeChanged;
        }
    }

    private void OnDisable()
    {
        if (playerStateController != null)
        {
            playerStateController.OnModeChanged -= HandleModeChanged;
        }
    }

    private void HandleModeChanged(PlayerMode newMode)
    {
        ApplyCursorState();
    }

    private void ApplyCursorState()
    {
        if (playerStateController == null)
            return;

        if (playerStateController.CurrentMode == PlayerMode.Gameplay)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}