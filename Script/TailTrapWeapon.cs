using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailTrapWeapon : WeaponBase
{
    [SerializeField] GameObject Prefabs;   
    public float speed = 100f;
    public int damage;
    public override void Attack()
    {
        damage = weaponStats.damage;
        UpdateVectorOfAttack();
        for (int i = 0; i < weaponStats.numberOfAttack; i++)
        {
            GameObject setTrap = Instantiate(Prefabs);
            FindObjectOfType<AudioManager>().Play("ZomTrap");

            Vector3 newTrapPosition = transform.position;

            setTrap.transform.position = newTrapPosition;

            SetTailTrap setTailTrap = setTrap.GetComponent<SetTailTrap>();
            setTrap.GetComponent<SetTailTrap>().SetDirection(vectorOfAttack.x, vectorOfAttack.y);
            setTailTrap.damage = GetDamage();
        }

    }
}
