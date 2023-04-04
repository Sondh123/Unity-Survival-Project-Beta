using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour, IDDamageable
{
    public void TakeDamage(int damage)
    {
        Destroy(gameObject);
        GetComponent<DropOnDestroy>().CheckDrop();
        FindObjectOfType<AudioManager>().Play("Small Chest Break");
    }
    public void TakeEffect(float effect) { }
}
