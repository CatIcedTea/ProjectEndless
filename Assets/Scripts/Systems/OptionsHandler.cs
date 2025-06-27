using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour
{
    [SerializeField] private Slider _FOVSlider;
    [SerializeField] private Slider _SensSlider;
    [SerializeField] private Slider _AmbienceSlider;
    [SerializeField] private Slider _SFXSlider;

    private CinemachineCamera _cinemachineCamera;
    private PlayerCamera _playerCam;

    private AudioManager _audioManager;

    void Awake()
    {
        _cinemachineCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<CinemachineCamera>();
        _playerCam = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerCamera>();

        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        _FOVSlider.value = _cinemachineCamera.Lens.FieldOfView;
        _SensSlider.value = _playerCam.GetSensitivity();

        _FOVSlider.GetComponentInChildren<TextMeshProUGUI>().text = "Field of View: " + (int)_FOVSlider.value;
        _SensSlider.GetComponentInChildren<TextMeshProUGUI>().text = "Look Sensitivity: " + _SensSlider.value.ToString("0.00");

        if (PlayerPrefs.HasKey("FOV"))
        {
            _FOVSlider.value = PlayerPrefs.GetFloat("FOV");
            UpdateFOV();
        }
        if (PlayerPrefs.HasKey("Sens"))
        {
            _SensSlider.value = PlayerPrefs.GetFloat("Sens");
            UpdateSensitivity();
        }
        if (PlayerPrefs.HasKey("Ambience"))
        {
            _AmbienceSlider.value = PlayerPrefs.GetFloat("Ambience");
            UpdateAmbience();
        }
        if (PlayerPrefs.HasKey("SFX"))
        {
            _SFXSlider.value = PlayerPrefs.GetFloat("SFX");
            UpdateSFX();
        }
    }

    public void UpdateFOV()
    {
        _cinemachineCamera.Lens.FieldOfView = _FOVSlider.value;
        _FOVSlider.GetComponentInChildren<TextMeshProUGUI>().text = "Field of View: " + (int)_FOVSlider.value;

        PlayerPrefs.SetFloat("FOV", _FOVSlider.value);
        PlayerPrefs.Save();
    }

    public void UpdateSensitivity()
    {
        _playerCam.GetComponent<PlayerCamera>().SetSensitivity(_SensSlider.value);
        _SensSlider.GetComponentInChildren<TextMeshProUGUI>().text = "Look Sensitivity: " + _SensSlider.value.ToString("0.00");

        PlayerPrefs.SetFloat("Sens", _SensSlider.value);
        PlayerPrefs.Save();
    }

    public void UpdateAmbience()
    {
        _audioManager.ambienceSource.volume = _AmbienceSlider.value;

        PlayerPrefs.SetFloat("Ambience", _AmbienceSlider.value);
        PlayerPrefs.Save();
    }

    public void UpdateSFX()
    {
        _audioManager.sfx.volume = _SFXSlider.value;

        PlayerPrefs.SetFloat("SFX", _SFXSlider.value);
        PlayerPrefs.Save();
    }


    public void Return()
    {
        _audioManager.PlayAudio(_audioManager.menuCancel);
        gameObject.SetActive(false);
    }
}
