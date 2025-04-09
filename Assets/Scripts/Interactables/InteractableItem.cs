using Wardetta.Interactables;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    public void Interact(GameObject other)
    {
        Debug.Log("Interact");
    }
    

}
