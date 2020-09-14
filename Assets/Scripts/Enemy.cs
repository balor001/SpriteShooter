using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    private Transform player;
    NavMeshAgent agent;
    public GameObject projectile;
    private Transform projectileSpawn;
    public GameObject healthBar;
    public Slider healthSlider;

    private float health = 100f;
    [Header("Health Settings")]
    public float maxHealth = 100f;

    public enum AttackType 
    { 
        hitscan, 
        projectile, 
        melee
    }

    [Header("Attack Settings")]
    public AttackType attackType = AttackType.hitscan;
    public float minAttackDistance = 20f;
    private float meleeDistance = 5f;
    public static float damage = 10f;
    public float fireRate = 1f;
    private float nextTimeToFire = 0f;

    private bool isAlive;

    private void Start()
    {
        isAlive = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        projectileSpawn = transform.GetChild(0);
        agent = GetComponent<NavMeshAgent>();
        health = maxHealth;
        healthSlider.value = CalculateHealth();
    }

    private void Update()
    {
        healthSlider.value = CalculateHealth();

        // Turn to look at player
        gameObject.transform.LookAt(player);

        // Get distance to player
        float distance = Vector3.Distance(gameObject.transform.position, player.position);

        // Move towards player
        agent.SetDestination(player.position);

        // Stop moving when within attack range (melee range or shooting range)
        if (attackType == AttackType.melee)
        {
            if (distance <= meleeDistance)
            {
                agent.isStopped = true;
            }
            else if (distance > meleeDistance)
            {
                agent.isStopped = false;
            }
        }
        else
        {
            if (distance <= minAttackDistance)
            {
                agent.isStopped = true;
            }
            else if (distance > minAttackDistance)
            {
                agent.isStopped = false;
            }
        }

        // Attack player when within range
        if (distance <= minAttackDistance && Time.time >= nextTimeToFire && isAlive)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            AttackPlayer();
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
            //Die();
            Destroy(gameObject);
        }
    }

    void AttackPlayer()
    {
        RaycastHit hit;

        if (attackType == AttackType.hitscan)
        {
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, minAttackDistance))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    // Animation + sound
                    hit.transform.GetComponent<PlayerStats>().TakeDamage(damage);
                }
            }
        }
        else if (attackType == AttackType.projectile)
        {
            // Animation + sound
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, minAttackDistance))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    // Animation + sound
                    Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
                }
            }
        }
        else if (attackType == AttackType.melee)
        {
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, meleeDistance))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    // Animation + sound
                    hit.transform.GetComponent<PlayerStats>().TakeDamage(damage);
                }
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
