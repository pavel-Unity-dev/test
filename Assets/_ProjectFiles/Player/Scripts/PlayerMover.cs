using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private PlayerInputReader inputReader;
    [SerializeField] private PlayerStateController stateController;
    [SerializeField] private PlayerMovementConfig movementConfig;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (inputReader == null || stateController == null || movementConfig == null)
            return;

        if (!stateController.IsGameplay())
            return;

        Vector2 moveInput = inputReader.MoveInput;

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        move *= movementConfig.moveSpeed;

        characterController.Move(move * Time.deltaTime);
    }
}