using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    Equipment[] currentEquipment;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    InventoryManagement inventory;
    public Image swordImage;
    public Image healPotionImage;
    public Image healPotionButton;
    public Image ringImage;
    public Image helmetImage;
    public Image shoeImage;
    public Image gloveImage;
    public Image armorImage;


    public Animator playerAnimator;
    void Start()
    {
        inventory = InventoryManagement.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        if (inventory.items.Count == 0)
        {
            Debug.Log("There are no equipment in the inventory");
        }
        else
        {
            int slotIndex = (int)newItem.equipSlot;
            Equipment oldItem = null;

            if (currentEquipment[slotIndex] != null)
            {
                oldItem = currentEquipment[slotIndex];
                inventory.Add(oldItem);
            }
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged(newItem, oldItem);
            }

            currentEquipment[slotIndex] = newItem;
            if (slotIndex == 0)
            {
                helmetImage.sprite = newItem.icon;
                helmetImage.gameObject.SetActive(true);
            }
            else if (slotIndex == 1)
            {
                armorImage.sprite = newItem.icon;
                armorImage.gameObject.SetActive(true);
            }
            else if (slotIndex == 2)
            {
                healPotionImage.sprite = newItem.icon;
                healPotionButton.sprite = newItem.icon;
                healPotionImage.gameObject.SetActive(true);
                healPotionButton.gameObject.SetActive(true);

            }
            else if (slotIndex == 3)
            {
                swordImage.sprite = newItem.icon;
                swordImage.gameObject.SetActive(true);
            }
            else if (slotIndex == 4)
            {
                ringImage.sprite = newItem.icon;
                ringImage.gameObject.SetActive(true);
            }
            else if (slotIndex == 5)
            {
                shoeImage.sprite = newItem.icon;
                shoeImage.gameObject.SetActive(true);
            }
            else if (slotIndex == 6)
            {
                gloveImage.sprite = newItem.icon;
                gloveImage.gameObject.SetActive(true);
            }
        }
    }
    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged(null, oldItem);
            }

            currentEquipment[slotIndex] = null;
            if (slotIndex == 0)
            {
                helmetImage.sprite = null;
                helmetImage.gameObject.SetActive(false);
            }
            else if (slotIndex == 1)
            {
                armorImage.sprite = null;
                armorImage.gameObject.SetActive(false);
            }
            else if (slotIndex == 2)
            {
                healPotionImage.sprite = null;
                healPotionButton.sprite = null;
                healPotionImage.gameObject.SetActive(false);
                healPotionButton.gameObject.SetActive(false);
            }
            else if (slotIndex == 3)
            {
                swordImage.sprite = null;
                swordImage.gameObject.SetActive(false);
                playerAnimator.SetBool("EquipSword", false);
            }
            else if (slotIndex == 4)
            {
                ringImage.sprite = null;
                ringImage.gameObject.SetActive(false);
            }
            else if (slotIndex == 5)
            {
                shoeImage.sprite = null;
                shoeImage.gameObject.SetActive(false);
            }
            else if (slotIndex == 6)
            {
                gloveImage.sprite = null;
                gloveImage.gameObject.SetActive(false);
            }
        }
    }
    public void UnequipAll()
    {
        for(int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }
}
