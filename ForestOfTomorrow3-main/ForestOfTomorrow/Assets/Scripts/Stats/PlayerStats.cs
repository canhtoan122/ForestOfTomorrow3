using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : CharacterStat
{
    [SerializeField]
    private TMP_Text HPText;
    [SerializeField]
    private TMP_Text AttackText;
    [SerializeField]
    private TMP_Text DefenceText;

    public HealthBar healthBar;
    public RageBar rageBar;
    public GameObject healPotion;


    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        HPText.text = currentHealth.ToString();
        AttackText.text = damage.GetValue().ToString();
        DefenceText.text = armor.GetValue().ToString();

        healthBar.SetMaxHealth(maxHealth);
        
    }
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
            string checkItem = newItem.equipSlot.ToString();
            if (checkItem == "HealPotion")
            {
                Debug.Log("There are no stat");
            }
            else
            {
                if (checkItem == "Armor")
                {
                    DefenceText.text = armor.GetValue().ToString();
                }
                else if (checkItem == "Weapon")
                {
                    AttackText.text = damage.GetValue().ToString();
                }
            }
        }
        if(oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
            string checkItem = oldItem.equipSlot.ToString();
            if (checkItem == "HealPotion")
            {
                Debug.Log("There are no stat");
            }
            else
            {
                if (checkItem == "Armor")
                {
                    DefenceText.text = armor.RemoveValue().ToString();
                }
                else if (checkItem == "Weapon")
                {
                    AttackText.text = damage.RemoveValue().ToString();
                }
            }
        }
    }
    public void UsePotion()
    {
        if (healPotion.activeSelf == true)
        {
            Heal();
            healPotion.SetActive(false);
            UpdateHealthBar();
            EquipmentManager.instance.DeleteHealPotion();
        }
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    TakeDamage(10);
        //    UpdateRageBar();
        //}
    }
    public void UpdateHealthBar()
    {
        healthBar.SetHealth(currentHealth);
    }
    public void UpdateRageBar()
    {
        rageBar.SetHealth(currentHealth);
    }
}
