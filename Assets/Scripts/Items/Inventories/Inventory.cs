using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Wardetta.Items.Inventories
{
    public class Inventory : MonoBehaviour, IItemContainer
    {
        [SerializeField] private int money = 100;
        [SerializeField] private UnityEvent onInventoryItemsUpdated = null;
        [SerializeField] private ItemSlot[] itemSlots = Array.Empty<ItemSlot>();

        public int Money { get { return money; } set { money = value; } }

        public ItemSlot GetSlotByIndex(int index) => itemSlots[index];

        public ItemSlot AddItem(ItemSlot itemSlot)
        {
            Debug.Log(itemSlot.item.Name);
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item != null)
                {
                    if (itemSlots[i].item == itemSlot.item)
                    {
                        int slotRemainingSpace = itemSlots[i].item.MaxStack - itemSlots[i].quantity;

                        if (itemSlot.quantity <= slotRemainingSpace)
                        {
                            itemSlots[i].quantity += itemSlot.quantity;

                            itemSlot.quantity = 0;

                            onInventoryItemsUpdated.Invoke();

                            return itemSlot;
                        }
                        else if (slotRemainingSpace > 0)
                        {
                            itemSlots[i].quantity += slotRemainingSpace;

                            itemSlot.quantity -= slotRemainingSpace;
                        }
                    }
                }
            }

            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item == null)
                {
                    if (itemSlot.quantity <= itemSlot.item.MaxStack)
                    {
                        itemSlots[i] = itemSlot;

                        itemSlot.quantity = 0;

                        onInventoryItemsUpdated.Invoke();

                        return itemSlot;
                    }
                    else
                    {
                        itemSlots[i] = new ItemSlot(itemSlot.item, itemSlot.item.MaxStack);

                        itemSlot.quantity -= itemSlot.item.MaxStack;
                    }
                }
            }

            onInventoryItemsUpdated.Invoke();

            return itemSlot;
        }

        public void RemoveItem(ItemSlot itemSlot)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item != null)
                {
                    if (itemSlots[i].item == itemSlot.item)
                    {
                        if (itemSlots[i].quantity < itemSlot.quantity)
                        {
                            itemSlot.quantity -= itemSlots[i].quantity;

                            itemSlots[i] = new ItemSlot();
                        }
                        else
                        {
                            itemSlots[i].quantity -= itemSlot.quantity;

                            if (itemSlots[i].quantity == 0)
                            {
                                itemSlots[i] = new ItemSlot();

                                onInventoryItemsUpdated.Invoke();

                                return;
                            }
                        }
                    }
                }
            }
        }
        public void RemoveItem(ItemSlot itemSlot, int quantity)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item != null)
                {
                    if (itemSlots[i].item == itemSlot.item)
                    {
                        if (itemSlots[i].quantity < quantity)
                        {
                            itemSlot.quantity -= quantity;

                            itemSlots[i] = new ItemSlot();
                        }
                        else
                        {
                            itemSlots[i].quantity -= quantity;

                            onInventoryItemsUpdated.Invoke();
                            
                            if (itemSlots[i].quantity == 0)
                            {
                                itemSlots[i] = new ItemSlot();

                                onInventoryItemsUpdated.Invoke();

                                return;
                            }
                        }
                    }
                }
            }
        }
        public List<InventoryItem> GetAllUniqueItems()
        {
            List<InventoryItem> items = new List<InventoryItem>();

            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item == null) { continue; }

                if (items.Contains(itemSlots[i].item)) { continue; }

                items.Add(itemSlots[i].item);
            }

            return items;
        }
        public List<ItemSlot> GetAllItems()
        {
            // List<ItemSlot> items = new List<ItemSlot>();
            //
            // for (int i = 0; i < itemSlots.Length; i++)
            // {
            //     if (itemSlots[i].item == null) { continue; }
            //
            //     if (items.Contains(itemSlots[i].item)) { continue; }
            //
            //     items.Add(itemSlots[i].item);
            // }

            return itemSlots.ToList();
        }
        public void RemoveAt(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex > itemSlots.Length - 1) { return; }

            itemSlots[slotIndex] = new ItemSlot();

            onInventoryItemsUpdated.Invoke();
        }

        public void Swap(int indexOne, int indexTwo)
        {
            ItemSlot firstSlot = itemSlots[indexOne];
            ItemSlot secondSlot = itemSlots[indexTwo];

            if (firstSlot.Equals(secondSlot)) { return; }

            if (secondSlot.item != null)
            {
                if (firstSlot.item == secondSlot.item)
                {
                    int secondSlotRemainingSpace = secondSlot.item.MaxStack - secondSlot.quantity;

                    if (firstSlot.quantity <= secondSlotRemainingSpace)
                    {
                        itemSlots[indexTwo].quantity += firstSlot.quantity;

                        itemSlots[indexOne] = new ItemSlot();

                        onInventoryItemsUpdated.Invoke();

                        return;
                    }
                }
            }

            itemSlots[indexOne] = secondSlot;
            itemSlots[indexTwo] = firstSlot;

            onInventoryItemsUpdated.Invoke();
        }

        public bool HasItem(InventoryItem item)
        {
            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.item == null) { continue; }
                if (itemSlot.item != item) { continue; }

                return true;
            }

            return false;
        }

        public int GetTotalQuantity(InventoryItem item)
        {
            int totalCount = 0;

            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.item == null) { continue; }
                if (itemSlot.item != item) { continue; }

                totalCount += itemSlot.quantity;
            }

            return totalCount;
        }
        
        [SerializeField] private ItemDatabase itemDatabase;

        private string SavePath => Path.Combine(Application.persistentDataPath, "inventory.json");

        public void SaveInventory()
        {
            // var data = new InventorySaveData
            // {
            //     money = money,
            //     itemSlots = itemSlots
            //         .Where(slot => slot.item != null)
            //         .Select(slot => new ItemSlotSave { itemID = slot.item.Name, quantity = slot.quantity })
            //         .ToList()
            // };
            //
            // string json = JsonUtility.ToJson(data, true);
            // File.WriteAllText(SavePath, json);
            // Debug.Log("Saved inventory to " + SavePath);
            
            var data = new InventorySaveData
            {
                money = money,
                itemSlots = new List<ItemSlotSave>()
            };

            for (int i = 0; i < itemSlots.Length; i++)
            {
                var slot = itemSlots[i];
                if (slot.item == null) continue;

                data.itemSlots.Add(new ItemSlotSave
                {
                    slot = i, // Save the current slot index
                    itemID = slot.item.Name,
                    quantity = slot.quantity
                });
            }

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(SavePath, json);
        }

        public void LoadInventory()
        {
            if (!File.Exists(SavePath)) return;

            string json = File.ReadAllText(SavePath);
            InventorySaveData data = JsonUtility.FromJson<InventorySaveData>(json);

            money = data.money;
            itemSlots = new ItemSlot[itemSlots.Length]; // Clear current slots

            foreach (var savedSlot in data.itemSlots)
            {
                var item = itemDatabase.GetItemByID(savedSlot.itemID);
                if (item != null && savedSlot.slot >= 0 && savedSlot.slot < itemSlots.Length)
                {
                    itemSlots[savedSlot.slot] = new ItemSlot(item, savedSlot.quantity);
                }
            }

            onInventoryItemsUpdated?.Invoke();
            // if (!File.Exists(SavePath))
            // {
            //     Debug.LogWarning("No save file found.");
            //     return;
            // }
            //
            // string json = File.ReadAllText(SavePath);
            // InventorySaveData data = JsonUtility.FromJson<InventorySaveData>(json);
            //
            // money = data.money;
            // itemSlots = new ItemSlot[itemSlots.Length]; // Or data.itemSlots.Count if you want dynamic length
            //
            // for (int i = 0; i < data.itemSlots.Count && i < itemSlots.Length; i++)
            // {
            //     var item = itemDatabase.GetItemByID(data.itemSlots[i].itemID);
            //     if (item != null)
            //         itemSlots[i] = new ItemSlot(item, data.itemSlots[i].quantity);
            // }
            //
            // onInventoryItemsUpdated?.Invoke();
        }
    }
    
    
}
