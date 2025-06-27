using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private GameObject _playerCam;
    private GameObject _cameraPos;
    private GameObject _cameraYRotation;

    [SerializeField] private float _defaultNoiseAmplitude;
    [SerializeField] private float _defaultNoiseFrequency;

    [Tooltip("Mouse Sensitivity")]
    [Header("Sensitivity")]
    [SerializeField] private float _sensitivityX;
    [SerializeField] private float _sensitivityY;

    //The X rotation input
    private float _xRotation;
    //The Y rotation input
    private float _yRotation;

    void Start()
    {
        _playerCam = GameObject.FindGameObjectWithTag("PlayerCamera");
        _cameraPos = GameObject.Find("CameraPos");
        _cameraYRotation = GameObject.Find("CameraYRotation");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void HandleCamera(Vector2 cameraInput)
    {
        _yRotation += cameraInput.x * Time.deltaTime * _sensitivityX;
        _xRotation -= cameraInput.y * Time.deltaTime * _sensitivityY;

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        _cameraPos.transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        _cameraYRotation.transform.rotation = Quaternion.Euler(0, _yRotation, 0);
    }

    public void HandleZTitlt(float rotation)
    {
        DOTween.To(() => _playerCam.GetComponent<CinemachineCamera>().Lens.Dutch, x => _playerCam.GetComponent<CinemachineCamera>().Lens.Dutch = x, -rotation * 2f, 0.5f);
    }

    public void HandleZoom(bool isZooming)
    {
        if (isZooming)
        {
            DOTween.To(() => _playerCam.GetComponent<CinemachineCamera>().Lens.FieldOfView, x => _playerCam.GetComponent<CinemachineCamera>().Lens.FieldOfView = x, 45, 0.5f);
        }
        else
        {
            DOTween.To(() => _playerCam.GetComponent<CinemachineCamera>().Lens.FieldOfView, x => _playerCam.GetComponent<CinemachineCamera>().Lens.FieldOfView = x, 90, 0.5f);
        }
    }

    public void HandleShake(float amount)
    {
        _playerCam.GetComponent<CinemachineBasicMultiChannelPerlin>().FrequencyGain = amount;
        DOTween.To(() => _playerCam.GetComponent<CinemachineBasicMultiChannelPerlin>().FrequencyGain
            , x => _playerCam.GetComponent<CinemachineBasicMultiChannelPerlin>().FrequencyGain = x, _defaultNoiseFrequency, 1f);
    }

    public void HandleWobble(float amount)
    {
        _playerCam.GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = _defaultNoiseAmplitude + amount;
    }

    public void SetNoiseToDefault()
    {
        _playerCam.GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = _defaultNoiseAmplitude;
        _playerCam.GetComponent<CinemachineBasicMultiChannelPerlin>().FrequencyGain = _defaultNoiseFrequency;
    }

    public float GetDefaultNoiseAmplitude()
    {
        return _defaultNoiseAmplitude;
    }

    public void SetSensitivity(float val)
    {
        _sensitivityX = val;
        _sensitivityY = val;
    }

    public float GetSensitivity()
    {
        return _sensitivityX;
    }
}
