using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject removeButton;
    public Image icon;

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.gameObject.SetActive(true);
        removeButton.SetActive(true);
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.gameObject.SetActive(false);
        removeButton.SetActive(false);
    }
    public void OnRemoveButton()
    {
        InventoryManagement.instance.Remove(item);
    }
    public Animator playerAnimator;
    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
            MissionManagement.mission4Complete = true;
            playerAnimator.SetBool("EquipSword", true);
        }
    }
}
