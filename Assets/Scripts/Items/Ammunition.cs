﻿using System.Text;
using UnityEngine;
using Wardetta.Items.Inventories;

namespace Wardetta.Items
{
    [CreateAssetMenu(fileName = "New Ammunition", menuName = "Items/Ammunition")]
    public class Ammunition : InventoryItem
    {
        [SerializeField] private GameObject ammunitionPrefab = null;

        public GameObject AmmunitionPrefab => ammunitionPrefab;

        public override string GetInfoDisplayText()
        {
            StringBuilder builder = new StringBuilder();
            
            builder.Append("Max Stack: ").Append(MaxStack).AppendLine();
            builder.Append("Sell Price: ").Append(SellPrice).Append(" Gold");

            return builder.ToString();
        }
    }
}
