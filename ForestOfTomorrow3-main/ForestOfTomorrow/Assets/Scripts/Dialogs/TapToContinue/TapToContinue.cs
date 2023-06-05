using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapToContinue : MonoBehaviour
{
    public GameObject missionPanel;
    public GameObject menuButtonUI;
    public GameObject dialoguePanel;
    public GameObject playerDialog;
    public GameObject nPCDialog;

    public DialogueTrigger dialogueTrigger;
    public MasterEndBossDialogueTrigger masterEndBossDialogueTrigger;
    public PlayerEndBossDialogueTrigger playerEndBossDialogueTrigger;
    public NPCDialogueTrigger NPCDialogueTrigger;
    public DialogueManagement dialogueManagement;

    private string currentSceneName;
    private bool masterNextSentence = false;
    private bool playerNextSentence = false;
    private bool endDialog = false;
    public static bool MasterEndDialog = false;
    public static bool playerDie = false;
    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "AP_Scene 4" && playerDie)
        {
            dialogueTrigger.TriggerMasterDialogue2();
            ControllerUI.Instance.ActiveMovementUI(false);
            playerDialog.SetActive(false);
            dialoguePanel.SetActive(true);
            return;
        }
        else if(currentSceneName == "AP_Scene 4")
        {
            return;
        }
        dialogueTrigger.TriggerMasterDialogue();
        ControllerUI.Instance.ActiveMovementUI(false);
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
            if(currentSceneName == "AP_Scene 4")
            {
                NPCDialogueTrigger.TriggerNPCEndBossDialogue();
                nPCDialog.SetActive(true);
                if(masterNextSentence)
                {
                    dialogueTrigger.TriggerMasterDialogue3();
                    if (playerNextSentence)
                    {
                        playerEndBossDialogueTrigger.TriggerPlayerEndBossDialogue();
                        playerDialog.SetActive(true);
                        if (endDialog)
                        {
                            dialogueManagement.EndDialogue();
                        }
                        endDialog = true;
                    }
                    playerNextSentence = true;
                }
                masterNextSentence = true;
                return;
            }
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
        ControllerUI.Instance.ActiveMovementUI(false);
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
