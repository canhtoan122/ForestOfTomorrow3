using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class CraftController : MonoBehaviour
{
    #region Singleton
    public static CraftController instance;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject craftUI;
    public Image customCursor;
    public InventoryData inventoryData;

    public CraftingSlot[] slot;

    public List<Item> itemList;
    public string[] recipes;
    public Item[] recipeResults;
    public CraftingSlot resultSlot;

    private Item currentItem;

    private void Update()
    {
        if (craftUI.active)
        {
            ControllerUI.Instance.ActiveMovementUI(false);
        }
    }
    public void OnMouseUpItem()
    {
        if (currentItem != null)
        {
            customCursor.gameObject.SetActive(false);
            CraftingSlot nearestSlot = null;
            float shortestDistance = float.MaxValue;

            foreach (CraftingSlot slot in slot)
            {
                float distance = Vector2.Distance(Input.mousePosition, slot.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestSlot = slot;
                }
            }
            nearestSlot.gameObject.SetActive(true);
            nearestSlot.EnableImage(currentItem.icon);
            nearestSlot.item = currentItem;
            itemList[nearestSlot.index] = currentItem;
            currentItem = null;

            CheckForCreatedRecipes();
        }
    }
    public void CheckForCreatedRecipes()
    {
        resultSlot.gameObject.SetActive(false);
        resultSlot.item = null;

        string currentRecipeString = "";
        foreach(Item item in itemList)
        {
            if(item != null)
            {
                currentRecipeString += item.itemName;
            }
            else
            {
                currentRecipeString += "null";
            }
        }
        for(int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i] == currentRecipeString)
            {
                resultSlot.gameObject.SetActive(true);
                resultSlot.GetComponent<Image>().sprite = recipeResults[i].icon;
                resultSlot.item = recipeResults[i];
                RemoveItemIfSuccess();
            }
        }
    }
    public void RemoveItemIfSuccess()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            slot[i].GetComponent<CraftingSlot>().item = null;
            slot[i].GetComponent<CraftingSlot>().DisableImage();
            itemList[i] = null;
        }
    }
    public void OnSlotDown(CraftingSlot slot)
    {
        if(slot.item == null)
        {
            return;
        }
        customCursor.gameObject.SetActive(true);
        customCursor.sprite = slot.item.icon;
        itemList[slot.index] = null;
        slot.DisableImage();
    }
    public void OnClickSlot(CraftingSlot slot)
    {
        InventoryManagement.instance.Add(slot.item);
        inventoryData.AddEquipment(slot.item);
        slot.item = null;
        customCursor.gameObject.SetActive(false);
        customCursor.sprite = null;
        if(slot.gameObject.name == "Result Slot")
        {
            slot.gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        ControllerUI.Instance.OnInteractTriggered += OpenCraftUI;
    }

    private void OnDisable()
    {
        ControllerUI.Instance.OnInteractTriggered -= OpenCraftUI;
    }
    public void ActivatePickUpButton()
    {
        ControllerUI.Instance.ActiveAttackButton(false);
        ControllerUI.Instance.SetInteractState(EInteractState.OPENCRAFT);
        ControllerUI.Instance.ActiveInteractButton(true);
    }
    public void DeActivatePickUpButton()
    {
        ControllerUI.Instance.ActiveAttackButton(true);
        ControllerUI.Instance.SetInteractState(EInteractState.NONE);
        ControllerUI.Instance.ActiveInteractButton(false);
    }
    public void OpenCraftUI(EInteractState interactState)
    {
        if (interactState != EInteractState.OPENCRAFT)
        {
            return;
        }
        ControllerUI.Instance.ActiveMovementUI(false);
        craftUI.SetActive(true);
    }
    public void CloseCraftUI()
    {
        ControllerUI.Instance.ActiveMovementUI(true);
        craftUI.SetActive(false);
    }
    public void OnMouseDownItem(Item item)
    {
        if(currentItem == null)
        {
            currentItem = item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.icon;
        }
    }

}
