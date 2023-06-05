using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpenterAntsController : MonoBehaviour
{
    public float moveSpeed;    // The move speed of the boss
    public List<Transform> patrolPoints = new List<Transform>();    // The patrol point when boss patrol

    private Animator animator;  //  The game object animator
    private int currentPatrolIndex = 0; // The current patrol point
    private bool playerDetected = false;  // Whether the player is detected or not
    public enum MovementState { move, attack }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Patrol()
    {
        if (!playerDetected)
        {
            if (patrolPoints.Count > 0)
            {
                Transform currentPatrolPoint = patrolPoints[currentPatrolIndex];
                Vector3 direction = (currentPatrolPoint.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;

                if (Vector3.Distance(transform.position, currentPatrolPoint.position) < 0.1f)
                {
                    currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
                    if (currentPatrolIndex == 0)
                    {
                        GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().flipX = false;
                    }
                }
            }
        }
    }
    //public void UpdateMovementAnimation()
    //{
    //    MovementState state;
    //    if (nearPlayer)
    //    {
    //        state = MovementState.attack;
    //    }
    //    else
    //    {
    //        state = MovementState.move;
    //    }
    //    animator.SetInteger("state", (int)state);
    //}
}
