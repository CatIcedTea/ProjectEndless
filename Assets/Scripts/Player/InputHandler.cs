using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public InputActionAsset inputActions;

    private InputAction _movementAction;
    private InputAction _lookAction;
    private InputAction _sprintAction;
    private InputAction _crouchAction;
    private InputAction _zoomAction;
    private InputAction _attackAction;

    private PlayerMovement _playerMovement;
    private PlayerCamera _playerCamera;
    [SerializeField] PlayerAttackHandler _playerAttack;

    private void OnEnable()

    {
        inputActions.FindActionMap("Player").Enable();

        _playerMovement = GetComponent<PlayerMovement>();
        _playerCamera = GetComponent<PlayerCamera>();
    }

    private void Awake()
    {
        _movementAction = InputSystem.actions.FindAction("Movement");
        _lookAction = InputSystem.actions.FindAction("Look");
        _sprintAction = InputSystem.actions.FindAction("Sprint");
        _crouchAction = InputSystem.actions.FindAction("Crouch");
        _zoomAction = InputSystem.actions.FindAction("Zoom");
        _attackAction = InputSystem.actions.FindAction("Attack");
    }

    private void Update()
    {
        if (_sprintAction.WasPressedThisFrame())
            _playerMovement.HandleSprint(true);
        if (_sprintAction.WasReleasedThisFrame())
            _playerMovement.HandleSprint(false);

        if (_crouchAction.WasPressedThisFrame())
            _playerMovement.HandleCrouch(true);
        if (_crouchAction.WasReleasedThisFrame())
            _playerMovement.HandleCrouch(false);

        if (_zoomAction.WasPressedThisFrame())
            _playerCamera.HandleZoom(true);
        if (_zoomAction.WasReleasedThisFrame())
            _playerCamera.HandleZoom(false);

        if (_attackAction.WasPressedThisFrame())
            _playerAttack.HandleAttack();
    }

    private void FixedUpdate()
    {

        _playerMovement.HandleMovement(_movementAction.ReadValue<Vector2>());
        _playerCamera.HandleZTitlt(_movementAction.ReadValue<Vector2>().x);
    }

    private void LateUpdate()
    {
        _playerCamera.HandleCamera(_lookAction.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }
}
