﻿using System.Collections.Generic;
using Wardetta.Items.Inventories;

namespace Wardetta.Items
{
    public interface IItemContainer
    {
        int Money { get; set; }
        ItemSlot GetSlotByIndex(int index);
        ItemSlot AddItem(ItemSlot itemSlot);
        List<InventoryItem> GetAllUniqueItems();
        void RemoveItem(ItemSlot itemSlot);
        void RemoveAt(int slotIndex);
        void Swap(int indexOne, int indexTwo);
        bool HasItem(InventoryItem item);
        int GetTotalQuantity(InventoryItem item);
    }
}
