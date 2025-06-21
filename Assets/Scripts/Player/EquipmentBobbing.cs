using UnityEngine;

public class EquipmentBobbing : MonoBehaviour
{
    [SerializeField] private float _bobSpeed = 7;
    [SerializeField] private GameObject _cameraPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _cameraPos.transform.rotation, Time.deltaTime * _bobSpeed);
    }
}
