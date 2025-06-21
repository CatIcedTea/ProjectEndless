using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _maxStamina;
    [SerializeField] private float _staminaRegenSpeed;
    [SerializeField] private float _staminaRegenTimer;
    [SerializeField] private float _sprintStaminaCost;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _sprintAcceleration;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _maxAirSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private Transform _playerBasis;
    [SerializeField] private float _friction;

    [Header("Crouching")]
    [SerializeField] private float _crouchSpeed;
    [SerializeField] private float _crouchInitialYScaling;
    [SerializeField] private float _crouchFriction;
    [SerializeField] private float _crouchYScaling;

    [Header("Floor Check")]
    [Tooltip("Used for raycast checking floor collision")]
    [SerializeField] private float _playerHeight;

    private bool _isOnFloor;
    private bool _isSprinting;
    private bool _isCrouching;
    private Vector3 _moveDir;
    private Rigidbody _rigidBody;
    private float _moveSpeed;
    private float _stamina;


    void Start()
    {
        _moveSpeed = _acceleration;
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if it is on floor
        _isOnFloor = Physics.SphereCast(transform.position, 0.25f, Vector3.down, out RaycastHit hit, _playerHeight * transform.localScale.y * 0.5f - 0.25f + 0.02f);

        if (_isSprinting)
        {
            if (_stamina > 0)
            {
                _stamina -= _sprintStaminaCost * Time.deltaTime;
                if (_stamina <= 0)
                {
                    _isSprinting = false;
                    _moveSpeed = _acceleration;
                    _stamina = 0;
                }
            }
        }
        else
        {
            if (_stamina < _maxStamina)
            {
                _stamina += _staminaRegenSpeed * Time.deltaTime;
                if (_stamina > _maxStamina)
                    _stamina = _maxStamina;
            }
        }

        Debug.Log(_stamina);
    }

    public void HandleMovement(Vector2 input)
    {
        _moveDir = input.x * _playerBasis.right + input.y * _playerBasis.forward;
        _rigidBody.AddForce(_moveDir * _moveSpeed, ForceMode.Force);

        if (_isOnFloor)
        {
            _rigidBody.linearDamping = _friction;
        }
    }

    public void HandleSprint(bool isSprinting)
    {
        if (_stamina <= 0)
            return;

        _isSprinting = isSprinting;

        if (isSprinting && !_isCrouching)
            _moveSpeed = _sprintAcceleration;
        else
            _moveSpeed = _acceleration;
    }

    public void HandleCrouch(bool isCrouching)
    {
        _isCrouching = isCrouching;

        if (isCrouching)
        {
            _moveSpeed = _crouchSpeed;
            GetComponent<CapsuleCollider>().height = 0.5f;
            _rigidBody.AddForce(Vector3.down * 5, ForceMode.Impulse);
        }
        else
        {
            _moveSpeed = _acceleration;
            GetComponent<CapsuleCollider>().height = 2f;
        }
    }
}
