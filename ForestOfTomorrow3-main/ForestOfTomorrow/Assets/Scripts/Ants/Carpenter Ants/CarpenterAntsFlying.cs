using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpenterAntsFlying : StateMachineBehaviour
{
    private CarpenterAntsController carpenterAntsController;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        carpenterAntsController = animator.GetComponent<CarpenterAntsController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
