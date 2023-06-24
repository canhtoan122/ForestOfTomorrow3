using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HienDialogTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public void TriggerHienDialogue()
    {
        FindObjectOfType<DialogueManagement>().StartHienDialogue(dialogue);
    }
}
