using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Saved Inventory", menuName = "Inventory Data")]
public class InventoryData : ScriptableObject
{
    public List<Item> items = new List<Item>();
    public void AddEquipment(Item item)
    {
        items.Add(item);
    }
    public void RemoveEquipment(Item item)
    {
        items.Remove(item);
    }
    public void ClearEquipment()
    {
        items.Clear();
    }
}
