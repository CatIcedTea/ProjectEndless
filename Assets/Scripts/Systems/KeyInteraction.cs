using TMPro;
using UnityEngine;

public class KeyInteraction : Interactable
{
    private AudioManager _audioManager;

    public override bool DoInteraction()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<QuestTracker>().IncrementKeys();
        GameObject.FindGameObjectWithTag("Message").GetComponent<TextMeshProUGUI>().text = "";
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        _audioManager.PlayAudio(_audioManager.keyPickup);
        Destroy(gameObject);
        return true;
    }

    public override string GetMessage()
    {
        return "Pick Up [E]";
    }
}
