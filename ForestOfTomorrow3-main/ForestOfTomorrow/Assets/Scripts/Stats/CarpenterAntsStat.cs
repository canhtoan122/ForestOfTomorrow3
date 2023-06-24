using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarpenterAntsStat : AntStats
{
    #region Singleton
    public static CarpenterAntsStat instance;
    void Awake()
    {
        instance = this;

    }
    #endregion
    public BossHPBar healthBar;
    public TMP_Text healthText;
    public GameObject[] itemDrops;
    public GameObject[] moneyDrops;

    public void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public override void Die()
    {
        base.Die();
        // Disable the enemy
        this.GetComponent<Animator>().SetTrigger("isDead");
        this.GetComponent<Collider2D>().enabled = false;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        this.GetComponentInChildren<Canvas>().enabled = false;
        CarpenterAntsController.instance.EndBossFight();

        //  Add some money into player inventory
        StartCoroutine(moneyDrop());
        //  Add some item into player inventory
        StartCoroutine(itemDrop());
    }
    IEnumerator itemDrop()
    {
        int minItems = 1; // Minimum number of items to drop
        int maxItems = 5; // Maximum number of items to drop
        int numItems = Random.Range(minItems, maxItems + 1); // Randomly determine the number of items to drop

        for (int i = 0; i < numItems; i++)
        {
            int randomIndex = Random.Range(0, itemDrops.Length); // Randomly select an index from the itemDrops array
            Instantiate(itemDrops[randomIndex], transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
    }
    IEnumerator moneyDrop()
    {
        int minItems = 1; // Minimum number of items to drop
        int maxItems = 5; // Maximum number of items to drop
        int numItems = Random.Range(minItems, maxItems + 1); // Randomly determine the number of items to drop

        for (int i = 0; i < numItems; i++)
        {
            int randomIndex = Random.Range(0, moneyDrops.Length); // Randomly select an index from the moneyDrops array
            Instantiate(moneyDrops[randomIndex], transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
    }
    public void UpdateHealthBar()
    {
        healthBar.SetHealth(currentHealth);
        healthText.text = currentHealth.ToString();
    }
    public void DamagePlayer(Transform attackPoint, float attackRange, LayerMask playerLayer)
    {
        int damage = this.damage.GetStat();

        // Detect enemy in range of attack
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);


        //Damage them
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerStats>().TakeDamage(damage);
        }

        // Update health bar
        PlayerStats.instance.UpdateHealthBar();
        if (PlayerStats.instance.currentHealth <= 0)
        {
            PlayerStats.instance.Die();
        }
    }
}
