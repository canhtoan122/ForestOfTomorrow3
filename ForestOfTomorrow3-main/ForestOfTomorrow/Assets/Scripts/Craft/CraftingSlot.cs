using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour
{
    public Image slotImage;
    public Item item;
    public int index;

    public void EnableImage(Sprite sprite)
    {
        slotImage.sprite = sprite;
        slotImage.gameObject.SetActive(true);
    }
    public void DisableImage()
    {
        slotImage.sprite = null;
        slotImage.gameObject.SetActive(false);
    }
}
