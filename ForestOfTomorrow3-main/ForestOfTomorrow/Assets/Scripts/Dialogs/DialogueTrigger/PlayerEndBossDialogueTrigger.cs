using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEndBossDialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue dialogue2;
    public void TriggerPlayerEndBossDialogue()
    {
        FindObjectOfType<DialogueManagement>().StartPlayerEndBossDialogue(dialogue);
    }
    public void TriggerPlayerEndBossDialogue2()
    {
        FindObjectOfType<DialogueManagement>().StartPlayerEndBossDialogue(dialogue2);
    }
}
