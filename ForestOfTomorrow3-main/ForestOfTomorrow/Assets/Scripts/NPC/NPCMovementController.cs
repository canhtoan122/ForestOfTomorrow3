using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCMovementController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public List<Transform> waypoint = new List<Transform>();
    private bool moveRight = true;
    private bool moveUp = false;
    private Animator animator;
    private Rigidbody2D rb;
    private string sceneName;
    public static bool canMove = false;

    public enum MovementState { idle, running}
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        transform.position = waypoint[0].position;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!canMove)
        {
            return;
        }
        else
        {
            Move();
        }
    }

    void Move()
    {
        if (moveUp)
        {
            //// Set move right animation
            //animator.SetBool("isRunning", true);
            // Move up
            transform.position = Vector2.MoveTowards(transform.position, waypoint[2].transform.position, Time.deltaTime * moveSpeed);
        }
        else
        {
            if (moveRight)
            {
                //// Set move right animation
                //animator.SetBool("isRunning", true);
                // Move right
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
            else
            {
                if (sceneName == "Scene 3")
                {
                    //animator.SetBool("isRunning", false);
                }
                else
                {
                    //animator.SetBool("isRunning", false);
                    // Disappear NPC
                    this.gameObject.SetActive(false);
                }
            }
        }



        //if(sceneName == "Scene 2")
        //{
        //    moveUp = false;
        //    moveRight = false;
        //    if (canMove)
        //    {
        //        moveRight = true;
        //    }
        //}
        //else
        //{
        //    if (transform.position.x >= waypoint[2].position.x && moveUp)
        //    {
        //        moveUp = false;
        //        moveRight = false;
        //    }
        //}
        
        if (transform.position.x >= waypoint[1].position.x && !moveUp)
        {
            moveUp = true;
            if (sceneName == "Scene 3")
            {
                if (canMove)
                {
                    moveUp = false;
                    moveRight = false;
                    string objectName = gameObject.name;

                    canMove = false;
                }
            }
        }
        else if (transform.position.x <= waypoint[0].position.x)
        {
            moveRight = true;
        }
        else if (transform.position.x >= waypoint[2].position.x && moveUp)
        {
            moveRight = false;
            moveUp = false;
        }

    }
    public void UpdateMovementAnimation()
    {
        MovementState state;
        if (canMove)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }
        animator.SetInteger("state", (int)state);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Stair check
        if (collision.gameObject.CompareTag("Stair"))
        {
            rb.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }
}
