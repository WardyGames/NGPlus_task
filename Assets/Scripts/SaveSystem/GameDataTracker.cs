using System;
using System.Collections.Generic;
using UnityEngine;
using Wardetta.Items.Inventories;

public class GameDataTracker : MonoBehaviour
{
    [SerializeField] Inventory playerInventory;
    //IDataService _jsonDataService = new JsonDataService();
    void Start()
    {
        TryToLoadItems();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    [ContextMenu("Save Inventory")]
    void Save()
    {
        playerInventory.SaveInventory();
    }
    
    [ContextMenu("Load Inventory")]
    void TryToLoadItems()
    {
        playerInventory.LoadInventory();
    }

    private void OnDestroy()
    {
        Save();
    }
}

[System.Serializable]
public struct ItemSlotSave
{
    public string itemID;
    public int quantity;
    public int slot;
}

[System.Serializable]
public class InventorySaveData
{
    public int money;
    public List<ItemSlotSave> itemSlots;
}
