using UnityEngine;

public class PlayerAttackDetection : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private GameObject _bloodSplatter;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag.Equals("EnemyHitbox"))
        {
            Instantiate(_bloodSplatter, collision.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position), Quaternion.identity);
            GetComponentInParent<PlayerAttackHandler>().DisableAttackHitbox();
            collision.gameObject.GetComponent<EnemyHitbox>().calculateDamage(_damage);
        }
    }
}
