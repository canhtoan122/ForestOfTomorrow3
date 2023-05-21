using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public int armorModifier;
    public int damageModifier;
    

    public override void Use()
    {
        if(equipSlot.ToString() == "Currency")
        {
            return;
        }
        if(equipSlot.ToString() == "Weapon")
        {
            EquipmentManager.instance.EquipWeaponLayer();
        }
        base.Use();
        // Equip the item
        EquipmentManager.instance.Equip(this);
        // Remove it from the inventory
        RemoveFromInventory();
    }
}

public enum EquipmentSlot { Helmet, Armor, HealPotion, Weapon, Ring, Shoe, Glove, Currency}
