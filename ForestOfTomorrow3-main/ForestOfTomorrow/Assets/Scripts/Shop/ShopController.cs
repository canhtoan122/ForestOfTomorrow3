using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public GameObject pickUpButton;
    public GameObject attackButton;
    public GameObject shopUI;
    public GameObject buyUI;
    public GameObject optionUI;
    public TMP_Text categoryName;
    public TMP_Text swordCategory;
    public TMP_Text armorCategory;
    public TMP_Text energyCategory;
    public GameObject itemDisplay;
    public GameObject itemImage;
    public GameObject itemName;
    public GameObject itemPrice;

    private string tempCategory;
    private bool buyItem = false;

    public List<Item> items = new List<Item>();
    public void ActivatePickUpButton()
    {
        pickUpButton.SetActive(true);
        attackButton.SetActive(false);
    }
    public void DeActivatePickUpButton()
    {
        pickUpButton.SetActive(false);
        attackButton.SetActive(true);
    }
    public void OpenShopUI()
    {
        shopUI.SetActive(true);
    }
    public void CloseShopUI()
    {
        buyUI.SetActive(false);
        shopUI.SetActive(false);
        optionUI.SetActive(true);
    }
    public void OpenBuyUI()
    {
        buyUI.SetActive(true);
        optionUI.SetActive(false);
    }
    public void GetItemByCategory(string category)
    {
        foreach(Equipment item in items)
        {
            if (item.equipSlot.ToString().Contains(category))
            {
                itemImage.GetComponent<Image>().sprite = item.icon;
                itemName.GetComponent<TMP_Text>().text = item.itemName;
                itemPrice.GetComponent<TMP_Text>().text = item.itemPrice.ToString();
                if (buyItem)
                {
                    InventoryManagement.instance.Add(item);
                    buyItem = false;
                }
            }
        }
    }
    public void BuyItem()
    {
        buyItem = true;
        GetItemByCategory(tempCategory);
    }
    public void SwordCategory()
    {
        swordCategory.color = Color.yellow;
        armorCategory.color = Color.white;
        energyCategory.color = Color.white;
        categoryName.text = swordCategory.text;
        itemDisplay.SetActive(true);
        tempCategory = "Weapon";
        GetItemByCategory(tempCategory);
    }
    public void ArmorCategory()
    {
        swordCategory.color = Color.white;
        armorCategory.color = Color.yellow;
        energyCategory.color = Color.white;
        categoryName.text = armorCategory.text;
        itemDisplay.SetActive(true);
        tempCategory = "Armor";
        GetItemByCategory(tempCategory);
    }
    
    public void EnergyCategory()
    {
        swordCategory.color = Color.white;
        armorCategory.color = Color.white;
        energyCategory.color = Color.yellow;
        categoryName.text = energyCategory.text;
        itemDisplay.SetActive(true);
        tempCategory = "HealPotion";
        GetItemByCategory(tempCategory);
    }

}
