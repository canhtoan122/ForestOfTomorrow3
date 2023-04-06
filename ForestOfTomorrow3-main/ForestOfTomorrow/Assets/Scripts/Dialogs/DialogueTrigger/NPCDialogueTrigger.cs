using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public void TriggerNPCDialogue()
    {
        FindObjectOfType<DialogueManagement>().StartNPCDialogue(dialogue);
    }
}
