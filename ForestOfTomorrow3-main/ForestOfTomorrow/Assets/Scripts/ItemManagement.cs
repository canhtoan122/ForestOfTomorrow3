using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManagement : MonoBehaviour
{

    [SerializeField]
    private GameObject inventoryUI;
    [SerializeField]
    private Equipment sword;
    [SerializeField]
    private Equipment healPotion;
    [SerializeField]
    private GameObject swordShelf;
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Animator anim;

    public void PickUpItem()
    {
        InventoryManagement.instance.Add(healPotion);
        bool wasPickedUp = InventoryManagement.instance.Add(sword);
        if(wasPickedUp)
        {
            swordShelf.SetActive(false);
        }
    }
    public void OpenInventory()
    {
        inventoryUI.SetActive(true);
        anim.SetBool("IsSlidingIn", false);
    }
    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
    }
}
