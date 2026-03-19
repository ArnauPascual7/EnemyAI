using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    public InputSystem_Actions InputActions { get; private set; }

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool SprintInput { get; private set; }

    private void OnEnable()
    {
        InputActions = new InputSystem_Actions();
        InputActions.Player.Enable();
        InputActions.Player.SetCallbacks(this);
    }

    private void OnDisable()
    {
        InputActions.Player.Disable();
        InputActions.Player.RemoveCallbacks(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        SprintInput = context.ReadValueAsButton();
    }
}
