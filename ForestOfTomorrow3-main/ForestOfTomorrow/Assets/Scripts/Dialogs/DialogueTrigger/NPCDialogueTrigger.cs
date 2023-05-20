using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue endBossDialogue;
    public void TriggerNPCDialogue()
    {
        FindObjectOfType<DialogueManagement>().StartNPCDialogue(dialogue);
    }
    public void TriggerNPCEndBossDialogue()
    {
        FindObjectOfType<DialogueManagement>().StartNPCEndBossDialogue(endBossDialogue);
    }
}
