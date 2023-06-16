using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public int flashCount = 3;  // The number of time enemy get flash when get hit by player
    public float flashDuration = 0.5f;  // When the enemy get hit bu player, the enemy will flash
    public float flashAlpha = 0f;   // Flash color
    public Sprite playerSpinSprite;
    public float spinSpeed;
    public float targetRotation = 360f;
    public LayerMask groundLayer;   // The layer(s) that represent the ground
    private SpriteRenderer spriteRenderer; //The character image
    private BoxCollider2D coll;     // Player collider
    private Rigidbody2D rb;     // Player Rigidbody
    public float attackRange = 0.5f; // The attack range of the attack point
    public LayerMask enemyLayers;   // Detect enemy layer
    private int attackDamage;       // Player Damage
    private PlayerStats playerStats;        // Player Stat

    public float attackRate = 2f;

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
    private bool isSpinning = false;
    private float currentRotation = 0f;

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
    private void Update()
    {
        Moving();
        if(isSpinning)
        {
            currentRotation = 0f; // Reset currentRotation to 0
            StartCoroutine(SpinAttack());
        }
    }
    // Main moving left right component
    public void Moving()
    {
        if (isMovingLeft)
        {
            spriteRenderer.flipX = true;
            _moveDirection = -1;
            // Move the character to the left
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            animator.SetBool("isMoving", isJumping ? false : true);
            animator.SetBool("isStandingUp", false);
        }
        else if (isMovingRight)
        {
            spriteRenderer.flipX = false;
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
        StartCoroutine(Jump());
    }
    IEnumerator Jump()
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
        yield return null;
    }
    // Main attacking component
    public void Attacking()
    {
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        if(isJumping)
        {
            isSpinning = true;
            yield break;
        }
        if (animator.GetBool("isAttacking") || isJumping || isDashing)
        {
            yield break;
        }
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
            if (enemy.GetComponent<SpriteRenderer>() != null)
            {
                for (int i = 0; i < flashCount; i++)
                {
                    SpriteRenderer spriteRenderer = enemy.GetComponent<SpriteRenderer>();
                    Color originalColor = spriteRenderer.color;
                    yield return StartCoroutine(FlashAlpha(originalColor, spriteRenderer));
                }
            }

            if (enemy.GetComponent<Enemy>() == null)
            {
                if (enemy.GetComponent<DinoponeraAntsStats>() == null)
                {
                    enemy.GetComponent<SpittingAntsStats>().TakeDamage(attackDamage);
                    enemy.GetComponent<SpittingAntsStats>().UpdateHealthBar();
                    animator.SetBool("isAttacking", false);
                    yield break;
                }
                enemy.GetComponent<DinoponeraAntsStats>().TakeDamage(attackDamage);
                enemy.GetComponent<DinoponeraAntsStats>().UpdateHealthBar();
                animator.SetBool("isAttacking", false);
                yield break;
            }
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

        yield return new WaitForSeconds(0.5f);

        animator.SetBool("isAttacking", false);
    }
    IEnumerator SpinAttack()
    {
        animator.enabled = false;
        spriteRenderer.sprite = playerSpinSprite;
        rb.bodyType = RigidbodyType2D.Static;

        // Spin Player
        float startRotation = transform.rotation.eulerAngles.z;
        float endRotation = startRotation + targetRotation;

        while (currentRotation < endRotation)
        {
            currentRotation += spinSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
            yield return null;
        }

        // Reset the Z rotation to 0
        transform.rotation = Quaternion.Euler(0f, 0f, endRotation);

        yield return new WaitForSeconds(0.5f);
        animator.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        isSpinning = false;
    }
    IEnumerator FlashAlpha(Color originalColor, SpriteRenderer spriteRenderer)
    {
        float startTime = Time.time;
        Color flashColor = new Color(originalColor.r, originalColor.g, originalColor.b, flashAlpha);

        while (Time.time - startTime < flashDuration)
        {
            spriteRenderer.color = flashColor;
            yield return null;
        }

        spriteRenderer.color = originalColor;

        yield return null;
    }
    // Main dashing component
    public void Dashing()
    {
        StartCoroutine(Dash());
    }
    IEnumerator Dash()
    {
        animator.SetBool("isDashing", true);
        rb.velocity = new Vector2(dashSpeed * _moveDirection, rb.velocity.y);
        audioSource.clip = dashSFX;
        audioSource.loop = false;
        audioSource.Play();

        yield return new WaitForSeconds(0.6f);

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
    public void ResetScene3()
    {
        DialogueManagement.dialogEnd = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        TapToContinue.playerDie = true;
    }
    public void ResetScene()
    {
        DialogueManagement.dialogEnd = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnStair()
    {
        rb.GetComponent<Rigidbody2D>().gravityScale = 0f;
    }
    public void OffStair()
    {
        rb.GetComponent<Rigidbody2D>().gravityScale = 3f;
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
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
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

