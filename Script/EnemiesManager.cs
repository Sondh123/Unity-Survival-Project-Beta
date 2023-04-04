using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesSpawnGroup
{
    public EnemyData enemyData;
    public int count;
    public bool isBoss;
    public float repeatTimer;
    public float timeBetweenSpawn;
    public int repeatCount;

    public EnemiesSpawnGroup(EnemyData enemyData, int count, bool isBoss)
    {
        this.enemyData = enemyData;
        this.count = count;
        this.isBoss = isBoss;
    }

    public void SetRepeatSpawn(float timeBetweenSpawn, int repeatCount)
    {
        this.timeBetweenSpawn = timeBetweenSpawn;
        this.repeatCount = repeatCount;
        repeatTimer = timeBetweenSpawn;
    }
}
public class EnemiesManager : MonoBehaviour
{
    [SerializeField] StageProcess stageProcess;
    [SerializeField] GameObject enemy;   
    [SerializeField] Vector2 spawnArea;
    GameObject player;

    List<Enemy> bossEnemiesList;
    int totalBossHealth;
    int currentBosssHealth;
    public string bossName;
    [SerializeField] Slider bossHealthBar;

    DayNightSystem2D dayNightSystem2D;   

    List<EnemiesSpawnGroup> enemiesSpawnGroupList;
    List<EnemiesSpawnGroup> repeatedSpawnGroupList;

    int spawnPerFrame = 2;
    //DayNightSystem2D dayNightSystem2D = new DayNightSystem2D();

    private void Start()
    {
        player = GameManager.instance.playerTransform.gameObject;
        dayNightSystem2D = DayNightSystem2D.instance;
        bossHealthBar = FindObjectOfType<BossHPBar>(true).GetComponent<Slider>();
        stageProcess = FindObjectOfType<StageProcess>();

    }

    private void Update()
    {
        ProcessSpawn();
        ProcessRepeatedSpawnGroup();
        UpdateBossHealth();

    }

    private void ProcessRepeatedSpawnGroup()
    {
        if(repeatedSpawnGroupList == null)
        {
            return;
        }
        for(int i = repeatedSpawnGroupList.Count - 1; i >=0; i--)
        {
            repeatedSpawnGroupList[i].repeatTimer -= Time.deltaTime;
            if (repeatedSpawnGroupList[i].repeatTimer < 0)
            {
                repeatedSpawnGroupList[i].repeatTimer = repeatedSpawnGroupList[i].timeBetweenSpawn;
                AddGroupToSpawn(repeatedSpawnGroupList[i].enemyData, repeatedSpawnGroupList[i].count, repeatedSpawnGroupList[i].isBoss);
                repeatedSpawnGroupList[i].repeatCount -= 1;
                if (repeatedSpawnGroupList[i].repeatCount <= 0)
                {
                    repeatedSpawnGroupList.RemoveAt(i);
                }
            }
        }
    }

    private void ProcessSpawn()
    {
        if(enemiesSpawnGroupList == null)
        {
            return;
        }
        for(int i = 0;i< spawnPerFrame; i++)
        {
            if(enemiesSpawnGroupList.Count > 0)
            {
                if(enemiesSpawnGroupList[0].count <= 0)
                {
                    return;
                }
                SpawnEnemy(enemiesSpawnGroupList[0].enemyData, enemiesSpawnGroupList[0].isBoss);
                enemiesSpawnGroupList[0].count -= 1;
                if(enemiesSpawnGroupList[0].count <= 0)
                {
                    enemiesSpawnGroupList.RemoveAt(0);
                }
            }
        }

    }

    private void UpdateBossHealth()
    {
        if(bossEnemiesList == null) { return; }
        if(bossEnemiesList.Count == 0) { return; }
        currentBosssHealth = 0;

        for(int i = 0; i< bossEnemiesList.Count; i++)
        {         
            currentBosssHealth += bossEnemiesList[i].stats.hp;
        }
        bossHealthBar.value = currentBosssHealth;
        if(currentBosssHealth <= 0)
        {
            bossHealthBar.gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Play("Boss Death");
            bossEnemiesList.Clear();
        }
    }

    public void AddRepeatedSpawn(StageEvent stageEvent, bool isBoss)
    {
        EnemiesSpawnGroup repeatSpawnGroup = new EnemiesSpawnGroup(stageEvent.enemyToSpawn, stageEvent.number, isBoss);
        repeatSpawnGroup.SetRepeatSpawn(stageEvent.repeatEverySecond, stageEvent.repeatCount);

        if(repeatedSpawnGroupList == null)
        {
            repeatedSpawnGroupList = new List<EnemiesSpawnGroup>();
        }

        repeatedSpawnGroupList.Add(repeatSpawnGroup);
    }

    public void AddGroupToSpawn(EnemyData enemyToSpawn, int count, bool isBoss)
    {
        EnemiesSpawnGroup newGroupToSpawn = new EnemiesSpawnGroup(enemyToSpawn, count, isBoss);

        if(enemiesSpawnGroupList == null)
        {
            enemiesSpawnGroupList = new List<EnemiesSpawnGroup>();
        }

        enemiesSpawnGroupList.Add(newGroupToSpawn);

    }
    public void SpawnEnemy(EnemyData enemyToSpawn, bool isBoss)
    {
        Vector3 position = UtilityTool.GenerateRandomPositionSquarePattern(spawnArea);

        position += player.transform.position;

        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;
        newEnemy.AddComponent<BoxCollider2D>();

        Enemy newEnemyComponent = newEnemy.GetComponent<Enemy>();
      
        newEnemyComponent.SetTarget(player);
        newEnemyComponent.Setstats(enemyToSpawn.stats);
        newEnemyComponent.UpdateStatsForProgress(stageProcess.Progress);
        

        if (isBoss == true)
        {
            SpawnBossEnemy(newEnemyComponent);
            bossName = enemyToSpawn.Name;
        }
        if (dayNightSystem2D.nightVeil == true && isBoss == false)
        {
            
            newEnemyComponent.UpdateStatsForNightVeilProgress(stageProcess.NightVeilProgress);
        }

        newEnemy.transform.parent = transform;

        //spawning sprite object of the enemy
        GameObject spriteObject = Instantiate(enemyToSpawn.animatedPrefab);
        spriteObject.transform.parent = newEnemy.transform;
        spriteObject.transform.localPosition = Vector3.zero;

    }

    private void SpawnBossEnemy(Enemy newBoss)
    {
        if(bossEnemiesList == null)
        {
            bossEnemiesList = new List<Enemy>();
        }
        bossEnemiesList.Add(newBoss);

        totalBossHealth += newBoss.stats.hp;
        bossHealthBar.gameObject.SetActive(true);
        bossHealthBar.maxValue = totalBossHealth;
    }
}
