using UnityEngine;

public class ValveDoorLink : MonoBehaviour
{
    [SerializeField] private Transform valveVisual;
    [SerializeField] private Transform doorVisual;
    [SerializeField] private ValveConfig valveConfig;

    private Quaternion valveStartRotation;
    private Vector3 doorStartPosition;

    private void Awake()
    {
        if (valveVisual != null)
            valveStartRotation = valveVisual.localRotation;

        if (doorVisual != null)
            doorStartPosition = doorVisual.localPosition;
    }

    public void ApplyProgress(float progress)
    {
        if (valveConfig == null)
            return;

        float normalizedProgress = Mathf.Clamp01(progress / valveConfig.maxProgress);

        if (valveVisual != null)
        {
            float angle = normalizedProgress * valveConfig.maxValveAngle;
            valveVisual.localRotation = valveStartRotation * Quaternion.Euler(0f, 0f, -angle);
        }

        if (doorVisual != null)
        {
            float doorOffset = normalizedProgress * valveConfig.maxDoorHeight;
            doorVisual.localPosition = doorStartPosition + new Vector3(0f, doorOffset, 0f);
        }
    }
}