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
    private NPCDialogueTrigger NPCDialogueTrigger;
    private Queue<string> sentences;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject movementUI;
    [SerializeField]
    private GameObject missionPanel;
    [SerializeField]
    private GameObject menuButtonUI;
    [SerializeField]
    private GameObject dialoguePanel;
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
    void EndDialogue()
    {
        movementUI.SetActive(true);
        missionPanel.SetActive(true);
        menuButtonUI.SetActive(true);
        dialoguePanel.SetActive(false);
    }
}
