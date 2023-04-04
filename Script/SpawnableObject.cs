using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    [SerializeField] GameObject toSpawn;
    [SerializeField] [Range(0f, 1f)] float probability;

    public void Spawn()
    {
        if (Random.value < probability)
        {
            if(toSpawn == null)
            {
                return;
            }
            //GameObject go = Instantiate(toSpawn, transform.position, Quaternion.identity);
            SpawnManager.instance.SpawnObject(transform.position, toSpawn);
            
        }
    }
}
