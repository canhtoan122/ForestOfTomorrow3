using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; set; }

    public Stat damage;
    public Stat armor;

    public void Heal()
    {
        if(currentHealth >= maxHealth)
        {
            Debug.Log("Stop Healing");
        }
        else
        {
            currentHealth += 50;
            if(currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        if (PlayerController.isVulnerable)
        {
            return;
        }
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
    }
    public virtual void Die()
    {
        // Die in some way
        // This method is meant to be overwritten
        Debug.Log(transform.name + "died.");
    }
}
