using UnityEngine;

public class LeverInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform leverVisual;
    [SerializeField] private LeverTarget leverTarget;
    [SerializeField] private Vector3 offEulerAngles = new Vector3(0f, 0f, 25f);
    [SerializeField] private Vector3 onEulerAngles = new Vector3(0f, 0f, -25f);
    [SerializeField] private string promptText = "E Ч переключить";

    private bool isOn;

    private void Start()
    {
        UpdateVisual();
        ApplyTarget();
    }

    public InteractionPromptData GetPromptData()
    {
        return new InteractionPromptData(true, promptText);
    }

    public void OnInteractPressed()
    {
        isOn = !isOn;
        UpdateVisual();
        ApplyTarget();
    }

    public void OnInteractHeld(float deltaTime)
    {
    }

    public void OnInteractReleased()
    {
    }

    private void UpdateVisual()
    {
        if (leverVisual == null)
            return;

        leverVisual.localRotation = Quaternion.Euler(isOn ? onEulerAngles : offEulerAngles);
    }

    private void ApplyTarget()
    {
        if (leverTarget != null)
        {
            leverTarget.SetState(isOn);
        }
    }
}