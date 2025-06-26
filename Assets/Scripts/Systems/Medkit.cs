using UnityEngine;

public class Medkit : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().Heal(50);
            Destroy(gameObject);
        }
    }
}
