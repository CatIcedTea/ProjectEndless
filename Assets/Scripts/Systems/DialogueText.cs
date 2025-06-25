using UnityEngine;

public class DialogueText : MonoBehaviour
{
    private string[] dialogueText = new string[]
    {"Oh... You're back again so soon...",
    "Well, I shouldn't be so surprised. You're here pretty often... with it being your own <color=\"red\">head</color>, after all.",
    "So tell me, what is it this time that compels you to be here? A breakup? Got chewed out by the boss?",
    "You always arrive here in your own twisted nightmare after <color=\"red\">something</color> happens, as a way to cope? Escape? Punish yourself because you think you deserved it?",
    "<color=\"red\">Do you think you deserved it? Everything that has happened to you...?</color>"};

    public string[] GetText(int num)
    {
        return dialogueText;
    }
}
