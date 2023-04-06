using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpeningTapToContinue : MonoBehaviour
{
    public GameObject movementUI;
    public GameObject missionPanel;
    public GameObject menuButtonUI;
    public GameObject dialoguePanel;
    public GameObject masterDialog;
    public GameObject NPCDialog;

    public PlayerDialogueTrigger playerDialogueTrigger;
    public DialogueManagement dialogueManagement;
    private void Start()
    {
        playerDialogueTrigger.TriggerPlayerDialogue();
        movementUI.SetActive(false);
        missionPanel.SetActive(false);
        menuButtonUI.SetActive(false);
        dialoguePanel.SetActive(true);
        masterDialog.SetActive(false);
        NPCDialog.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    dialogueManagement.DisplayNextSentence();
        //}
        if (Input.GetMouseButtonDown(0))
        {
            dialogueManagement.PlayerDisplayNextSentence();
        }
    }
}
