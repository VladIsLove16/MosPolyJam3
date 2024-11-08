using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; } // Экспонируем направление движения для других компонентов

    private PlayerInputActions playerInput;

    private void OnEnable()
    {
        playerInput = new PlayerInputActions();
        playerInput.Player.Enable();
        playerInput.Player.Move.performed += OnMove;
        playerInput.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        playerInput.Player.Move.performed -= OnMove;
        playerInput.Player.Move.canceled -= OnMove;
        playerInput.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }
}
