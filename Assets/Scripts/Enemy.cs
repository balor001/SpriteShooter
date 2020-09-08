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

    public float minAttackDistance = 20f;
    public float damage = 10f;
    public float fireRate = 1f;
    private float nextTimeToFire = 0f;

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

        // Turn to look at player
        gameObject.transform.LookAt(player.transform);

        // Get distance to player
        float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);

        // Shoot player when close enough
        if (distance < minAttackDistance && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            ShootPlayer();
        }

        // Only show health bar when not at full health
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

    void ShootPlayer()
    {
        RaycastHit hit;

        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, minAttackDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                hit.transform.GetComponent<PlayerStats>().TakeDamage(damage);
            }
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
