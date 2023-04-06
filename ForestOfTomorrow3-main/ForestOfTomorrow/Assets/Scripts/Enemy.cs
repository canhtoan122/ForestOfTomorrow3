using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] 
    private LayerMask playerLayer;  // The layer that the player is on
    [SerializeField] 
    private float detectionRadius;  // The radius of the detection range
    [SerializeField] 
    private float moveSpeed;  // The speed at which the NPC moves towards the player
    private bool playerDetected;  // Whether the player is detected or not
    private Transform playerTransform;  // The transform of the detected player

    private Animator animator;
    public int maxHealth = 10;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    // Update is called once per frame
    void Update()
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
        // Move towards the player if they are detected
        if (playerDetected)
        {
            // Set move right animation
            animator.SetBool("isRunning", true);
            // Move toward force
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
    }

    // Draw the detection range gizmo in the editor for debugging purposes
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Play hurt animation
        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy died!");

        // Die animation
        animator.SetBool("IsDead", true);

        // Disable the enemy
        this.enabled = false;   
        GetComponent<Collider2D>().enabled = false;
    }
}
