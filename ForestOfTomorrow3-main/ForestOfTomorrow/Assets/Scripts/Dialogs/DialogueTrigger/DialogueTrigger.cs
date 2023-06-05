using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue dialogue2;
    public Dialogue dialogue3;
    public void TriggerMasterDialogue()
    {
        FindObjectOfType<DialogueManagement>().StartMasterDialogue(dialogue);
    }
    public void TriggerMasterDialogue2()
    {
        FindObjectOfType<DialogueManagement>().StartMasterDialogue(dialogue2);
    }
    public void TriggerMasterDialogue3()
    {
        FindObjectOfType<DialogueManagement>().StartMasterDialogue(dialogue3);
    }
}
