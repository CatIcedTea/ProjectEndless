using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _healthText;

    private PlayerHealth _playerHealth;
    void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        _healthText.text = "Health: " + (int)_playerHealth.GetHealth();
    }
}
