using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask jumpableGround;

    private bool moveLeft = false;
    private bool moveRight = false;
    private bool jump = false;

    public GameObject door;
    public GameObject openDoor;

    private Rigidbody2D rb;
    private PolygonCollider2D coll;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private enum MovementState { idle, walk, jump, falling }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        UpdateAnimationState();
    }
    private void UpdateAnimationState()
    {
        MovementState state;
        if (moveLeft)
        {
            transform.position += Vector3.right * -playerSpeed * Time.deltaTime;
            spriteRenderer.flipX = true;
            state = MovementState.walk;
        }
        else if (moveRight)
        {
            transform.position += Vector3.right * playerSpeed * Time.deltaTime;
            spriteRenderer.flipX = false;
            state = MovementState.walk;
        }
        else if (jump)
        {
            state = MovementState.jump;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        else
        {
            state = MovementState.idle;
        }

        anim.SetInteger("state", (int)state);
        //if (moveLeft)
        //{
        //    transform.position += Vector3.right * -playerSpeed * Time.deltaTime;
        //    spriteRenderer.flipX = true;
        //    anim.SetBool("Walk", true);
        //}
        //else if (moveRight)
        //{
        //    transform.position += Vector3.right * playerSpeed * Time.deltaTime;
        //    spriteRenderer.flipX = false;
        //    anim.SetBool("Walk", true);
        //}
        //else
        //{
        //    anim.SetBool("Walk", false);
        //}
        //if (rb.velocity.y < -.1f)
        //{
        //    anim.SetBool("Falling", true);
        //}
        //else
        //{
        //    anim.SetBool("Falling", false);
        //}
    }
    public void MoveLeft(bool _moveLeft)
    {
        moveLeft = _moveLeft;
    }

    public void MoveRight(bool _moveRight)
    {
        moveRight = _moveRight;
        //rb.velocity = new Vector2(playerSpeed * dirX, 0);
    }
    public void Jump(bool _jump)
    {
        //if (IsGrounded())
        //{
            jump = _jump;
        //}
    }
    public void FallingState()
    {
        jump = false;
    }
    public void JumpForce()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OpenDoor()
    {
        door.SetActive(false);
        openDoor.SetActive(true);
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
