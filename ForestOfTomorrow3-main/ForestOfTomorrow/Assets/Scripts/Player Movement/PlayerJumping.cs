using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumping : StateMachineBehaviour
{
    private PlayerController player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<PlayerController>();
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player.Jumping();
        player.UpdateMovementAnimation();
    }
}
