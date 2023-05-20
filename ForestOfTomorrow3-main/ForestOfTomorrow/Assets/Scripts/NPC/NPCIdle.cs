using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCIdle : StateMachineBehaviour
{
    private NPCMovementController npc;
    private string sceneName;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc = animator.GetComponent<NPCMovementController>();
        sceneName = SceneManager.GetActiveScene().name;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (sceneName == "Scene 4")
        {
            return;
        }
        else
        {
            npc.UpdateMovementAnimation();
        }
    }
}
