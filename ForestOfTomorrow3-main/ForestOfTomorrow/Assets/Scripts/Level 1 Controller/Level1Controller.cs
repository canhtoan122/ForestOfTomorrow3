using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1Controller : MonoBehaviour
{
    public GameObject door;
    public Sprite openDoorSprite;
    public GameObject keyGameObject;
    public Equipment key;
    public GameObject behindDoorbG;
    public GameObject player;
    public GameObject mapUI;
    public GameObject homeButton;
    public GameObject level1Button;
    public Sprite currentLevelButton;
    public Sprite homeLevelButton;
    public GameObject sideQuestUI;
    public GameObject sideQuestText;
    public Mission sideQuest;
    public Mission sideQuest1;

    public static bool haveKey = false;
    public static Vector2 lastCheckPointPosition;
    private void OnEnable()
    {
        ControllerUI.Instance.OnInteractTriggered += PickUpKey;
        ControllerUI.Instance.OnInteractTriggered += OpenDoor;
        ControllerUI.Instance.OnInteractTriggered += ActivateMap;
    }

    private void OnDisable()
    {
        ControllerUI.Instance.OnInteractTriggered -= PickUpKey;
        ControllerUI.Instance.OnInteractTriggered -= OpenDoor;
        ControllerUI.Instance.OnInteractTriggered -= ActivateMap;
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
        ControllerUI.Instance.SetInteractState(EInteractState.OPEN);
        ControllerUI.Instance.ActiveInteractButton(true);
    }

    public void OpenTeleportPortal()
    {
        ControllerUI.Instance.ActiveAttackButton(false);
        ControllerUI.Instance.SetInteractState(EInteractState.ACTIVEMAP);
        ControllerUI.Instance.ActiveInteractButton(true);
        LoadLevel();
    }
    public void PickUpKey(EInteractState interactState)
    {
        if (interactState == EInteractState.PICKUPKEY)
        {
            InventoryManagement.instance.Add(key);
            keyGameObject.SetActive(false);
        }
    }
    public void OpenDoor(EInteractState interactState)
    {
        if (interactState == EInteractState.OPEN)
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
    public void ActivateMap(EInteractState interactState)
    {
        if(interactState != EInteractState.ACTIVEMAP)
        {
            return;
        }
        ControllerUI.Instance.ActiveMovementUI(false);
        mapUI.SetActive(true);
    }
    public void DeActiveMap()
    {
        ControllerUI.Instance.ActiveMovementUI(true);
        mapUI.SetActive(false);
    }
    public void CloseTeleportPortal()
    {
        ControllerUI.Instance.ActiveMovementUI(true);
        ControllerUI.Instance.ActiveAttackButton(true);
        ControllerUI.Instance.SetInteractState(EInteractState.NONE);
        ControllerUI.Instance.ActiveInteractButton(false);
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
    public void LoadLevel()
    {
        homeButton.GetComponent<Image>().sprite = homeLevelButton;
        homeButton.GetComponent<Button>().interactable = true;
        level1Button.GetComponent<Image>().sprite = currentLevelButton;
        level1Button.GetComponent<Button>().interactable = true;
    }
    public void Level1()
    {
        ControllerUI.Instance.ActiveAttackButton(true);
        ControllerUI.Instance.ActiveInteractButton(false);
        SceneManager.LoadScene("AP_Level 1");
    }
    public void Home()
    {
        TapToContinue.playerDie = false;
        ControllerUI.Instance.ActiveAttackButton(true);
        ControllerUI.Instance.ActiveInteractButton(false);
        SceneManager.LoadScene("AP_Scene 4");
    }
    public void ActivateSideQuest1()
    {
        Time.timeScale = 0f;
        sideQuestUI.SetActive(true);
        sideQuest1.isActive = true;
        sideQuest.MissionTitle = sideQuest1.MissionTitle;
        sideQuest.MissionDescription = sideQuest1.MissionDescription;
        sideQuest.MissionType = sideQuest1.MissionType;
        sideQuest.MissionProgress = sideQuest1.MissionProgress;
        sideQuestText.GetComponent<TMP_Text>().text = sideQuest.MissionTitle;
    }
    public void AcceptSideQuest1()
    {
        Time.timeScale = 1f;
        sideQuestUI.SetActive(false);
    }
    public void DeclineSideQuest1()
    {
        Time.timeScale = 1f;
        sideQuestUI.SetActive(false);
    }
}
