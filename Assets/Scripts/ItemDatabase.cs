using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Wardetta.Items.Inventories;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<InventoryItem> allItems;

    private Dictionary<string, InventoryItem> lookup;

    public void Init()
    {
        lookup = allItems.ToDictionary(item => item.Name, item => item);
    }

    public InventoryItem GetItemByID(string id)
    {
        if (lookup == null) Init();
        lookup.TryGetValue(id, out var item);
        return item;
    }
}