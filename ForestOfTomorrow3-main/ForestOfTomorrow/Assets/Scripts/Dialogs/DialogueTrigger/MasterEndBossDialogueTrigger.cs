using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterEndBossDialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue dialogue2;
    public Dialogue dialogue3;

    public void TriggerMasterEndBossDialogue()
    {
        FindObjectOfType<DialogueManagement>().StartMasterEndBossDialogue(dialogue);
    }
    public void TriggerMasterEndBossDialogue2()
    {
        FindObjectOfType<DialogueManagement>().StartMasterEndBossDialogue(dialogue2);
    }
    public void TriggerMasterEndBossDialogue3()
    {
        FindObjectOfType<DialogueManagement>().StartMasterEndBossDialogue(dialogue3);
    }
}
