using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth { get; set; }

    public Stat armor;
    public Stat damage;
    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Die in some way
        // This method is meant to be overwritten
        Debug.Log(transform.name + "died.");
    }
}
