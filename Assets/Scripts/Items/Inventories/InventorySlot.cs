﻿using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Wardetta.Items.Inventories
{
    public class InventorySlot : ItemSlotUI, IDropHandler
    {
        [SerializeField] private Inventory inventory = null;
        [SerializeField] private TextMeshProUGUI itemQuantityText = null;

        public override Item SlotItem
        {
            get { return ItemSlot.item; }
            set { }
        }

        public ItemSlot ItemSlot => inventory.GetSlotByIndex(SlotIndex);

        public override void OnDrop(PointerEventData eventData)
        {
            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

            if (itemDragHandler == null) { return; }

            if ((itemDragHandler.ItemSlotUI as InventorySlot) != null)
            {
                inventory.Swap(itemDragHandler.ItemSlotUI.SlotIndex, SlotIndex);
            }
        }

        public override void UpdateSlotUI()
        {
            if (ItemSlot.item == null)
            {
                EnableSlotUI(false);
                return;
            }

            EnableSlotUI(true);

            itemIconImage.sprite = ItemSlot.item.Icon;
            itemQuantityText.text = ItemSlot.quantity > 1 ? ItemSlot.quantity.ToString() : "";
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantityText.enabled = enable;
        }
    }
}
