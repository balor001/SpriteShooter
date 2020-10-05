using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 1f;
    public float impactForce = 30f;
    public int maxAmmo = 50;
    public static int ammo = 25;

    public Animator gunAnimator;
    public Camera fpsCam;
    public PlayerInputController playerInputController;
    public ParticleSystem muzzleFlash;
    public GameObject hitEffect;
    public Text ammoText;

    private float nextTimeToFire = 0f;

    void Update()
    {
        if (ammo > maxAmmo)
        {
            ammo = maxAmmo;
        }

        ammoText.text = string.Format("AMMO: {0:00}/{1:00}", ammo, maxAmmo);

        if (playerInputController.inputActions.Player.Fire.triggered && Time.time >= nextTimeToFire && ammo >= 1)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            ammo -= 1;
        }
    }

    void Shoot()
    {
        // Gun animation
        gunAnimator.SetTrigger("Shooting");
        
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.transform.CompareTag("Destructable"))
            {
                hit.transform.GetComponent<HitHandler>().TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject hitEffectGameObject = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(hitEffectGameObject, 2f);
        }
    }

    public static void AddAmmo(int amount)
    {
        ammo += amount;
    }
}
