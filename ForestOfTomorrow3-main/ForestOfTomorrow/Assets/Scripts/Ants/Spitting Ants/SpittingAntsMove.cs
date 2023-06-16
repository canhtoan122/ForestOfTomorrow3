using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpittingAntsMove : StateMachineBehaviour
{
    private SpittingAntsController spittingAntsController;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spittingAntsController = animator.GetComponent<SpittingAntsController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spittingAntsController.Patrol();
        spittingAntsController.DetectPlayer();
        spittingAntsController.KeepDistance();
        spittingAntsController.AttackingPlayer();
        spittingAntsController.UpdateMovementAnimation();
    }
}
