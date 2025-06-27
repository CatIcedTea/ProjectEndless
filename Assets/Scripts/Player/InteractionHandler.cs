using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    private List<Interactable> interactables = new List<Interactable>();
    private TextMeshProUGUI _text;

    void Start()
    {
        _text = GameObject.FindGameObjectWithTag("Message").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
    }


    public void StartInteraction()
    {
        if (interactables.Count > 0)
        {
            if (interactables[0].DoInteraction())
                interactables.Clear();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interactables.Add(collision.GetComponent<Interactable>());
            _text.text = collision.GetComponent<Interactable>().GetMessage();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interactables.Remove(collision.GetComponent<Interactable>());
            _text.text = "";
        }
    }
}
