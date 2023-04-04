using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageData stageData;
    EnemiesManager enemiesManager;

    StageTime stageTime;
    [SerializeField] float spawnTimer;

    int eventIndexer;
    float timer;
    PlayerWinManager playerWin;

    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
    }
    private void Start()
    {
        playerWin = FindObjectOfType<PlayerWinManager>();
        enemiesManager = FindObjectOfType<EnemiesManager>();
    }
    private void Update()
    {
        if (eventIndexer >= stageData.stageEvents.Count)
        {
            return;
        }
        
        if (stageTime.time > stageData.stageEvents[eventIndexer].time)
        {
            switch (stageData.stageEvents[eventIndexer].eventType)
            {
                case StageEventType.SpawnEnemy:                    
                    SpawnEnemy(false);                    
                    break;
                case StageEventType.SpawnObject:                                        
                    SpawnObject();                    
                    break;
                case StageEventType.WinStage:
                    WinStage();
                    break;
                case StageEventType.SpawnEnemyBoss:
                    SpawnEnemyBoss();
                    break;
            }
            Debug.Log(stageData.stageEvents[eventIndexer].message);

            eventIndexer += 1;
        }
    }
    private void SpawnEnemyBoss()
    {
        SpawnEnemy(true);
    }

    private void WinStage()
    {
        playerWin.Win();
    }

    private void SpawnEnemy(bool bossEnemy)
    {
        StageEvent currentEvent = stageData.stageEvents[eventIndexer];
        enemiesManager.AddGroupToSpawn(currentEvent.enemyToSpawn, currentEvent.number, bossEnemy);   
        
        if(currentEvent.isRepeatedEvent == true)
        {
            enemiesManager.AddRepeatedSpawn(currentEvent, bossEnemy);
        }
    }

    private void SpawnObject()
    {    
        StageEvent currentEvent = stageData.stageEvents[eventIndexer];
        for(int i = 0; i < currentEvent.number; i++)
        {
            Vector3 positionToSpawn = GameManager.instance.playerTransform.position;
            positionToSpawn += UtilityTool.GenerateRandomPositionSquarePattern(new Vector2(45f, 50f));

            SpawnManager.instance.SpawnObject(positionToSpawn,stageData.stageEvents[eventIndexer].objectToSpawn);          
            
        }
    }

}
