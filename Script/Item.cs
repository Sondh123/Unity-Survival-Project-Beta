using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemStats
{
    public int armor;
    public int hp;
    public int level;
    public float speed;

    internal void Sum(ItemStats stats)
    {
        armor += stats.armor;
        level += stats.level;
        hp += stats.hp;
        speed += stats.speed;
    }
}

[CreateAssetMenu]

public class Item : ScriptableObject
{
    public string Name;
    public ItemStats stats;
    public List<UpgradeData> upgrades;

    public void Init(string Name)
    {
        this.Name = Name;
        stats = new ItemStats();
        upgrades = new List<UpgradeData>();
    }

    public void Equip(Character character)
    {
        character.armor += stats.armor;
        character.maxHp += stats.hp;
        character.speed += stats.speed;
    }
   
    public void UnEquip(Character character)
    {
        character.armor -= stats.armor;
        character.maxHp -= stats.hp;
        character.speed -= stats.speed;
    }
}
