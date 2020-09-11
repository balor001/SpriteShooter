using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public PlayerInputController playerInputController;

    public static float health = 100f;
    public float maxHealth = 100f;
    public static float shield = 100f;
    public float maxShield = 100f;

    public static float overDamage; // Damage that exceeds current shield amount

    public Slider healthSlider;
    public Slider shieldSlider;
    public Text healthAmount;
    public Text shieldAmount;

    void Start()
    {
        shield = maxShield;
        shieldSlider.value = CalculateShield();
        shieldAmount.text = shield.ToString();

        health = maxHealth;
        healthSlider.value = CalculateHealth();
        healthAmount.text = health.ToString();
    }

    void Update()
    {
        if (shield > maxShield)
        {
            shield = maxShield;
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (health <= 0)
        {
            // Game over
            health = 0;
        }

        shieldSlider.value = CalculateShield();
        healthSlider.value = CalculateHealth();

        shieldAmount.text = shield.ToString();
        healthAmount.text = health.ToString();

        // Testing player health (Bind: Q)
        if (playerInputController.inputActions.Player.DamagePlayer.triggered)
        {
            TakeDamage(15);
        }
    }

    float CalculateShield()
    {
        return shield / maxShield;
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }

    public static void AddHealth(float amount)
    {
        health += amount;
    }

    public static void AddShield(float amount)
    {
        shield += amount;
    }

    // Damage target order: Shield -> Shield & Health -> Health
    public void TakeDamage(float amount)
    {
        if (shield >= amount)
        {
            shield -= amount;
        }
        // If player has shields and damage is more than current shields, reduce over going damage from health
        else if (shield > 0 && shield < amount)
        {
            overDamage = amount - shield;
            shield = 0;
            health -= overDamage;
        }
        else
        {
            health -= amount;
        }
    }
}
