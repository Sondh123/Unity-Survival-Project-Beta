using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JadartWeapon : WeaponBase
{
    [SerializeField] GameObject kunaiPrefabs;
    [SerializeField] float spread = 15f;
    public float speed = 100f;
    public int damage;
    public float ttl = 2f;
    float tempRot;

    public override void Attack()
    {
        damage = weaponStats.damage;
        speed = weaponStats.speed;
       
        float facingRotation = Mathf.Atan2(vectorOfAttack.y, vectorOfAttack.x) * Mathf.Rad2Deg;       
        float startRotation = facingRotation + spread / 2f;       
        float angleIncrease = spread / ((float)weaponStats.numberOfAttack + 1f);
        
        UpdateVectorOfAttack();
        for (int i = 0 ; i < weaponStats.numberOfAttack; i++)
        {
            tempRot = startRotation + angleIncrease * 60 * i;           
            GameObject throwingDagger = Instantiate(kunaiPrefabs, transform.position, Quaternion.Euler(0f, 0f, tempRot));
            FindObjectOfType<AudioManager>().Play("Jadart");
            Vector3 newKnifePosition = transform.position;

            if (weaponStats.numberOfAttack > 1)
            {
                newKnifePosition.y -= (spread * weaponStats.numberOfAttack) / 2; // calculating offset
                newKnifePosition.y += i * spread; //multi vu khi theo line               
            }
            throwingDagger.transform.position = newKnifePosition;

            ThrowingJadartProjectile throwingDaggerProjectile = throwingDagger.GetComponent<ThrowingJadartProjectile>();
            //throwingDagger.GetComponent<ThrowingJadartProjectile>().SetDirection(vectorOfAttack.x, vectorOfAttack.y * (i + 1));      
            throwingDagger.GetComponent<ThrowingJadartProjectile>().SetDirection(Mathf.Cos(tempRot * Mathf.Deg2Rad), Mathf.Sin(tempRot * Mathf.Deg2Rad));          
            throwingDaggerProjectile.damage = GetDamage();
        }
        ttl = weaponStats.duration;
        ttl -= Time.deltaTime;
        if (ttl < 0f)
        {
            Destroy(gameObject);
        }
    }
}
