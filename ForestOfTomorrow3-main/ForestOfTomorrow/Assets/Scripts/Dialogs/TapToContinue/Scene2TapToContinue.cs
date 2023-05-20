using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2TapToContinue : MonoBehaviour
{
    public GameObject missionPanel;
    public GameObject menuButtonUI;
    public GameObject dialoguePanel;
    public GameObject masterDialog;
    public GameObject NPCDialog;
    private bool scene2Dialog = false;

    public PlayerDialogueTrigger playerDialogueTrigger;
    public DialogueManagement dialogueManagement;
    public NPCDialogueTrigger NPCDialogueTrigger;

    public static bool NPCEndDialog = false;
    private void Start()
    {
        playerDialogueTrigger.TriggerPlayerDialogue();
        ControllerUI.Instance.ActiveMovementUI(false);
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
        string sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "Scene 2")
        {
            scene2Dialog = true;
        }
        if(!scene2Dialog)
        {
            return;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                dialogueManagement.PlayerDisplayNextSentence();
                bool endAnimation = EndAnimation.endAnimation;
                if (NPCEndDialog && endAnimation == true)
                {
                    dialogueManagement.NPCDisplayNextSentence();

                }
            }
        }
    }
}
