using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    private Animator _animator;

    private bool _canAttack = true;
    private bool _firstAttack = true;
    private AudioManager _audioManager;


    void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        _animator = GetComponent<Animator>();
    }

    public void HandleAttack()
    {
        if (_canAttack)
        {
            if (_firstAttack)
            {
                _animator.SetTrigger("AttackRight");
                _firstAttack = false;
            }
            else
            {
                _animator.SetTrigger("AttackLeft");
                _firstAttack = true;
            }
        }
    }

    public void EnableAttackInput()
    {
        _canAttack = true;
    }

    public void DisableAttackInput()
    {
        _canAttack = false;
    }

    public void EnableAttackHitbox()
    {
        GetComponentInChildren<BoxCollider>().enabled = true;
        _audioManager.PlayAudio(_audioManager.attackSwing);
    }

    public void DisableAttackHitbox()
    {
        GetComponentInChildren<BoxCollider>().enabled = false;
    }

    public void SetFirstAttack()
    {
        _firstAttack = true;
    }

    public void SetSecondAttack()
    {
        _firstAttack = false;
    }

    public void StopAttacking()
    {
        _animator.SetTrigger("StopAttacking");
        _firstAttack = true;
    }
}
