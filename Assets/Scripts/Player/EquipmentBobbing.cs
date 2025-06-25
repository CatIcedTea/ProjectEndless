using UnityEngine;

public class EquipmentBobbing : MonoBehaviour
{
    [SerializeField] private float _bobSpeed = 7;
    [SerializeField] private GameObject _cameraPos;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _cameraPos.transform.rotation, Time.deltaTime * _bobSpeed);
    }
}
