using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipWeapon : WeaponBase
{

    [SerializeField] GameObject leftWhipObject;
    [SerializeField] GameObject rightWhipObject;

    [SerializeField] Vector2 attackSize = new Vector2(4f, 2f);
    PlayerMove playerMove;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }
 
    private void ApplyDamage(Collider2D[] colliders)
    {
        int damage = GetDamage();
        for(int i = 0; i < colliders.Length; i++)
        {
            IDDamageable e = colliders[i].GetComponent<IDDamageable>();
            if(e != null)
            {
                PostDamage(damage, colliders[i].transform.position);
                e.TakeDamage(damage);
            }
           
        }
    }

    public override void Attack()
    {
        StartCoroutine(AttackProcess());
  
    }

    IEnumerator AttackProcess()
    {
        for (int i = 0; i < weaponStats.numberOfAttack; i++)
        {


            if (playerMove.lastHorizontalDeCoupledVector > 0)
            {
                rightWhipObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
            }
            else
            {
                Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
                leftWhipObject.SetActive(true);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
