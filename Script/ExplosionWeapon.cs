using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionWeapon : WeaponBase
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] Vector2 attackSize;
    [SerializeField] Vector2 scaleSize;
    Transform scale;

    private bool slowEffect = false;
    private float slowEffectUpgrade;

    private void Start()
    {
        scale = GetComponentInChildren<Transform>();
    }
    public override void Attack()
    {        
        StartCoroutine(AttackProcess());
        scale.localScale = scaleSize * weaponStats.area;
    }
    IEnumerator AttackProcess()
    {
        explosionPrefab.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Explosion");
        Collider2D[] colliders = Physics2D.OverlapBoxAll(explosionPrefab.transform.position, attackSize * weaponStats.area, 0f);
        ApplyDamage(colliders);         

        yield return new WaitForSeconds(weaponStats.timeToAttack);
    }

    private void ApplyDamage(Collider2D[] colliders)
    {
       
        for (int i = 0; i < colliders.Length; i++)
        {
            IDDamageable e = colliders[i].GetComponent<IDDamageable>();
            if (e != null)
            {
                PostDamage(weaponStats.damage, colliders[i].transform.position);
                e.TakeDamage(weaponStats.damage);
                if (slowEffect == true)
                {
                    e.TakeEffect(slowEffectUpgrade);
                }
            }

        }
    }

    public void CheckUpgrade(int specNumber)
    {
        for (int i = 0; i < weaponStats.specialEffect.Count; i++)
        {
            if (weaponStats.specialEffect[i] == specNumber)
            {
                slowEffect = true;
                slowEffectUpgrade += 0.2f;
            }
        }
    }

}
