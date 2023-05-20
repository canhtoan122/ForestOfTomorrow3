using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Animator animator;   // Reference to the Animator component
    private float jumpForce = 15f;   // The force applied to the character when jumping
    private float moveSpeed = 5f;   // The speed at which the character moves left and right
    private int _moveDirection = 1;
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
    private bool isDashing = false;   // Flag to indicate if the character is dashing
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    public enum MovementState { idle, walking, jumping, standing, dash, attack}

    private void OnEnable()
    {
        ControllerUI.Instance.OnAttackTriggered += Attacking;
        ControllerUI.Instance.OnJumpTriggered += Jumping;
        ControllerUI.Instance.OnDashTriggered += Dashing;
        ControllerUI.Instance.OnMoveLeftTriggered += MoveLeft;
        ControllerUI.Instance.OnMoveRightTriggered += MoveRight;
    }

    private void OnDisable()
    {
        ControllerUI.Instance.OnAttackTriggered -= Attacking;
        ControllerUI.Instance.OnJumpTriggered -= Jumping;
        ControllerUI.Instance.OnDashTriggered -= Dashing;
        ControllerUI.Instance.OnMoveLeftTriggered -= MoveLeft;
        ControllerUI.Instance.OnMoveRightTriggered -= MoveRight;
    }

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
        Moving();
    }
    // Main moving left right component
    public void Moving()
    {
        if (isMovingLeft)
        {
            _moveDirection = -1;
            // Move the character to the left
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            animator.SetBool("isMoving", isJumping ? false : true);
            animator.SetBool("isStandingUp", false);
        }
        else if (isMovingRight)
        {
            _moveDirection = 1;
            // Move the character to the right
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            animator.SetBool("isMoving", isJumping ? false : true);
            animator.SetBool("isStandingUp", false);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
    // Main jumping component
    public void Jumping()
    {
        // Check if the jump button is pressed and the character is grounded
        if (!isJumping && IsGrounded())
        {
            animator.SetBool("isJumping", true);
            // Set the "IsJumping" flag to true to indicate the character is jumping
            isJumping = true;

            // Apply a force to the character's rigidbody to make them jump
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

            // Apply audio SFX into jumping
            audioSource.clip = jumpSFX;
            audioSource.Play();
        }
    }
    // Main attacking component
    public void Attacking()
    {
        animator.SetBool("isAttacking", true);
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
    }

    public void EndAttack()
    {
        animator.SetBool("isAttacking", false);
    }
    // Main dashing component
    public void Dashing()
    {
        animator.SetBool("isDashing", true);
        rb.velocity = new Vector2(dashSpeed * _moveDirection, rb.velocity.y);
        audioSource.clip = dashSFX;
        audioSource.loop = false;
        audioSource.Play();
    }
    public void EndDashing()
    {
        animator.SetBool("isDashing", false);
    }
    // Activate when user click the move left button
    public void MoveLeft(bool isMoveLeft)
    {
        isMovingLeft = isMoveLeft;
        if (isMovingLeft)
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
    public void MoveRight(bool isMoveRight)
    {
        isMovingRight = isMoveRight;
        if (isMovingRight)
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
    public void ResetScene()
    {
        DialogueManagement.dialogEnd = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        EquipmentManager.instance.PlayerDeadInScene3();
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
            animator.SetBool("isJumping", false);
            isJumping = false;
            if (rb.velocity.y == 0)
            {
                animator.SetBool("isStandingUp", true);
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
            animator.SetBool("isJumping", false);
            if (rb.velocity.y == 0)
            {
                animator.SetBool("isStandingUp", true);
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

