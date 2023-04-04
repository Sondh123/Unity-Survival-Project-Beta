using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectContainer;

    [SerializeField] WeaponData startingWeapon;

    List<WeaponBase> weapons;

    Character character;
    private void Awake()
    {
        weapons = new List<WeaponBase>();
    }
    private void Start()
    {
        AddWeapon(startingWeapon);
        character = GetComponent<Character>();
    }
    public void AddWeapon(WeaponData weaponData)
    {
        GameObject weaponGameObject = Instantiate(weaponData.weaponBasePrefab, weaponObjectContainer);

        WeaponBase weaponBase = weaponGameObject.GetComponent<WeaponBase>();

        weaponBase.SetData(weaponData);
        weapons.Add(weaponBase);
        weaponBase.AddOwnerCharacter(character);
        weaponGameObject.GetComponent<WeaponBase>().SetData(weaponData);
        Level level = GetComponent<Level>();
        if(level != null)
        {
            level.AddUpgraddesIntoTheListOfAvailableUpgrades(weaponData.upgrades);
        }

    }

    internal void UpgradeWeapon(UpgradeData upgradeData)
    {
        WeaponBase weaponToUpgrade =  weapons.Find(wd => wd.weaponData == upgradeData.weaponData);
        weaponToUpgrade.Upgrade(upgradeData);
    }
}
