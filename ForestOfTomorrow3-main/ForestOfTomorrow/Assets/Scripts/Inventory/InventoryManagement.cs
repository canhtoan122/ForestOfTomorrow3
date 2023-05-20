using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagement : MonoBehaviour
{
    #region Singleton
    public static InventoryManagement instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;    
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    public int space = 20;
    public InventoryData inventoryData;

    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }
            items.Add(item);
            if (inventoryData.items.Contains(item))
            {
                Debug.Log(item + " exist.");
            }
            else
            {
                inventoryData.AddEquipment(item);
            }
            if (onItemChangedCallBack != null)
            {
                onItemChangedCallBack.Invoke();
            }
        }
        return true;
    }
    public void Remove(Item item)
    {
        items.Remove(item);
        inventoryData.RemoveEquipment(item);

        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }
    public void CheckItemInInventory(Equipment item)
    {
        if(items.Count == 0)
        {
            ShopController.buyItem = false;
        }
        if(!ShopController.buyItem)
        {
            return;
        }
        foreach (Equipment equipment in items)
        {
            if (equipment.equipSlot.ToString().Contains("Currency"))
            {
                if(equipment.quantity >= item.itemPrice)
                {
                    equipment.quantity -= item.itemPrice;
                }
                if(equipment.quantity <= 0)
                {
                    Remove(equipment);
                }
            }
            else
            {
                ShopController.buyItem = false;
            }
        }
    }
    public void AddMoney(Equipment equipment)
    {
        equipment.quantity = 15;
        InventorySlot.isMoney = true;
        Add(equipment);
    }
}
