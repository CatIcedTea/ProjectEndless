using UnityEngine;

public class DialogueText : MonoBehaviour
{

    private string[] _dialogueZero = new string[]
    {"Oh... You're back again...",
    "Well, I shouldn't be so surprised. You're here pretty often... and with this being your own <color=\"red\">head</color> and all.",
    "Just in case you forgot, you can move around using <color=\"red\">WASD</color> and sprint with <color=\"red\">LEFT SHIFT</color>, but watch your stamina!",
    "And keep that knife handy, you can swing it by clicking the <color=\"red\">LEFT MOUSE BUTTON</color>.",
    "If you need to squint to see something in the distance, try using the <color=\"red\">RIGHT MOUSE BUTTON</color>.",
    "And finally, you can sneak around by holding down <color=\"red\">CTRL</color>."
    };

    private string[] _dialogueOne = new string[]
    {"Shiny isn't it? Makes you want to grab it huh? I don't blame you.",
    "...Do you still remember how all of this works? You can easily see the shine on these keys even in the dark.",
    "If you want to get out of here, <color=\"red\">find four of these keys and open the door in the center</color>.",
    "And you already found the first one... Lucky you."
    };

    private string[] _dialogueTwo = new string[]
    {"You always arrive here in your own twisted nightmare after <color=\"red\">something bad</color> happens...",
     "as a way to cope? Escape? Punish yourself because you think you deserved it?",
    "<color=\"red\">Do you think you deserved everything that has happened to you...?</color>",
    "..."
    };

    private string[] _dialogueThree = new string[]
    {"Those <color=\"red\">creatures</color>... or whatever you call them... They look a bit like you, don't they? ...A bit like us?",
    "It's like you're <color=\"red\">killing a part of yourself</color> each time you kill one of them, or at least, that's how you imagined it anyways.",
    "Does it feel good? Watching them scream and bleed? Does a part of you enjoy hurting your own self?"
    };

    private string[] _dialogueFour = new string[]
    {"You got all four keys...",
    "If you're ready to leave, just head back to <color=\"red\">the door</color> at the center.",
    "Or you can stay here for as long as you like, if you want..."
    };

    private string[] _winDialogue = new string[]
    {
    "Can't wait to leave me already?",
    "Don't worry, I'll always be with you. Whenever you feel that impulse to hurt yourself again, You'll know its me.",
    "I'll see you soon... me...",
    "..."
    };

    private string[][] _dialogueText = new string[5][];

    void Start()
    {
        _dialogueText[0] = _dialogueZero;
        _dialogueText[1] = _dialogueOne;
        _dialogueText[2] = _dialogueTwo;
        _dialogueText[3] = _dialogueThree;
        _dialogueText[4] = _dialogueFour;
    }

    public string[] GetText(int num)
    {
        return _dialogueText[num];
    }

    public string[] GetWin()
    {
        return _winDialogue;
    }
}
