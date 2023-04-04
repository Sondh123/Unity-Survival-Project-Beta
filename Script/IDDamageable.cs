using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDDamageable
{
    public void TakeDamage(int damage);
    public void TakeEffect(float effect);
}
