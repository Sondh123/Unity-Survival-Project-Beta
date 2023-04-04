using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    

    Vector3 positionToSpawnRepeated;
    private void Awake()
    {
        instance = this;
    }
    public void SpawnObject(Vector3 worldPosition, GameObject toSpawn)
    {  
        if(toSpawn == null)
        {
            return;
        }
        Transform t = Instantiate(toSpawn, transform).transform;
        t.transform.parent = gameObject.transform;
        t.position = worldPosition;

    }
}
