using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToContinue : MonoBehaviour
{
    public GameObject movementUI;
    public GameObject missionPanel;
    public GameObject menuButtonUI;
    public GameObject dialoguePanel;
    public GameObject playerDialog;

    public DialogueTrigger dialogueTrigger;
    public MasterEndBossDialogueTrigger masterEndBossDialogueTrigger;
    public PlayerEndBossDialogueTrigger playerEndBossDialogueTrigger;
    public NPCDialogueTrigger NPCDialogueTrigger;
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
        if (DialogueManagement.dialogEnd == true)
            return;
        if (Enemy.bossDied == true)
            return;
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

    public void EndBossDialogue()
    {
        masterEndBossDialogueTrigger.TriggerMasterEndBossDialogue();
        movementUI.SetActive(false);
        missionPanel.SetActive(false);
        menuButtonUI.SetActive(false);
        playerDialog.SetActive(false);
        dialoguePanel.SetActive(true);
    }
    public void ActivePlayerDialog()
    {
        playerDialog.SetActive(true);
        playerEndBossDialogueTrigger.TriggerPlayerEndBossDialogue();
    }
    public void NPCDialogTrigger()
    {
        NPCDialogueTrigger.TriggerNPCEndBossDialogue();
    }
    public void TriggerMasterDialog()
    {
        masterEndBossDialogueTrigger.TriggerMasterEndBossDialogue2();
    }
    public void MasterNextSentence()
    {
        masterEndBossDialogueTrigger.TriggerMasterEndBossDialogue3();
    }
    public void PlayerNextSentence()
    {
        playerEndBossDialogueTrigger.TriggerPlayerEndBossDialogue2();
    }
    public void EndDialogue()
    {
        dialogueManagement.EndBossDialogue();
    }
}
