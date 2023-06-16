using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallCollider : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(Explode());
        }
        if (collision.gameObject.tag == "Terrain")
        {
            StartCoroutine(Explode());
        }
    }
    // When the fire ball hit the ground, it will explode and delete from the scene
    IEnumerator Explode()
    {
        animator.SetBool("isExplode", true);
        rb.bodyType = RigidbodyType2D.Static;

        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isExplode", false);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(this.gameObject);
    }

}
