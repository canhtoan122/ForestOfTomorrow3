using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] 
    private LayerMask playerLayer;  // The layer that the player is on
    [SerializeField] 
    private float detectionRadius;  // The radius of the detection range
    [SerializeField] 
    private float moveSpeed;  // The speed at which the NPC moves towards the player
    [SerializeField]
    private GameObject menuButton;   // Menu Button
    [SerializeField]
    private GameObject bossHPBar;    // Boss HP Bar
    [SerializeField]
    private Transform attackPoint;  // Boss Attack
    [SerializeField]
    private float attackRange; // The attack range of the attack point
    [SerializeField]
    private int attackDamage;   // The attack damage of the attack point
    [SerializeField]
    private Animator playerAnimation;   // Player hurt animation
    [SerializeField]
    private GameObject player;
    private bool playerDetected;  // Whether the player is detected or not
    private Transform playerTransform;  // The transform of the detected player
    private Collider2D bossCollider; //  The boss collider
    public Collider2D passThroughCollider;   //  The Player collider
    public TMP_Text bossHP; //  The boss HP Text
    public BossHPBar bossHPFill; // The boss HP fill
    public PlayerStats playerStats;
    public GameObject slash;    // The slash projectile
    public float slashSpeed;    // The slash projectile speed
    public Animator notification;   // The slash projectile notification

    private Animator animator;
    public int maxHealth = 100;
    int currentHealth;

    private bool isPassable = false;
    private bool isDashingAway = false;
    private bool nearPlayer = false;
    private bool playerDead = false;
    private bool longRangeAttack = false;

    public enum MovementState { idle, running, attacking, dashingAway, rangeSlash}
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        bossHPFill.SetMaxHealth(maxHealth);
        bossCollider = GetComponent<BoxCollider2D>();
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
        // Move towards the player if they are detected
        if (playerDetected)
        {
            //  Face the target
            FaceTarget();
            if (!nearPlayer)
            {
                // Move toward force
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
            }
            // Set Destination
            float distance = Vector2.Distance(transform.position, playerTransform.position);
            if (distance <= 2f)
            {
                nearPlayer = true;
            }
            else
            {
                nearPlayer = false;
            }
            menuButton.SetActive(false);
            bossHPBar.SetActive(true);
        }
    }
    public void AttackingPlayer()
    {
        // Move towards the player if they are detected
        if (playerDetected)
        {
            //  Face the target
            FaceTarget();
            // Set Destination
            float distance = Vector2.Distance(transform.position, playerTransform.position);
            if (distance <= 2f)
            {
                nearPlayer = true;
            }
            else
            {
                nearPlayer = false;
            }
        }
        // Move pass player collider
        if (isPassable)
        {
            Physics2D.IgnoreCollision(bossCollider, passThroughCollider, true);
        }
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
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Draw the detection range gizmo in the editor for debugging purposes
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        if (attackPoint == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Update enemy health
        bossHP.text = currentHealth.ToString();
        bossHPFill.SetHealth(currentHealth);
        //Play hurt animation
        animator.SetTrigger("Hurt");

        // Boss is not attacking, so check if it should dash
        float dashawayProbability = 0.2f;
        if(Random.value < dashawayProbability)
        {
            isDashingAway = true;
            float teleportDistance = 30f;
            // Determine a random direction for the dash attack
            Vector2 randomPosition = new Vector2(Random.Range(transform.position.x - teleportDistance, transform.position.x + teleportDistance), transform.position.y);
            randomPosition.x = Mathf.Clamp(randomPosition.x, -16f, 43f);

            // Update the boss's position during the dash away
            transform.position = randomPosition;

            if (currentHealth <= 75)
            {
                animator.SetTrigger("LongRangeAttack");
                notification.SetTrigger("LongRangeAttack");
            }
        }

        if (currentHealth <= 0)
        {
            Die();
            bossHP.gameObject.SetActive(false);
            bossHPBar.SetActive(false);
            menuButton.SetActive(true);
        }
    }
    public void Slash()
    {
        // Set the direction of the bullet to the right (default)
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Spawn a new instance of the bullet prefab at the position of the player
        GameObject clone = Instantiate(slash, transform.position, Quaternion.identity);

        // Set the velocity of the bullet based on the direction and speed
        Rigidbody2D bulletRigidbody = clone.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = direction * slashSpeed;

    }
    public void DamagePlayer(int Damage)
    {
        if (!playerDead)
        {
            //  Play player hurt animation
            //playerAnimation.SetTrigger("Hurt");
            // Detect enemy in range of attack
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

            Damage = attackDamage;
            //Damage them
            foreach (Collider2D player in hitPlayer)
            {
                player.GetComponent<PlayerStats>().TakeDamage(Damage);
            }
            // Update health bar
            playerStats.UpdateHealthBar();
            if (playerStats.currentHealth <= 0)
            {
                PlayerDie();
            }
        }
    }
    public void PlayerDie()
    {
        Debug.Log("Player died!");

        // Die animation
        bool isEquipSword = playerAnimation.GetBool("EquipSword");
        if (!isEquipSword)
        {
            playerAnimation.SetBool("IsDead", true);
        }
        else
        {
            playerAnimation.SetBool("SwordDead", true);
        }

        // Disable the player
        player.GetComponent<Collider2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        playerDead = true;
    }
    public void EndDashAway()
    {
        isDashingAway = false;
        animator.SetBool("DashAway", false);
    }
    public void Die()
    {
        Debug.Log("Enemy died!");

        // Die animation
        animator.SetTrigger("IsDead");

        // Disable the enemy
        this.enabled = false;   
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
    public void UpdateMovementAnimation()
    {
        MovementState state;
        if (playerDetected && !nearPlayer)
        {
            state = MovementState.running;
        }
        else if(playerDetected && nearPlayer && !isDashingAway)
        {
            state = MovementState.attacking;
        }
        else if (isDashingAway)
        {
            state = MovementState.dashingAway;
        }
        else
        {
            state = MovementState.idle;
        }
        animator.SetInteger("state", (int)state);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPassable = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPassable = false;
        }
    }
}
