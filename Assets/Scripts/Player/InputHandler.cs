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
    private InputAction _escapeAction;
    private InputAction _interactAction;

    private PlayerMovement _playerMovement;
    private PlayerCamera _playerCamera;
    private GameObject _menu;
    private GameObject _optionsMenu;
    private bool _isInMenu = false;
    private bool _isGameOver = false;
    [SerializeField] PlayerAttackHandler _playerAttack;
    [SerializeField] InteractionHandler _interactHandler;

    private AudioManager _audioManager;

    private void OnEnable()

    {
        inputActions.FindActionMap("Player").Enable();

        _playerMovement = GetComponent<PlayerMovement>();
        _playerCamera = GetComponent<PlayerCamera>();

        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Awake()
    {
        _menu = GameObject.FindGameObjectWithTag("Menu");

        _optionsMenu = GameObject.FindGameObjectWithTag("Options");

        _movementAction = InputSystem.actions.FindAction("Movement");
        _lookAction = InputSystem.actions.FindAction("Look");
        _sprintAction = InputSystem.actions.FindAction("Sprint");
        _crouchAction = InputSystem.actions.FindAction("Crouch");
        _zoomAction = InputSystem.actions.FindAction("Zoom");
        _attackAction = InputSystem.actions.FindAction("Attack");
        _escapeAction = InputSystem.actions.FindAction("Escape");
        _interactAction = InputSystem.actions.FindAction("Interact");
    }

    public void Start()
    {
        _menu.SetActive(false);
        _optionsMenu.SetActive(false);
    }

    private void Update()
    {
        if (_escapeAction.WasPressedThisFrame() && !_isGameOver)
        {
            if (!_isInMenu)
            {
                _audioManager.PlayAudio(_audioManager.menuHover);
                EnableIsInMenu();
            }
            else
            {
                _audioManager.PlayAudio(_audioManager.menuCancel);
                DisableIsInMenu();
                _optionsMenu.SetActive(false);
            }
        }

        if (_isInMenu)
            return;

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

        if (_interactAction.WasPressedThisFrame())
            _interactHandler.StartInteraction();
    }

    private void FixedUpdate()
    {
        if (_isInMenu)
            return;

        _playerMovement.HandleMovement(_movementAction.ReadValue<Vector2>());
        _playerCamera.HandleZTitlt(_movementAction.ReadValue<Vector2>().x);
    }

    private void LateUpdate()
    {
        if (_isInMenu)
            return;

        _playerCamera.HandleCamera(_lookAction.ReadValue<Vector2>());
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    public void EnableGameOverState()
    {
        _isGameOver = true;
        _isInMenu = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void EnableIsInMenu()
    {
        _isInMenu = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _menu.SetActive(true);
    }

    public void DisableIsInMenu()
    {
        _isInMenu = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _menu.SetActive(false);
    }
}
