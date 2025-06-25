using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private EnemyDoll _enemyDoll;

    public void calculateDamage(float damage)
    {
        _enemyDoll.TakeDamage(damage);
    }
}
