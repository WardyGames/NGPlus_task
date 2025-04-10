using UnityEngine;

namespace Wardetta.Items.Inventories
{
    public abstract class InventoryItem : Item
    {
        [Header("Item Data")]
        [SerializeField] [Min(0)] private int sellPrice = 1;
        [SerializeField] [Min(1)] private int maxStack = 1;
        
        public int SellPrice => sellPrice;
        public int MaxStack => maxStack;
    }
}
