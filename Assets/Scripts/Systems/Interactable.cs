using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract bool DoInteraction();
    public abstract string GetMessage();
}
