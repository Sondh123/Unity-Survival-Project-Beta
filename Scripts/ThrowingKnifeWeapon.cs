using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnifeWeapon : WeaponBase
{
    PlayerMove playerMove;

    [SerializeField] GameObject daggerPrefabs;
    [SerializeField] float spread = 0.5f;
    

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }


    public override void Attack()
    {
        
        for(int i = 0; i < weaponStats.numberOfAttack; i++)
        {
            GameObject throwingDagger = Instantiate(daggerPrefabs);

            Vector3 newKnifePosition = transform.position;
            newKnifePosition.y -= (spread * weaponStats.numberOfAttack) / 2; // calculating offset
            newKnifePosition.y += i * spread; //multi vu khi theo line

            throwingDagger.transform.position = newKnifePosition;

            ThrowingKnifeProjectile throwingDaggerProjectile = throwingDagger.GetComponent<ThrowingKnifeProjectile>();
            throwingDagger.GetComponent<ThrowingKnifeProjectile>().SetDirection(playerMove.lastHorizontalVector, 0f);
            throwingDaggerProjectile.damage = weaponStats.damage;
        }

    }
}
