using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Animator animator;   // Reference to the Animator component
    private float jumpForce = 15f;   // The force applied to the character when jumping
    private float moveSpeed = 5f;   // The speed at which the character moves left and right
    public float dashSpeed = 15f;   // The speed that the player after dash
    public float dashDistance = 5f;      // The distance that the player after dash
    public float dashDuration;      // The duration that the player after dash
    private float dashTimeLeft; 
    public float dashCooldown = 1f; // The cooldown of the dash
    public LayerMask groundLayer;   // The layer(s) that represent the ground
    private SpriteRenderer spriteRenderer; //The character image
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    private float dashCooldownLeft = 0f;
    private float horizontalInput = 0f;
    public float attackRange = 0.5f; // The attack range of the attack point
    public LayerMask enemyLayers;
    private int attackDamage;
    private PlayerStats playerStats;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip moveSFX;
    [SerializeField]
    private AudioClip jumpSFX;
    [SerializeField]
    private AudioClip dashSFX;
    [SerializeField]
    private AudioClip attackSFX;
    [SerializeField]
    private Transform attackPoint;

    private bool isGrounded = false;   // Flag to indicate if the character is grounded
    private bool isJumping = false;   // Flag to indicate if the character is jumping
    private bool isStandingUp = false;   // Flag to indicate if the character is standing up
    private bool isDashing = false;   // Flag to indicate if the character is dashing

    private bool jump = false;
    private bool standingUp = false;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool dash = false;
    private bool attack = false;
    public static bool openDoor = false;

    public enum MovementState { idle, walking, jumping, standing, dash, attack}
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        playerStats = GetComponent<PlayerStats>();
    }
    private void Update()
    {
        //if (Input.GetKey(KeyCode.D))
        //{
        //    MoveRight(true);
        //}
        //else
        //{
        //    MoveRight(false);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    MoveLeft(true);
        //}
        //else
        //{
        //    MoveLeft(false);
        //}
        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    Dash();
        //}
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    Jump();
        //}
        //if (Input.GetKey(KeyCode.E))
        //{
        //    Attack();
        //}
        UpdateMovementAnimation();
    }
    // Main moving left right component
    public void Moving()
    {
        if (moveLeft)
        {
            standingUp = false;
            // Move the character to the left
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        else if (moveRight)
        {
            standingUp = false;
            // Move the character to the right
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }
    // Main jumping component
    public void Jumping()
    {
        // Check if the jump button is pressed and the character is grounded
        if (jump && IsGrounded())
        {
            // Set the "IsJumping" flag to true to indicate the character is jumping
            isJumping = true;

            // Apply a force to the character's rigidbody to make them jump
            GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

            // Apply audio SFX into jumping
            audioSource.clip = jumpSFX;
            audioSource.Play();

            // Set the "standingUp" flag to false to indicate the character is no longer jumping
            standingUp = false;

            // Set the "jump" flag to false to indicate the character is no longer jumping
            jump = false;
        }
    }
    // Main attacking component
    public void Attacking()
    {
        if (attack)
        {
            // Play attack sound effect loop
            audioSource.clip = attackSFX;
            audioSource.Play();

            // Detect enemy in range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            // Add Player stat to damage
            attackDamage = playerStats.damage.GetStat();

            //Damage them
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
            attack = false;
        }
    }
    // Main dashing component
    public void Dashing()
    {
        if (dash && moveLeft)
        {
            float moveDirection = -1f;
            float xVelocity = moveDirection * dashSpeed;
            rb.velocity = new Vector2(xVelocity, rb.velocity.y);
        }
        if (dash && moveRight)
        {
            float moveDirection = 1f;
            float xVelocity = moveDirection * dashSpeed;
            rb.velocity = new Vector2(xVelocity, rb.velocity.y);
        }
        dash = false;
    }
    // Activate when user click the jump button
    public void Jump()
    {
        jump = true;
        TutorialManagement.isJumped = true;
        UpdateMovementAnimation();
    }
    // Activate when user click the move left button
    public void MoveLeft(bool _left)
    {
        moveLeft = _left;
        TutorialManagement.isMoved = true;
        if (moveLeft)
        {
            // Apply audio SFX into moving
            audioSource.clip = moveSFX;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            // Stop move sound effect loop
            audioSource.Stop();
            audioSource.loop = false;
        }

    }
    // Activate when user click the move right button
    public void MoveRight(bool _right)
    {
        moveRight = _right;
        TutorialManagement.isMoved = true;
        if (moveRight)
        {
            // Apply audio SFX into moving
            audioSource.clip = moveSFX;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            // Stop move sound effect loop
            audioSource.Stop();
            audioSource.loop = false;
        }
    }
    // Activate when user click the dash button
    public void Dash()
    {
        dash = true;
        TutorialManagement.isDashed = true;
        if (dash && !moveLeft && !moveRight)
        {
            // Play attack sound effect loop
            audioSource.clip = dashSFX;
            audioSource.Play();
        }
        else
        {
            // Stop move sound effect loop
            audioSource.Stop();
        }
    }
    // Activate when user click the attack button
    public void Attack()
    {
        attack = true;
        TutorialManagement.isAttacked = true;
    }
    public void ResetScene()
    {
        DialogueManagement.dialogEnd = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        EquipmentManager.instance.PlayerDeadInScene3();
    }
    // Open door animation
    public void OpenDoor()
    {
        openDoor = true;
    }
    // Activate when user finish standing up
    public void IsIdle()
    {
        standingUp = false;
    }
    // Check if user is on the ground or not
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }
    public void UpdateMovementAnimation()
    {
        MovementState state;
        if (moveLeft && !dash && !jump)
        {
            state = MovementState.walking;
            // Flip backward
            spriteRenderer.flipX = true;
        }
        else if (moveRight && !dash && !jump)
        {
            state = MovementState.walking;
            // Flip forward
            spriteRenderer.flipX = false;
        }
        else if (moveLeft && jump && !dash || moveRight && jump && !dash || jump && !dash)
        {
            state = MovementState.jumping;
        }
        else if (standingUp)
        {
            state = MovementState.standing;
        }
        else if (moveLeft && attack || moveRight && attack || attack)
        {
            state = MovementState.attack;
        }
        else if (moveLeft && dash || moveRight && dash)
        {
           state = MovementState.dash;
        }
        else
        {
            state = MovementState.idle;
        }
        animator.SetInteger("state", (int)state);
    }
    // Check if user touch the ground after jumping to activate standing up
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ground check
        if (collision.gameObject.CompareTag("Terrain"))
        {
            isJumping = false;
            if (!moveLeft || !moveRight)
            {
                if (rb.velocity.y == 0)
                {
                    standingUp = true;
                }
            }
        }
        // Invisible wall check
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Get the normal of the collision
            Vector2 normal = collision.contacts[0].normal;

            // If the collision is from the top or bottom, prevent vertical movement
            if (Mathf.Abs(normal.y) > Mathf.Abs(normal.x))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
            // If the collision is from the left or right, prevent horizontal movement
            else
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }
        //Stair check
        if(collision.gameObject.CompareTag("Stair"))
        {
            rb.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Stair check
        if (collision.gameObject.CompareTag("Stair"))
        {
            rb.GetComponent<Rigidbody2D>().gravityScale = 3f;
        }
        //Wall check
        if (collision.gameObject.CompareTag("Wall"))
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
            if (rb.velocity.y == 0)
            {
                animator.SetBool("IsStandingUp", true);
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

