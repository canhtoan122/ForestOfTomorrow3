using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject removeButton;
    public GameObject quantityNumber;
    public Image icon;

    public Item item;

    public static bool isMoney = false;
    public void AddItem(Item newItem)
    {
        item = newItem;
        item.quantity = newItem.quantity;
        quantityNumber.GetComponent<TMP_Text>().text = newItem.quantity.ToString();

        icon.sprite = item.icon;
        icon.gameObject.SetActive(true);
        removeButton.SetActive(true);
    }
    public void DragItem()
    {
        if (item == null)
        {
            return;
        }
        CraftController.instance.OnMouseDownItem(item);
        InventoryManagement.instance.Remove(item);
    }
    public void ActivateQuantity()
    {
        quantityNumber.SetActive(true);
    }
    public void DeactivateQuantity()
    {
        quantityNumber.SetActive(false);
    }
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.gameObject.SetActive(false);
        removeButton.SetActive(false);
        quantityNumber.SetActive(false);
    }
    public void OnRemoveButton()
    {
        InventoryManagement.instance.Remove(item);
    }
    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
            MissionManagement.mission4Complete = true;
        }
    }
}
