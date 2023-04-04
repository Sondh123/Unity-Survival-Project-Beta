using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectionOfAttack
{
    None,
    Forward,
    LeftRight,
    UpDown
}
public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData weaponData;
    
    public WeaponStats weaponStats;

    Character wielder;
    float timer;

    public Vector2 vectorOfAttack;
    [SerializeField] DirectionOfAttack attackDirection;
    PlayerMove playerMove;
    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }
    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Attack();
            timer = weaponStats.timeToAttack;
            
        }
    }

    public virtual void SetData(WeaponData wd)
    {
        weaponData = wd;
        

        weaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack, wd.stats.numberOfAttack, wd.stats.area, wd.stats.speed, wd.stats.level, wd.stats.duration, wd.stats.specialEffect);
    }

    public abstract void Attack();

    // bonus damage cho Weapon
    public int GetDamage()
    {
        //int damage = (int)weaponData.stats.damage * wielder.damageBonus;
        int damage = (int)weaponData.stats.damage;
        return damage;
    }
    public virtual void PostDamage(int damage, Vector3 targetPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), targetPosition);
    }

    public void AddOwnerCharacter(Character character)
    {
        wielder = character;
    }

    public void Upgrade(UpgradeData upgradeData)
    {
        weaponStats.Sum(upgradeData.weaponUpgradeStats);
    }

    public void UpdateVectorOfAttack()
    {
        if(attackDirection == DirectionOfAttack.None)
        {
            vectorOfAttack = Vector2.zero;
            return;
        }

        switch (attackDirection)
        {
            case DirectionOfAttack.Forward:
                vectorOfAttack.x = playerMove.lastHorizontalCoupledVector;
                vectorOfAttack.y = playerMove.lastVerticalCoupledVector;
                break;
            case DirectionOfAttack.LeftRight:
                vectorOfAttack.x = playerMove.lastHorizontalDeCoupledVector;
                vectorOfAttack.y = 0f;
                break;
            case DirectionOfAttack.UpDown:
                vectorOfAttack.x = 0f;
                vectorOfAttack.y = playerMove.lastVerticalDeCoupledVector;
                break;
        }
        vectorOfAttack = vectorOfAttack.normalized;
    }
}
