using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    [SerializeField] List<Item> items;

    Character character;

    


    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Start()
    {
        
    }
    public void Equip(Item itemToEquip)
    {
        if(items == null)
        {
            items = new List<Item>();
        }
        Item newItemInstace = new Item();
        newItemInstace.Init(itemToEquip.Name);
        newItemInstace.stats.Sum(itemToEquip.stats);

        items.Add(newItemInstace);
        newItemInstace.Equip(character);
    }

    public void UnEquip(Item itemToUnEquip)
    {

    }

    internal void UpgradeItem(UpgradeData upgradeData)
    {
        Item itemToUpgrade = items.Find(id => id.Name == upgradeData.item.Name);
        itemToUpgrade.UnEquip(character);
        itemToUpgrade.stats.Sum(upgradeData.itemStats);
        itemToUpgrade.Equip(character);
    }
}