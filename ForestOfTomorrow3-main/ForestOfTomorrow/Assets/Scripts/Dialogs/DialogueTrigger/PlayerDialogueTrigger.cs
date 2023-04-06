using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public void TriggerPlayerDialogue()
    {
        FindObjectOfType<DialogueManagement>().StartPlayerDialogue(dialogue);
    }
}
