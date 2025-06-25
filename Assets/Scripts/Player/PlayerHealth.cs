using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _MaxHealth;
    [SerializeField] private float _overhealDecay;

    private float _health;

    void Start()
    {
        _health = _MaxHealth;
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
        Debug.Log(_health);
    }

    public void Heal(float amount)
    {
        _health += amount;
    }
}
