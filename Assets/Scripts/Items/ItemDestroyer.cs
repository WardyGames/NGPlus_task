﻿using TMPro;
using UnityEngine;
using Wardetta.Items.Inventories;

namespace Wardetta.Items
{
    public class ItemDestroyer : MonoBehaviour
    {
        [SerializeField] private Inventory inventory = null;
        [SerializeField] private TextMeshProUGUI areYouSureText = null;

        private int slotIndex = 0;

        private void OnDisable() => slotIndex = -1;

        public void Activate(ItemSlot itemSlot, int slotIndex)
        {
            this.slotIndex = slotIndex;

            areYouSureText.text = $"Are you sure you wish to destroy {itemSlot.quantity}x {itemSlot.item.Name}?";

            gameObject.SetActive(true);
        }

        public void Destroy()
        {
            inventory.RemoveAt(slotIndex);

            gameObject.SetActive(false);
        }

        public void DestroyItemImmediately(ItemSlot itemSlot, int selectedSlotIndex)
        {
            inventory.RemoveAt(selectedSlotIndex);
        }
        
        public void UseItem(ItemSlot itemSlot)
        {
            inventory.RemoveItem(itemSlot, 1);
        }
    }
}
