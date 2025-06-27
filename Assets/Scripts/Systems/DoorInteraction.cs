using TMPro;
using UnityEngine;

public class DoorInteraction : Interactable
{
    private QuestTracker _questTracker;

    private bool _hasStartedWinCondition = false;

    private AudioManager _audioManager;

    void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        _questTracker = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestTracker>();
    }

    public override bool DoInteraction()
    {
        if (_questTracker.HasEnoughKeys() && !_hasStartedWinCondition)
        {
            _audioManager.PlayAudio(_audioManager.menuHover);
            _hasStartedWinCondition = true;
            _questTracker.StartWinCondition();
        }

        return false;
    }

    public override string GetMessage()
    {
        _audioManager.PlayAudio(_audioManager.menuHover);
        if (_questTracker.HasEnoughKeys())
            return "Leave [E]";
        else
            return "I don't have enough keys...";

    }
}
