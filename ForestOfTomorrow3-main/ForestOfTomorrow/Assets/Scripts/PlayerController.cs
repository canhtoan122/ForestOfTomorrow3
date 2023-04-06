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
    public int attackDamage = 2;

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
    private bool isMovingLeft = false;   // Flag to indicate if the character is moving left
    private bool isMovingRight = false;   // Flag to indicate if the character is moving right
    private bool isJumping = false;   // Flag to indicate if the character is jumping
    private bool isStandingUp = false;   // Flag to indicate if the character is standing up
    private bool isDashing = false;   // Flag to indicate if the character is dashing

    private bool jump = false;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool dash = false;
    private bool attack = false;
    public static bool openDoor = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        // Check if the jump button is pressed and the character is grounded
        if (jump && IsGrounded())
        {
            // Trigger the "IsJumping" parameter to start the jump animation
            animator.SetBool("IsJumping", true);

            // Set the "IsJumping" flag to true to indicate the character is jumping
            isJumping = true;

            // Apply a force to the character's rigidbody to make them jump
            GetComponent<Rigidbody2D>().AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

            // Set the "IsMovingRight" parameter to false to end the move right animation
            animator.SetBool("IsMovingRight", false);

            // Set the "IsMovingLeft" parameter to false to end the move left animation
            animator.SetBool("IsMovingLeft", false);

            // Set the "jump" flag to false to indicate the character is no longer jumping
            jump = false;
        }
        
        if (dash && !isDashing && dashCooldownLeft <= 0f)
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
            animator.SetBool("IsDashing", true);
        }

        if (dashCooldownLeft > 0f)
        {
            dashCooldownLeft -= Time.deltaTime;
        }
        
    }
    
    void FixedUpdate()
    {
        if (!isDashing)
        {
            if (moveLeft)
            {
                // Flip backward
                spriteRenderer.flipX = true;
                // Set the "IsMovingLeft" parameter to true to start the move left animation
                animator.SetBool("IsMovingLeft", true);
                // Move the character to the left
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            }
            else
            {
                // Set the "IsMovingLeft" parameter to false to end the move left animation
                animator.SetBool("IsMovingLeft", false);
            }
            if (moveRight)
            {
                // Flip backward
                spriteRenderer.flipX = false;
                // Set the "IsMovingRight" parameter to true to start the move right animation
                animator.SetBool("IsMovingRight", true);
                // Move the character to the right
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }
            else
            {
                // Set the "IsMovingRight" parameter to false to end the move right animation
                animator.SetBool("IsMovingRight", false);
            }
        }
        else
        {
            if (dashTimeLeft > 0f)
            {
                dashTimeLeft -= Time.deltaTime;
                if(moveLeft)
                {
                    float moveLeftSpeed = -1f;
                    float xVelocity = moveLeftSpeed * dashSpeed;
                    rb.velocity = new Vector2(xVelocity, rb.velocity.y);

                    // Set the "IsMovingLeft" parameter to false to end the move left animation
                    animator.SetBool("IsMovingLeft", false);
                }
                else if (moveRight)
                {
                    float moveRightSpeed = 1f;
                    float xVelocity = moveRightSpeed * dashSpeed;
                    rb.velocity = new Vector2(xVelocity, rb.velocity.y);

                    // Set the "IsMovingRight" parameter to false to end the move right animation
                    animator.SetBool("IsMovingRight", false);
                }
                else
                {
                    // Set the "IsDashing" parameter to false to end dashing
                    animator.SetBool("IsDashing", false);
                }

            }
            else
            {
                isDashing = false;
                dashCooldownLeft = dashCooldown;
                animator.SetBool("IsDashing", false);
            }
        }

        if (horizontalInput < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (horizontalInput > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    // Activate when user click the jump button
    public void Jump()
    {
        jump = true;
        TutorialManagement.isJumped = true;
        if (jump)
        {
            audioSource.clip = jumpSFX;
            audioSource.Play();
        }
        else
        {
            // Stop move sound effect loop
            audioSource.Stop();
        }
    }
    // Activate when user click the move left button
    public void MoveLeft(bool _left)
    {
        moveLeft = _left;
        TutorialManagement.isMoved = true;
        if (moveLeft)
        {
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
    public void Dashing(bool _dash)
    {
        dash = _dash;
        TutorialManagement.isDashed = true;
        if (dash)
        {
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
    public void Attacking()
    {
        if (Time.time >= nextAttackTime)
        {
            attack = true;
            TutorialManagement.isAttacked = true;
            if (attack)
            {
                // Play attack sound effect loop
                audioSource.clip = attackSFX;
                audioSource.Play();

                // Play an attack animation
                animator.SetBool("IsAttacking", true);

                // Detect enemy in range of attack
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                //Damage them
                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                }
                attack = false;
            }
            
        }
    }
    // Open door animation
    public void OpenDoor()
    {
        openDoor = true;
    }
    // Activate when user finish standing up
    public void IsIdle()
    {
        animator.SetBool("IsStandingUp", false);
    }
    // Activate when user finish attacking
    public void StopAttack()
    {
        attack = false;
        if (!attack)
        {
            // Stop move sound effect loop
            audioSource.Stop();

            // Stop an attack animation
            animator.SetBool("IsAttacking", false);
        }
    }
    // Check if user is on the ground or not
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }
    // Check if user touch the ground after jumping to activate standing up
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ground check
        if (collision.gameObject.CompareTag("Terrain"))
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
            if (rb.velocity.y == 0)
            {
                animator.SetBool("IsStandingUp", true);
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

