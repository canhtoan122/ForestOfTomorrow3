using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpittingAntsController : MonoBehaviour
{
    public float detectionRadius;  // The radius of the detection range
    public float retreatDistance;
    public Transform playerTransform;  // The transform of the detected player
    public Transform attackPoint;  // Dinoponera Ants Attack
    public float attackRange; // The attack range of the attack point
    public float moveSpeed = 5f;    // The move speed of the enemy
    public PlayerStats playerStats; //  Get the player stat to apply curent damage
    public LayerMask playerLayer;  // The layer that the player is on
    public List<Transform> patrolPoints = new List<Transform>();    // The patrol point when enemy patrol
    private Animator animator;   // The animation of Dinoponera Ants
    private SpittingAntsStats spittingAntsStats;    // Get The Dinoponera Ants Stats like damage, armor,...


    public static bool playerDetected = false;  // Whether the player is detected or not
    private bool nearPlayer = false; // Whether the player is in the attack range or not
    private int currentPatrolIndex = 0; // The current patrol point

    public enum MovementState { move, attack }
    private void Start()
    {
        animator = GetComponent<Animator>();
        spittingAntsStats = GetComponent<SpittingAntsStats>();
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
                        GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().flipX = true;
                    }
                }
            }
        }
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
    public void KeepDistance()
    {
        if (playerDetected)
        {
            //Face the player
            FaceTarget();
            // Calculate the distance between enemy and player
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            // Check if the player is within the detection radius
            if (distance < detectionRadius)
            {
                // Check if the player is within the retreat distance
                if (distance < retreatDistance)
                {
                    // Calculate the direction away from the player
                    Vector3 direction = transform.position - playerTransform.position;

                    // Normalize the direction vector
                    direction.Normalize();

                    // Move away from the player
                    transform.position += direction * moveSpeed * Time.deltaTime;
                }
                else
                {
                    // Calculate the direction towards the player
                    Vector3 direction = playerTransform.position - transform.position;

                    // Normalize the direction vector
                    direction.Normalize();

                    // Move towards the player
                    transform.position += direction * moveSpeed * Time.deltaTime;
                }
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
    // Draw the detection range gizmo in the editor for debugging purposes
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, retreatDistance);
        if (attackPoint == null)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void FaceTarget()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Get the facing direction of the enemy character (assuming forward is to the right)
        float facingThreshold = 0.9f;
        Vector2 facingDirection = transform.right;

        // Check if the facing direction is greater than the given threshold
        float dotProduct = Vector2.Dot(facingDirection, direction);
        if (dotProduct >= facingThreshold)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    public void DamagePlayer()
    {
        // Detect enemy in range of attack
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        int damage = spittingAntsStats.damage.GetStat();
        //Damage them
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerStats>().TakeDamage(damage);
        }

        // Update health bar
        PlayerStats.instance.UpdateHealthBar();
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Collider2D otherGameObject = collision.gameObject.GetComponent<Collider2D>();
            Collider2D thisGameObject = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(thisGameObject, otherGameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            Collider2D otherGameObject = collision.gameObject.GetComponent<Collider2D>();
            Collider2D thisGameObject = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(thisGameObject, otherGameObject);
        }
    }
}
