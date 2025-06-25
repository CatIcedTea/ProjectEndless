using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDoll : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _stunTime;
    [SerializeField] private Transform _headPosition;

    [SerializeField] private Animator _animator;

    [SerializeField] private NavMeshAgent _navAgent;
    private Transform _player;

    private bool _detected = false;
    private bool _canMove = true;

    private float _health;
    private float _stunTimer = 0;
    private bool _isDead = false;

    void Start()
    {
        _navAgent.updatePosition = true;
        _health = _maxHealth;
    }

    void Update()
    {
        if (_isDead)
            return;

        if (_stunTimer > 0)
            _stunTimer -= Time.deltaTime;

        if (_detected)
            HandleNavigation();
    }

    private void HandleNavigation()
    {
        _navAgent.destination = _player.position;

        if (IsInRange())
        {
            transform.DODynamicLookAt(new Vector3(_player.position.x, transform.position.y, _player.position.z), 0.5f);
            _navAgent.isStopped = true;
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isAttacking", true);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && !_detected)
        {
            Ray _rayInView = new Ray(_headPosition.position, collision.transform.position - _headPosition.position);
            RaycastHit _playerInView;

            Physics.Raycast(_rayInView, out _playerInView);

            if (_playerInView.transform.tag == "Player")
            {
                _player = collision.gameObject.transform;
                _detected = true;

                _navAgent.destination = _player.position;

                _navAgent.isStopped = false;
                _animator.SetBool("isRunning", true);
                _animator.SetBool("isAttacking", false);
            }
        }
    }

    private bool IsInRange()
    {
        return _navAgent.remainingDistance <= _navAgent.stoppingDistance;
    }

    public void HandleAttackFinish()
    {
        if (_isDead)
            return;

        if (IsInRange())
        {
            _navAgent.isStopped = true;
            _animator.SetBool("isRunning", false);
            _animator.SetBool("isAttacking", true);
        }
        else
        {
            _navAgent.isStopped = false;
            _animator.SetBool("isRunning", true);
            _animator.SetBool("isAttacking", false);
        }
    }

    public void SetCanMove(bool move)
    {
        _canMove = move;

    }

    public void TakeDamage(float damage)
    {
        if (!_detected)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;

            _navAgent.destination = _player.position;
            _navAgent.isStopped = false;
            _animator.SetBool("isRunning", true);
            _animator.SetBool("isAttacking", false);

            _detected = true;

        }
        if (!_isDead)
        {
            _health -= damage;
            if (_health < 0)
            {
                _animator.Play("Death");
                _animator.SetBool("isDead", true);
                GetComponent<CapsuleCollider>().enabled = false;
                _isDead = true;
            }
            else
            {
                if (_stunTimer <= 0)
                {
                    _animator.Play("Damaged");
                    _stunTimer = _stunTime;
                }
            }
        }
    }
}
