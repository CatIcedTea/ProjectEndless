using UnityEngine;

public class Medkit : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().Heal(50);
            AudioManager audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            audioManager.PlayAudio(audioManager.healthPickup);
            Destroy(gameObject);
        }
    }
}
