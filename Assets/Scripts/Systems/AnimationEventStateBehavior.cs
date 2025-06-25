using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : StateMachineBehaviour
{
    public string eventName;
    [Range(0f, 1f)] public float triggerTime;

    bool hasTriggered;


}
