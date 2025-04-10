using UnityEngine;

namespace Wardetta.Interactables
{
    public interface IInteractable
    {
        bool Interact(GameObject other);
    }
}
