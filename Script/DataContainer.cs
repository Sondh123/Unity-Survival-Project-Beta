using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerPersistentUpgrades
{
    HP,
    Armor,
    Speed,
    Area,
}
[Serializable]
public class PlayerUpgrades
{
    public PlayerPersistentUpgrades persistentUpgrades;
    public int level = 0;
    public int costToUpgrade = 100;
}
[CreateAssetMenu]
public class DataContainer : ScriptableObject
{
    public int coins;

    public int hp;
    public int armor;
    public float moveSpeed;

    public List<bool> stageCompletion;

    public List<PlayerUpgrades> upgrades;

    public void StageComplete(int i)
    {
        stageCompletion[i] = true;
    }
}
