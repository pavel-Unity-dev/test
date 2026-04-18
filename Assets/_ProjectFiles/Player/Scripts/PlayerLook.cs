using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private PlayerInputReader inputReader;
    [SerializeField] private PlayerStateController stateController;
    [SerializeField] private PlayerLookConfig lookConfig;
    [SerializeField] private Transform cameraPivot;

    private float pitch;

    private void Update()
    {
        if (inputReader == null || stateController == null || lookConfig == null || cameraPivot == null)
            return;

        if (!stateController.IsGameplay())
            return;

        Vector2 lookInput = inputReader.LookInput;

        float mouseX = lookInput.x * lookConfig.sensitivityX;
        float mouseY = lookInput.y * lookConfig.sensitivityY;

        transform.Rotate(Vector3.up * mouseX);

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, lookConfig.minPitch, lookConfig.maxPitch);

        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}