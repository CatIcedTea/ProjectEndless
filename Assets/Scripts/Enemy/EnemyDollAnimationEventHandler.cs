using UnityEngine;

public class EnemyDollAnimationEventHandler : MonoBehaviour
{
    [SerializeField] private EnemyDoll _enemy;
    [SerializeField] private BoxCollider _attackBox;
    [SerializeField] private float _damage;

    private AudioManager _audioManager;

    void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void StartAttack()
    {
    }

    public void AttackFinished()
    {
        _enemy.HandleAttackFinish();
    }

    public void HandleAttack()
    {
        _attackBox.enabled = true;

    }

    public void EndAttack()
    {
        _attackBox.enabled = false;
    }

    public void EnableCanMove()
    {
        _enemy.SetCanMove(true);
    }

    public void DisableCanMove()
    {
        _enemy.SetCanMove(false);
    }

    public void PlayFootstep()
    {
        AudioSource.PlayClipAtPoint(_audioManager.footstep, transform.position);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(_damage);
        }
    }
}
