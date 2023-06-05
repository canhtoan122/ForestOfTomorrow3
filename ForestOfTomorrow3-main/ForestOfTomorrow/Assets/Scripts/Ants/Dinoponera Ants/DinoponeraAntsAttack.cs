using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoponeraAntsAttack : StateMachineBehaviour
{
    private DinoponeraAntsController dinoponeraAntsController;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dinoponeraAntsController = animator.GetComponent<DinoponeraAntsController>();
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dinoponeraAntsController.DetectPlayer();
        dinoponeraAntsController.RunningToPlayer();
        dinoponeraAntsController.AttackingPlayer();
        dinoponeraAntsController.UpdateMovementAnimation();
    }
}
