using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private PlayerStateController stateController;
    [SerializeField] private InteractionPromptUI promptUI;
    [SerializeField] private InteractionConfig interactionConfig;

    private IInteractable currentInteractable;
    private IInteractable heldInteractable;

    private void Update()
    {
        if (playerCamera == null || stateController == null || promptUI == null || interactionConfig == null)
            return;

        if (!stateController.IsGameplay())
        {
            ReleaseHeldInteractableIfNeeded();
            currentInteractable = null;
            promptUI.Hide();
            return;
        }

        UpdateCurrentInteractable();

        if (heldInteractable != null && currentInteractable != heldInteractable)
        {
            ReleaseHeldInteractableIfNeeded();
        }

        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            heldInteractable = currentInteractable;
            heldInteractable.OnInteractPressed();
        }

        if (Input.GetKey(KeyCode.E) && heldInteractable != null)
        {
            heldInteractable.OnInteractHeld(Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            ReleaseHeldInteractableIfNeeded();
        }
    }

    private void UpdateCurrentInteractable()
    {
        currentInteractable = null;
        promptUI.Hide();

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionConfig.interactionDistance, interactionConfig.interactableMask))
        {
            IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                currentInteractable = interactable;

                InteractionPromptData promptData = currentInteractable.GetPromptData();

                if (promptData != null && promptData.IsVisible)
                {
                    promptUI.Show(promptData.Text);
                }
            }
        }
    }

    private void ReleaseHeldInteractableIfNeeded()
    {
        if (heldInteractable == null)
            return;

        heldInteractable.OnInteractReleased();
        heldInteractable = null;
    }
}