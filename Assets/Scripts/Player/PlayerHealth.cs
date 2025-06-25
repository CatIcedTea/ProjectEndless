using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _MaxHealth;
    [SerializeField] private float _overhealDecay;
    [SerializeField] private PlayerCamera _playerCamera;

    private float _health;
    private RawImage _damageFlash;

    void Start()
    {
        _health = _MaxHealth;

        _damageFlash = GameObject.FindGameObjectWithTag("DamageFlash").GetComponent<RawImage>();
    }

    void Update()
    {
        //Manages overheal
        if (_health > _MaxHealth)
        {
            _health -= _overhealDecay * Time.deltaTime;

            if (_health < _MaxHealth)
                _health = _MaxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _damageFlash.color = Color.white;
        DOTween.To(() => _damageFlash.color, x => _damageFlash.color = x, new Color(0, 0, 0, 0), 1f);
        _playerCamera.HandleShake(50);
        Debug.Log(_health);
    }

    public void Heal(float amount)
    {
        _health += amount;
    }
}
