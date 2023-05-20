using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemManagement : MonoBehaviour
{
    public GameObject inventoryUI;
    [SerializeField]
    private Equipment sword;
    [SerializeField]
    private Equipment healPotion;
    [SerializeField]
    private GameObject swordShelf;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private GameObject step6;
    [SerializeField]
    private GameObject step7;
    [SerializeField]
    private GameObject step8;

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
        step6.SetActive(false);
        step7.SetActive(false);
        step8.SetActive(true);
    }
    public void OpenInventory()
    {
        inventoryUI.SetActive(true);
        anim.SetBool("IsSlidingIn", false);
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Scene 3")
        {
            step8.SetActive(false);
        }
    }
    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
    }
}
