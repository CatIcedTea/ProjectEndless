using System.Collections;
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

    private AudioManager _audioManager;

    void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        _navAgent.updatePosition = true;
        _health = _maxHealth;

        StartCoroutine(idleSounds());
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

    private IEnumerator idleSounds()
    {
        yield return new WaitForSeconds(Random.Range(10, 60));
        if (!_isDead)
        {
            _audioManager.PlayAudioAtLocation(_audioManager.dollIdle, transform.position);
            StartCoroutine(idleSounds());
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
                EnterDetectedState(collision.gameObject.transform);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !_detected)
        {
            EnterDetectedState(collision.gameObject.transform);

        }
    }

    public void EnterDetectedState(Transform playerTransform)
    {
        _audioManager.PlayAudioAtLocation(_audioManager.dollDamage, transform.position);
        _player = playerTransform;

        _detected = true;

        _navAgent.destination = _player.position;

        _navAgent.isStopped = false;
        _animator.SetBool("isRunning", true);
        _animator.SetBool("isAttacking", false);

        _animator.Play("Running");
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

    public bool GetIsDead()
    {
        return _isDead;
    }

    public void TakeDamage(float damage)
    {
        _audioManager.PlayAudio(_audioManager.playerImpact);
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
                _audioManager.PlayAudioAtLocation(_audioManager.dollDeath, transform.position);
            }
            else
            {
                if (_stunTimer <= 0)
                {
                    _animator.Play("Damaged");
                    _stunTimer = _stunTime;
                    _audioManager.PlayAudioAtLocation(_audioManager.dollDamage, transform.position);
                }
            }
        }
    }
}
