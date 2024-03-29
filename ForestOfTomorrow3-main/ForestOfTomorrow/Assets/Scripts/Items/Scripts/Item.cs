using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName; // Name of the item
    public Sprite icon;     //Item icon
    public bool isDefaultItem = false;  // Is the item default wear?
    public int itemPrice;   // The price of the item on shop
    public int quantity;    // The quantity of the item

    public virtual void Use()
    {
        // Use the item
        // Something might happen

        Debug.Log("Using " + itemName);
    }
    public void RemoveFromInventory()
    {
        InventoryManagement.instance.Remove(this);
    }
}
