using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Transform craftInventory;

    InventoryManagement inventory;

    InventorySlot[] slots; 
    InventorySlot[] slots2;
    // Start is called before the first frame update
    void Start()
    {
        inventory = InventoryManagement.instance;
        inventory.onItemChangedCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        slots2 = craftInventory.GetComponentsInChildren<InventorySlot>();
    }
    void UpdateUI()
    {
        for (int i  = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
                if(inventory.items[i].quantity > 0)
                {
                    slots[i].ActivateQuantity();
                }
                else
                {
                    slots[i].DeactivateQuantity();
                }
            }
            else
            {
                slots[i].ClearSlot();
                slots[i].DeactivateQuantity();
            }
        }
        for (int i = 0; i < slots2.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots2[i].AddItem(inventory.items[i]);
                if (inventory.items[i].quantity > 0)
                {
                    slots2[i].ActivateQuantity();
                }
                else
                {
                    slots2[i].DeactivateQuantity();
                }
            }
            else
            {
                slots2[i].ClearSlot();
                slots2[i].DeactivateQuantity();
            }
        }
    }
}
