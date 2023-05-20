using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : StateMachineBehaviour
{
    private Enemy enemy;
    private bool playerTriggered = false;
    private bool masterNextSentence = false;
    private bool masterNextSentence2 = false;
    private bool playerNextSentence = false;
    private bool endDialog = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        //  Trigger end boss dialogue
        enemy.TriggerEndBossDialogue();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(endDialog)
            {
                enemy.EndDialogue();
            }
            if(playerNextSentence)
            {
                enemy.PlayerNextSentence();
                endDialog = true;
            }
            else
            {
                enemy.TriggerPlayerDialogue();
            }
            if (masterNextSentence2)
            {
                enemy.MasterNextSentence();
                masterNextSentence = false;
                playerNextSentence = true;
            }
            if (masterNextSentence)
            {
                enemy.TriggerMasterDialog();
                masterNextSentence2 = true;
            }
            if (playerTriggered)
            {
                enemy.TriggerNPCDialogue();
                masterNextSentence = true;
            }
            
            playerTriggered = true;
        }
    }
}
