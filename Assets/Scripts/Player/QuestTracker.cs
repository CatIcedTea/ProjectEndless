using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class QuestTracker : MonoBehaviour
{
    private int _numKeys = 0;
    private Dialogue _dialogue;
    private DialogueText _dialogueText;

    private GameObject _winCondition;

    void Awake()
    {
        _winCondition = GameObject.FindGameObjectWithTag("WinCondition");
        _winCondition.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        _winCondition.SetActive(false);
    }

    void Start()
    {
        _dialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>();
        _dialogueText = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueText>();
    }

    public void IncrementKeys()
    {
        _numKeys++;
        _dialogue.WriteText(_dialogueText.GetText(_numKeys));
    }

    public bool HasEnoughKeys()
    {
        return _numKeys >= 4;
    }

    public void StartWinCondition()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<InputHandler>().EnableGameOverState();

        _winCondition.SetActive(true);
        _dialogue.EnableWinState();
        DOTween.To(() => _winCondition.GetComponent<Image>().color, x => _winCondition.GetComponent<Image>().color = x, new Color(0.07f, 0.07f, 0.07f, 1), 1f).
            OnComplete(() => _dialogue.WriteText(_dialogueText.GetWin()));
    }
}
