using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float lifeSpan = 2f;
    public float projectileSpeed = 10f;

    Transform player;
    Rigidbody rb;
    Vector3 direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        direction = player.position - transform.position;
    }

    void Update()
    {
        rb.velocity = direction.normalized * projectileSpeed;
        Destroy(gameObject, lifeSpan);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerStats>().TakeDamage(Enemy.damage);
        }

        Destroy(gameObject);
    }
}
