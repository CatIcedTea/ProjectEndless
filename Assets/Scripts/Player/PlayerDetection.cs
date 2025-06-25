using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;

    private EnemyDoll _enemyDoll;
    private bool _detectable;

    void Update()
    {
        if (_detectable && !_playerMovement.GetCrouchingState() && _playerMovement.IsMoving())
        {
            _enemyDoll.EnterDetectedState(transform);
            _detectable = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!collision.GetComponent<EnemyDoll>().GetIsDead())
            {
                _detectable = true;
                _enemyDoll = collision.GetComponent<EnemyDoll>();
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!collision.GetComponent<EnemyDoll>().GetIsDead())
            {
                _detectable = false;
                _enemyDoll = null;
            }
        }
    }
}
