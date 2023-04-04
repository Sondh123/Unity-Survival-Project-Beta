using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDefeat : MonoBehaviour
{
    [SerializeField] List<GameObject> dropItemPrefab;
    [SerializeField] [Range(0f, 1f)] float chance = 1f;

    bool isQuitting = false;

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    private void OnDisable()
    {
        if (isQuitting)
        {
            return;
        }
        if (dropItemPrefab.Count <= 0)
        {
            Debug.LogWarning("List of drop items is empty");
            return;
        }
        if (Random.value < chance)
        {
            GameObject toDrop = dropItemPrefab[Random.Range(0, dropItemPrefab.Count)];
            Debug.Log("ok");
            if (toDrop == null)
            {
                Debug.LogWarning("DropOnDestroy, reference to dropped item is null!");
                return;
            }
            SpawnManager.instance.SpawnObject(transform.position, toDrop);
        }
        if(gameObject == null)
        {
            return;
        }
    }
}
