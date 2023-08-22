using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarpenterAntsController : MonoBehaviour
{
    #region Singleton
    public static CarpenterAntsController instance;
    void Awake()
    {
        instance = this;

    }
    #endregion
    public float moveSpeed;    // The move speed of the boss
    public float detectionRadius;  // The radius of the detection range
    public float attackRange; // The attack range of the attack point
    public GameObject fireBallAttackPoint;  // Carpenter Ants Attack point
    public GameObject bossHP;   // When the boss detect player, the boss HP appear
    public GameObject menuButton;    // And disable the menu panel
    public TMP_Text bossHPText; // And display boss HP
    public Image icon;  // And display boss icon
    public Sprite bossIcon;  // Change the original icon into the current boss icon
    public List<Transform> patrolPoints = new List<Transform>();    // The patrol point when enemy patrol
    public float rotationSpeed;     // The rotation of the fire ball
    public LayerMask playerLayer;  // The layer that the player is on
    public Transform playerTransform;  // The transform of the detected player

    private Animator animator;  //  The game object animator
    private bool playerDetected = false;  // Whether the player is detected or not
    private bool nearPlayer = false; // Whether the player is in the attack range or not
    private CarpenterAntsStat carpenterAntsStat;    //  The Carpenter Ants stats
    private int currentPatrolIndex = 0; // The current patrol point

    public enum MovementState { move, attack }

    private void Start()
    {
        animator = GetComponent<Animator>();
        carpenterAntsStat = GetComponent<CarpenterAntsStat>();
    }
    private void Update()
    {
        //  Patrol
        if (!playerDetected)
        {
            Patrol();
        }
        // Move toward player
        DetectPlayer();
        // Detect player is in attack range or not
        RunningToPlayer();
        // Attack player
        AttackingPlayer();
        // Detect is get hit or not
        // Limited the fly away height
        // If the boss is get hit, fly away
        // if the boss is below 1/3 health, activate stage 2

        UpdateMovementAnimation();
    }
    public void Patrol()
    {
        if (!playerDetected)
        {
            if (patrolPoints.Count > 0)
            {
                Transform currentPatrolPoint = patrolPoints[currentPatrolIndex];
                Vector3 direction = (currentPatrolPoint.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;

                if (Vector3.Distance(transform.position, currentPatrolPoint.position) < 0.1f)
                {
                    currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
                }
            }
        }
    }
    public void DetectPlayer()
    {
        // Check if the player is within the detection range
        Collider2D[] detectedPlayers = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
        if (detectedPlayers.Length > 0)
        {
            playerDetected = true;
            // The player has been detected, so do something here (e.g. attack, follow)
            playerTransform = detectedPlayers[0].transform;  // Assume there is only one detected player

            StartBossFight();
        }
        else
        {
            playerDetected = false;
        }
    }
    public void RunningToPlayer()
    {
        //Move towards the player if they are detected
        if (playerDetected)
        {
            if (!nearPlayer)
            {
                //Move toward force
                Vector2 targetPosition = new Vector2(playerTransform.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
            //Set Destination
            float distance = Vector2.Distance(transform.position, playerTransform.position);
            if (distance <= attackRange)
            {
                nearPlayer = true;
            }
            else
            {
                nearPlayer = false;
            }
        }
    }
    public void AttackingPlayer()
    {
        //Move towards the player if they are detected
        if (nearPlayer)
        {
            fireBallAttackPoint.GetComponent<Animator>().SetBool("FireBall", true);
        }
    }
    public void StartBossFight()
    {
        bossHP.SetActive(true);
        menuButton.SetActive(false);
        bossHPText.text = carpenterAntsStat.maxHealth.ToString();
        icon.sprite = bossIcon;
    }
    public void EndBossFight()
    {
        bossHP.SetActive(false);
        menuButton.SetActive(true);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        if (fireBallAttackPoint == null)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Collider2D otherGameObject = collision.gameObject.GetComponent<Collider2D>();
            Collider2D thisGameObject = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(thisGameObject, otherGameObject);
        }
    }
    public void UpdateMovementAnimation()
    {
        MovementState state;
        if (nearPlayer)
        {
            state = MovementState.attack;
        }
        else
        {
            state = MovementState.move;
        }
        animator.SetInteger("state", (int)state);
    }
}
