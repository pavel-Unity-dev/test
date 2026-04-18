using UnityEngine;

public class ValveInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private ValveConfig valveConfig;
    [SerializeField] private ValveDoorLink valveDoorLink;
    [SerializeField] private string promptText = "E Ч крутить";

    private float currentProgress;
    private bool isHolding;

    private void Update()
    {
        if (valveConfig == null || valveDoorLink == null)
            return;

        if (!isHolding && currentProgress > 0f)
        {
            currentProgress -= valveConfig.returnSpeed * Time.deltaTime;
            currentProgress = Mathf.Clamp(currentProgress, 0f, valveConfig.maxProgress);

            valveDoorLink.ApplyProgress(currentProgress);
        }
    }

    public InteractionPromptData GetPromptData()
    {
        return new InteractionPromptData(true, promptText);
    }

    public void OnInteractPressed()
    {
        isHolding = true;
    }

    public void OnInteractHeld(float deltaTime)
    {
        if (valveConfig == null || valveDoorLink == null)
            return;

        isHolding = true;

        currentProgress += valveConfig.holdFillSpeed * deltaTime;
        currentProgress = Mathf.Clamp(currentProgress, 0f, valveConfig.maxProgress);

        valveDoorLink.ApplyProgress(currentProgress);
    }

    public void OnInteractReleased()
    {
        isHolding = false;
    }
}