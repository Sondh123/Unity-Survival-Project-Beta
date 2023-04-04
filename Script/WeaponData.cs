using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class WeaponStats
{
    public int level = 1;

    public int damage;
    public float timeToAttack;
    public int numberOfAttack;
    public float area;
    public float speed;
    public float duration;

    public List<int> specialEffect;


    public WeaponStats(int damage, float timeToAttack, int numberOfAttack, float areaOfAttack, float speedOfAttack, int levelOfWeapon,float durationOfWeapon, List<int> specialEffectWeapon)
    {
        this.damage = damage;
        this.timeToAttack = timeToAttack;
        this.numberOfAttack = numberOfAttack;
        this.area = areaOfAttack;
        this.speed = speedOfAttack;
        this.level = levelOfWeapon;
        this.duration = durationOfWeapon;
        this.specialEffect = specialEffectWeapon;
    }

    internal void Sum(WeaponStats weaponUpgradeStats)
    {
        this.damage += weaponUpgradeStats.damage;
        this.timeToAttack += weaponUpgradeStats.timeToAttack;
        this.numberOfAttack += weaponUpgradeStats.numberOfAttack;
        this.area += weaponUpgradeStats.area;
        this.speed += weaponUpgradeStats.speed;
        this.level += weaponUpgradeStats.level;
        this.duration += weaponUpgradeStats.duration;
    }
}

[CreateAssetMenu]

public class WeaponData : ScriptableObject
{
    public string Name;
    public WeaponStats stats;
    public GameObject weaponBasePrefab;

    public List<UpgradeData> upgrades;
}
