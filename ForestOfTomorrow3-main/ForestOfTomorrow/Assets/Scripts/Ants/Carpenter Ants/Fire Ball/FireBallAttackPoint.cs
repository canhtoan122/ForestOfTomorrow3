using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAttackPoint : MonoBehaviour
{
    public GameObject fireBall;  // Carpenter Ants Attack prefabs
    public float fireBallSpeed;    // The move speed of the fire ball
    public Transform playerTransform;  // The transform of the detected player
    public CarpenterAntsStat carpenterAnts; // Apply the boss stat into the projectile damage
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void FireFireBall()
    {
        animator.SetBool("FireBall", false);
        // Set the direction of the bullet to the right (default)
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // Spawn a new instance of the fireball prefab at the position of the player
        GameObject clone = Instantiate(fireBall, transform.position, Quaternion.identity);

        // Rotate the fireball towards the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
        clone.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        // Set the velocity of the bullet based on the direction and speed
        Rigidbody2D bulletRigidbody = clone.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = direction * fireBallSpeed;
    }
}
