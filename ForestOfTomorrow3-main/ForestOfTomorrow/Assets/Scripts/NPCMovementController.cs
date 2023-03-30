using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementController : MonoBehaviour
{
    
    public float moveSpeed = 5f;
    public Transform waypoint1;
    public Transform waypoint2;
    public Transform waypoint3;
    private bool moveRight = true;
    private bool moveUp = false;
    private Animator animator;
    private Rigidbody rb;
    void Start()
    {
        transform.position = waypoint1.position;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (moveUp)
        {
            // Set move right animation
            animator.SetBool("isRunning", true);
            // Move up
            transform.position = Vector2.MoveTowards(transform.position, waypoint3.transform.position, Time.deltaTime * moveSpeed);
        }
        else
        {
            if (moveRight)
            {
                // Set move right animation
                animator.SetBool("isRunning", true);
                // Move right
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isRunning", false);
                // Disappear NPC
                this.gameObject.SetActive(false);
            }
        }

        

        if (transform.position.x >= waypoint2.position.x && !moveUp)
        {
            moveUp = true;
        }
        else if (transform.position.x <= waypoint1.position.x)
        {
            moveRight = true;
        }
        if(transform.position.x >= waypoint3.position.x && moveUp)
        {
            moveUp = false;
            moveRight = false;
        }
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
