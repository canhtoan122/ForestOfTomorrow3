using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToContinue : MonoBehaviour
{
    public GameObject movementUI;
    public GameObject missionPanel;
    public GameObject menuButtonUI;
    public GameObject dialoguePanel;

    public DialogueTrigger dialogueTrigger;
    public DialogueManagement dialogueManagement;

    public static bool MasterEndDialog = false;
    private void Start()
    {
        dialogueTrigger.TriggerMasterDialogue();
        movementUI.SetActive(false);
        missionPanel.SetActive(false);
        menuButtonUI.SetActive(false);
        dialoguePanel.SetActive(true);

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
            dialogueManagement.MasterDisplayNextSentence();
            bool endAnimation = EndAnimation.endAnimation;
            if(MasterEndDialog && endAnimation == true)
            {
                dialogueManagement.PlayerDisplayNextSentence();
            }

        }
    }
}
