using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpittingAntsStats : AntStats
{
    public HealthBar healthBar;

    public void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public override void Die()
    {
        base.Die();
        this.gameObject.SetActive(false);
    }
    public void UpdateHealthBar()
    {
        healthBar.SetHealth(currentHealth);
    }
}
