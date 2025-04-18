using UnityEngine;
using Wardetta.Interactables;

namespace Wardetta.Items
{
    public class ItemPickup : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemSlot itemSlot;

        public bool Interact(GameObject other)
        {
            var itemContainer = other.GetComponent<IItemContainer>();

            if (itemContainer == null) { return false; }

            if (itemContainer.AddItem(itemSlot).quantity == 0)
            {
                Destroy(gameObject);
                
            }
            return true;
        }
        
    }
}