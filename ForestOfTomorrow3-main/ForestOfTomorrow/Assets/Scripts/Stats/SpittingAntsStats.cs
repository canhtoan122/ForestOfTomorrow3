using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpittingAntsStats : AntStats
{
    public HealthBar healthBar;
    public GameObject[] itemDrops;

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

        //  Add some money into player inventory
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
    public void UpdateHealthBar()
    {
        healthBar.SetHealth(currentHealth);
    }
}
