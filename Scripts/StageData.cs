using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StageEventType
{
    SpawnEnemy,
    SpawnEnemyRepeat,
    SpawnEnemyBoss,
    SpawnObject,
    WinStage
}


[Serializable]
public class StageEvent
{
    public StageEventType eventType;

    public float time;
    public string message;

    public List<EnemyData> enemToSpawn;
    public GameObject objectToSpawn;

    public int number;

    public float timePerSpawn;
}


[Serializable]
public class StageEventContinouslyData
{
    public List<EnemyData> enemyToSpawn;
    public float time;
    public float timePerSpawn;
    public int numberPerSpawn;
}

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    public List<StageEvent> stageEvents;
    public List<StageEventContinouslyData> stageEventContinouslyData;
}