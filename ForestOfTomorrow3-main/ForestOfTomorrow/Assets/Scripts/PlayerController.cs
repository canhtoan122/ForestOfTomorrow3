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
    public LayerMask groundLayer;   // The layer(s) that represent the ground
    private SpriteRenderer spriteRenderer; //The character image
    private PolygonCollider2D coll;
    private Rigidbody2D rb;
    private Vector2 dashTarget;

    private bool isGrounded = false;   // Flag to indicate if the character is grounded
    private bool isMovingLeft = false;   // Flag to indicate if the character is moving left
    private bool isMovingRight = false;   // Flag to indicate if the character is moving right
    private bool isJumping = false;   // Flag to indicate if the character is jumping
    private bool isStandingUp = false;   // Flag to indicate if the character is standing up
    private bool isDashing = false;   // Flag to indicate if the character is dashing
    private bool facingRight = true;

    private bool jump = false;
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool dash = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<PolygonCollider2D>();
        rb = GetComponent<Rigidbody2D>();   
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

            // Set the "jump" flag to false to indicate the character is no longer jumping
            jump = false;
        }
        // Handle dashing input
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
            // Calculate target position based on dash distance
            dashTarget = rb.position + new Vector2(dashDistance * (facingRight ? 1 : -1), 0);

            // Calculate velocity to reach target position in dash duration
            Vector2 dashVelocity = (dashTarget - rb.position) / dashDuration;

            // Set player velocity to dash velocity
            rb.velocity = dashVelocity;

            // Trigger the "IsJumping" parameter to start the jump animation
            animator.SetBool("IsDashing", true);
        }
        else if (isDashing)
        {
            // Calculate remaining distance to dash target
            float remainingDistance = Mathf.Abs(rb.position.x - dashTarget.x);

            if (remainingDistance < 0.1f)
            {
                isDashing = false;
            }
            if (dashTimeLeft > 0)
            {
                dashTimeLeft -= Time.deltaTime;
            }
            else
            {
                isDashing = false;

                // Trigger the "IsJumping" parameter to stop the jump animation
                animator.SetBool("IsDashing", false);
            }
        }
        // Check if the left button is pressed
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
            // Flip backward
            spriteRenderer.flipX = false;
            // Set the "IsMovingLeft" parameter to false to end the move left animation
            animator.SetBool("IsMovingLeft", false);
        }

        // Check if the right button is pressed
        if (moveRight)
        {
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
    public void Jump()
    {
        jump = true;
    }
    public void MoveLeft(bool _left)
    {
        moveLeft = _left;
    }
    public void MoveRight(bool _right)
    {
        moveRight = _right;
    }
    public void Dashing()
    {
        dash = true;
    }
    public void IsIdle()
    {
        animator.SetBool("IsStandingUp", false);
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, groundLayer);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
            if (rb.velocity.y == 0)
            {
                animator.SetBool("IsStandingUp", true);
            }
        }
    }
}

