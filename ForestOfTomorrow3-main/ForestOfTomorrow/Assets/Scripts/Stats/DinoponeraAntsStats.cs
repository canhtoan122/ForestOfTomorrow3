using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoponeraAntsStats : AntStats
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
        this.GetComponent<Animator>().SetBool("isDead", true);
        this.GetComponent<Collider2D>().enabled = false;
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        //  Add some money into player inventory
        StartCoroutine(itemDrop());
    }
    IEnumerator itemDrop()
    {
        for (int i = 0; i < itemDrops.Length; i++)
        {
            Instantiate(itemDrops[i], transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
    }
    public void UpdateHealthBar()
    {
        healthBar.SetHealth(currentHealth);
    }
}
