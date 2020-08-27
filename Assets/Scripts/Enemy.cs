using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public GameObject player;

    public float health = 100f;
    public float maxHealth = 100f;

    public GameObject healthBar;
    public Slider healthSlider;

    private void Start()
    {
        health = maxHealth;
        healthSlider.value = CalculateHealth();
    }

    private void Update()
    {
        healthSlider.value = CalculateHealth();
        gameObject.transform.LookAt(player.transform);

        if (health < maxHealth)
        {
            healthBar.SetActive(true);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        animator.SetTrigger("Hit");

        if (health <= 0f)
        {
            Die();
        }
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        
        Destroy(gameObject, 10f);
    }
}
