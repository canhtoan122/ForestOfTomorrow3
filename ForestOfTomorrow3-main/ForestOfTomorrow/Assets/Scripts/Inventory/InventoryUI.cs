using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    InventoryManagement inventory;

    InventorySlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = InventoryManagement.instance;
        inventory.onItemChangedCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
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
    }
}
