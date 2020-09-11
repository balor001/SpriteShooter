using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            enemiesToSpawn[i].SetActive(true);
        }
        
        Destroy(gameObject);
    }
}
