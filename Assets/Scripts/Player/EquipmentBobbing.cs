using DG.Tweening;
using UnityEngine;

public class EquipmentBobbing : MonoBehaviour
{
    [SerializeField] private float _bobTime;
    [SerializeField] private GameObject _cameraPos;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.DORotateQuaternion(_cameraPos.transform.rotation, _bobTime);
    }
}
