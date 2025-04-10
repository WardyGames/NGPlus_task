using UnityEngine;
using UnityEngine.EventSystems;

namespace Wardetta.Items.Inventories
{
    public class InventoryItemDragHandler : ItemDragHandler
    {
        [SerializeField] private ItemDestroyer itemDestroyer = null;

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                base.OnPointerUp(eventData);

                if (eventData.hovered.Count == 0)
                {
                    InventorySlot thisSlot = ItemSlotUI as InventorySlot;
                    itemDestroyer.Activate(thisSlot.ItemSlot, thisSlot.SlotIndex);
                }
            }
            
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                InventorySlot thisSlot = ItemSlotUI as InventorySlot;
                itemDestroyer.UseItem(thisSlot.ItemSlot);
            }
            
        }
    }
}
