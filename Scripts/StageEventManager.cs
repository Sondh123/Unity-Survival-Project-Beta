using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageData stageData;
    [SerializeField] EnemiesManager enemiesManager;

    StageTime stageTime;
    [SerializeField] float spawnTimer;

    int eventIndexer;
    int eventIndexerRepeat;
    float timer;
    PlayerWinManager playerWin;

    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
    }
    private void Start()
    {
        playerWin = FindObjectOfType<PlayerWinManager>();
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
                case StageEventType.SpawnEnemyRepeat:
                    SpawnEnemyRepeat(false);
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
        for (int i = 0; i < stageData.stageEvents[eventIndexer].number; i++)
        {
            for(int j = 0; j < stageData.stageEvents[eventIndexer].enemToSpawn.Count; j++)
            {
                enemiesManager.SpawnEnemy(stageData.stageEvents[eventIndexer].enemToSpawn[j], bossEnemy);
            }
            
        }
            
    }

    private void SpawnObject()
    {
        for (int i = 0; i < stageData.stageEvents[eventIndexer].number; i++)
        {
            Vector3 positionToSpawn = GameManager.instance.playerTransform.position;
            positionToSpawn += UtilityTool.GenerateRandomPositionSquarePattern(new Vector2(5f, 5f));

            SpawnManager.instance.SpawnObject(positionToSpawn,
                stageData.stageEvents[eventIndexer].objectToSpawn);
        }
    }

    private void SpawnEnemyRepeat(bool bossEnemy)
    {
        if(timer > -5f)
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                for (int i = 0; i < stageData.stageEvents[eventIndexer].number; i++)
                {
                    for (int j = 0; j < stageData.stageEvents[eventIndexer].enemToSpawn.Count; j++)
                    {
                        enemiesManager.SpawnEnemy(stageData.stageEvents[eventIndexer].enemToSpawn[j], bossEnemy);
                    }

                }
                timer = stageData.stageEvents[eventIndexer].timePerSpawn;
            }
        }
   
    }
}
