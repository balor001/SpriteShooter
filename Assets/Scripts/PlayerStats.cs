using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public PlayerInputController playerInputController;

    public static float health = 100f;
    public float maxHealth = 100f;
    public static float stamina = 100f;
    public float maxStamina = 100f;

    /*
    private static float staminaRegenTimer = 0f;
    private const float staminaDecreaseRate = 15f;
    private const float staminaIncreaseRate = 5f;
    private const float staminaTimeToRegen = 3f;
    */

    public Slider healthSlider;
    public Slider staminaSlider;

    void Start()
    {
        health = maxHealth;
        stamina = maxStamina;
        healthSlider.value = CalculateHealth();
        staminaSlider.value = CalculateStamina();
    }

    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        healthSlider.value = CalculateHealth();
        staminaSlider.value = CalculateStamina();

        /* 
        // Reduce stamina when player sprints
        if (PlayerMovement.isSprinting)
        {
            stamina = Mathf.Clamp(stamina - (staminaDecreaseRate * Time.deltaTime), 0f, maxStamina);
            staminaRegenTimer = 0f;
        }
        else if (stamina < maxStamina)
        {
            // Regen stamina after not using stamina for a while
            if (staminaRegenTimer >= staminaTimeToRegen)
            {
                stamina = Mathf.Clamp(stamina + (staminaIncreaseRate * Time.deltaTime), 0f, maxStamina);
            }
            else
            {
                staminaRegenTimer += Time.deltaTime;
            }
        }
        */

        // Testing player health
        if (playerInputController.inputActions.Player.DamagePlayer.triggered)
        {
            health -= 10;
        }
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }

    float CalculateStamina()
    {
        return stamina / maxStamina;
    }

    /*
    public static void JumpStaminaReduction()
    {
        stamina -= 5;
        staminaRegenTimer = 0f;
    }
    */

    public static void AddHealth(float amount)
    {
        health += amount;
    }
}
