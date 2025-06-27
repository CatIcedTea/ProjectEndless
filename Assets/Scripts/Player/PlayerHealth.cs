using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _MaxHealth;
    [SerializeField] private float _MaxOverheal;
    [SerializeField] private float _overhealDecay;
    [SerializeField] private PlayerCamera _playerCamera;

    private float _health;
    private RawImage _damageFlash;
    private RawImage _healFlash;
    private GameObject _GameOverMenu;
    private InputHandler _inputHandler;

    private AudioManager _audioManager;

    void Awake()
    {
        _GameOverMenu = GameObject.FindGameObjectWithTag("GameOver");
        _GameOverMenu.SetActive(false);
    }

    void Start()
    {
        _health = _MaxHealth;
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        _inputHandler = GetComponent<InputHandler>();
        _damageFlash = GameObject.FindGameObjectWithTag("DamageFlash").GetComponent<RawImage>();
        _healFlash = GameObject.FindGameObjectWithTag("HealFlash").GetComponent<RawImage>();
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

        if (_health < _MaxHealth * 0.4f)
        {
            _health += Time.deltaTime * 0.3f;

            if (_health > _MaxHealth * 0.4f)
                _health = _MaxHealth * 0.4f;
        }
    }

    public void TakeDamage(float damage)
    {
        if (_health <= 0)
            return;

        _health -= damage;
        _damageFlash.color = Color.white;
        DOTween.To(() => _damageFlash.color, x => _damageFlash.color = x, new Color(0, 0, 0, 0), 1f);
        _playerCamera.HandleShake(50);
        _audioManager.PlayAudio(_audioManager.playerHit);

        if (_health <= 0)
        {
            _GameOverMenu.SetActive(true);
            DOTween.To(() => _GameOverMenu.GetComponent<Image>().color, x => _GameOverMenu.GetComponent<Image>().color = x, new Color(0.47f, 0.1f, 0.1f, 1), 1f);

            _inputHandler.EnableGameOverState();
        }
    }

    public void Heal(float amount)
    {
        _healFlash.color = Color.white;
        DOTween.To(() => _healFlash.color, x => _healFlash.color = x, new Color(0, 0, 0, 0), 1f);
        _health += amount;

        if (_health > _MaxHealth + 50)
            _health = _MaxHealth + 50;
    }

    public float GetHealth()
    {
        return _health;
    }
}
