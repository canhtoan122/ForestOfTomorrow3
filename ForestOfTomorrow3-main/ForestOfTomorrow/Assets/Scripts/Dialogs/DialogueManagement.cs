using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManagement : MonoBehaviour
{
    [SerializeField]
    private TMP_Text MasterName;
    [SerializeField]
    private TMP_Text MasterDialog;
    [SerializeField]
    private TMP_Text PlayerName;
    [SerializeField]
    private TMP_Text PlayerDialog;
    [SerializeField]
    private TMP_Text NPCName;
    [SerializeField]
    private TMP_Text NPCDialog;
    [SerializeField]
    private PlayerDialogueTrigger playerDialogueTrigger;
    [SerializeField]
    private PlayerEndBossDialogueTrigger playerEndBossDialogueTrigger;
    [SerializeField]
    private MasterEndBossDialogueTrigger masterEndBossDialogueTrigger;
    [SerializeField]
    private NPCDialogueTrigger NPCDialogueTrigger;
    [SerializeField]
    private GameObject tutorialPanel;
    [SerializeField]
    private GameObject step1;
    [SerializeField]
    private GameObject step6;
    private Queue<string> sentences;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject missionPanel;
    [SerializeField]
    private GameObject menuButtonUI;
    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private GameObject gameInstructionPanel;
    [SerializeField]
    private Transform playerPosition;

    public static bool dialogEnd = false;
    void Awake()
    {
        sentences = new Queue<string>();
    }
    public void StartMasterDialogue(Dialogue dialogue)
    {
        MasterName.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        MasterDisplayNextSentence();
    }
    public void MasterDisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            animator.SetBool("IsSliding", true);
            playerDialogueTrigger.TriggerPlayerDialogue();
            TapToContinue.MasterEndDialog = true;
            return;
        }

        string sentence = sentences.Dequeue();
        MasterDialog.text = sentence;
    }
    public void StartPlayerDialogue(Dialogue dialogue)
    {
        PlayerName.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        PlayerDisplayNextSentence();
    }
    public void PlayerDisplayNextSentence()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "Scene 2")
        {
            if (sentences.Count == 0)
            {
                animator.SetBool("NPCIsSliding", true);
                NPCDialogueTrigger.TriggerNPCDialogue();
                Scene2TapToContinue.NPCEndDialog = true;
                return;
            }

            string sentence = sentences.Dequeue();
            PlayerDialog.text = sentence;
        }
        else
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();
            PlayerDialog.text = sentence;
        }
    }
    public void StartNPCDialogue(Dialogue dialogue)
    {
        NPCName.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        NPCDisplayNextSentence();
    }
    public void NPCDisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        NPCDialog.text = sentence;
    }
    public void StartMasterEndBossDialogue(Dialogue dialogue)
    {
        MasterDialog.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        MasterEndBossDisplayNextSentence();
    }
    public void MasterEndBossDisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            return;
        }
        string sentence = sentences.Dequeue();
        MasterDialog.text = sentence;
    }
    public void StartPlayerEndBossDialogue(Dialogue dialogue)
    {
        PlayerDialog.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        PlayerEndBossDisplayNextSentence();
    }
    public void PlayerEndBossDisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            return;
        }

        string sentence = sentences.Dequeue();
        PlayerDialog.text = sentence;
    }
    public void StartNPCEndBossDialogue(Dialogue dialogue)
    {
        animator.SetBool("Scene3IsSliding", true);
        NPCName.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        NPCEndBossDisplayNextSentence();
    }
    public void NPCEndBossDisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            return;
        }

        string sentence = sentences.Dequeue();
        NPCDialog.text = sentence;
    }
    public void EndBossDialogue()
    {
        ControllerUI.Instance.ActiveMovementUI(true);
        missionPanel.SetActive(true);
        menuButtonUI.SetActive(true);
        dialoguePanel.SetActive(false);
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Scene 3")
        {
            Vector2 gameInstructionDisappearposition = new Vector2(-4, playerPosition.position.y);
            if (playerPosition.position.x <= gameInstructionDisappearposition.x)
            {
                gameInstructionPanel.SetActive(false);
            }
            else
            {
                gameInstructionPanel.SetActive(true);
            }
        }
    }
    void EndDialogue()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Scene 3")
        {
            dialogEnd = true;
        }
        ControllerUI.Instance.ActiveMovementUI(true);
        missionPanel.SetActive(true);
        menuButtonUI.SetActive(true);
        dialoguePanel.SetActive(false);
        if (sceneName == "Scene 3")
        {
            tutorialPanel.SetActive(true);
            step1.SetActive(false);
            step6.SetActive(true);
        }
        NPCMovementController.canMove = true;
        if (sceneName == "Scene 2")
        {
            gameInstructionPanel.SetActive(true);
        }
    }
}
