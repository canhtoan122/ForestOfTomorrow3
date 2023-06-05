using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class Level1Controller : MonoBehaviour
{
    public GameObject door;
    public Sprite openDoorSprite;
    public GameObject keyGameObject;
    public Equipment key;
    public GameObject behindDoorbG;
    public GameObject player;
    public float moveSpeed = 5f;

    public static bool haveKey = false;

    private void OnEnable()
    {
        ControllerUI.Instance.OnInteractTriggered += PickUpKey;
    }

    private void OnDisable()
    {
        ControllerUI.Instance.OnInteractTriggered -= PickUpKey;
    }
    public void PickUpKey()
    {
        ControllerUI.Instance.ActiveAttackButton(false);
        ControllerUI.Instance.SetInteractState(EInteractState.PICKUPKEY);
        ControllerUI.Instance.ActiveInteractButton(true);
    }
    public void OpenDoor()
    {
        ControllerUI.Instance.ActiveAttackButton(false);
        ControllerUI.Instance.SetInteractState(EInteractState.OPENLEVEL1);
        ControllerUI.Instance.ActiveInteractButton(true);
    }
    public void PickUpKey(EInteractState interactState)
    {
        if (interactState == EInteractState.PICKUPKEY)
        {
            InventoryManagement.instance.Add(key);
            keyGameObject.SetActive(false);
        }
        else if(interactState == EInteractState.OPENLEVEL1)
        {
            InventoryManagement.instance.CheckKey();
            if (haveKey)
            {
                behindDoorbG.SetActive(false);
                door.GetComponent<BoxCollider2D>().enabled = false;
                door.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
                door.GetComponent<Transform>().position = new Vector2(48.93f, 1.63f);
                InventoryManagement.instance.Remove(key);
                haveKey = false;
            }
        }
    }
    public void NotPickUpKey()
    {
        ControllerUI.Instance.ActiveMovementUI(true);
        ControllerUI.Instance.ActiveAttackButton(true);
        ControllerUI.Instance.SetInteractState(EInteractState.NONE);
        ControllerUI.Instance.ActiveInteractButton(false);
    }
    public void DeactiveOpenDoor()
    {
        ControllerUI.Instance.ActiveMovementUI(true);
        ControllerUI.Instance.ActiveAttackButton(true);
        ControllerUI.Instance.SetInteractState(EInteractState.NONE);
        ControllerUI.Instance.ActiveInteractButton(false);
    }
    
}
