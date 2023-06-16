using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CarpenterAntsController : MonoBehaviour
{
    public float moveSpeed;    // The move speed of the boss
    public float detectionRadius;  // The radius of the detection range
    public float attackRange; // The attack range of the attack point
    public GameObject fireBallAttackPoint;  // Carpenter Ants Attack point
    public float rotationSpeed;     // The rotation of the fire ball
    public LayerMask playerLayer;  // The layer that the player is on
    public Transform playerTransform;  // The transform of the detected player

    private Animator animator;  //  The game object animator
    private bool playerDetected = false;  // Whether the player is detected or not
    private bool nearPlayer = false; // Whether the player is in the attack range or not
    public enum MovementState { move, attack }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        // Move toward player
        DetectPlayer();
        // Detect player is in attack range or not
        RunningToPlayer();
        // Attack player
        AttackingPlayer();
        // Detect is get hit or not
        // Limited the fly away height
        // If the boss is get hit, fly away
        // if the boss is below 1/3 health, activate stage 2

        UpdateMovementAnimation();
    }
    public void DetectPlayer()
    {
        // Check if the player is within the detection range
        Collider2D[] detectedPlayers = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
        if (detectedPlayers.Length > 0)
        {
            playerDetected = true;
            // The player has been detected, so do something here (e.g. attack, follow)
            playerTransform = detectedPlayers[0].transform;  // Assume there is only one detected player

        }
        else
        {
            playerDetected = false;
        }
    }
    public void RunningToPlayer()
    {
        //Move towards the player if they are detected
        if (playerDetected)
        {
            if (!nearPlayer)
            {
                //Move toward force
                Vector2 targetPosition = new Vector2(playerTransform.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            }
            //Set Destination
            float distance = Vector2.Distance(transform.position, playerTransform.position);
            if (distance <= attackRange)
            {
                nearPlayer = true;
                fireBallAttackPoint.GetComponent<Animator>().SetBool("FireBall", true);
            }
            else
            {
                nearPlayer = false;
            }
        }
    }
    public void AttackingPlayer()
    {
        //Move towards the player if they are detected
        if (playerDetected)
        {
            //Set Destination
            float distance = Vector2.Distance(transform.position, playerTransform.position);
            if (distance <= attackRange)
            {
                nearPlayer = true;

            }
            else
            {
                nearPlayer = false;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        if (fireBallAttackPoint == null)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Collider2D otherGameObject = collision.gameObject.GetComponent<Collider2D>();
            Collider2D thisGameObject = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(thisGameObject, otherGameObject);
        }
    }
    public void UpdateMovementAnimation()
    {
        MovementState state;
        if (nearPlayer)
        {
            state = MovementState.attack;
        }
        else
        {
            state = MovementState.move;
        }
        animator.SetInteger("state", (int)state);
    }
}
