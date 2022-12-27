using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] StageProcess stageProcess;
    [SerializeField] GameObject enemy;   
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnTimer;
    GameObject player;

    List<Enemy> bossEnemiesList;
    int totalBossHealth;
    int currentBosssHealth;
    public string bossName;
    [SerializeField] Slider bossHealthBar;

    private bool nightVeil;
    DayNightSystem2D dayNightSystem2D;

    [SerializeField] public List<Enemy> listEnemy;
    

    //DayNightSystem2D dayNightSystem2D = new DayNightSystem2D();

    private void Start()
    {
        player = GameManager.instance.playerTransform.gameObject;
        dayNightSystem2D = DayNightSystem2D.instance;
        bossHealthBar = FindObjectOfType<BossHPBar>(true).GetComponent<Slider>();

    }

    private void Update()
    {
        UpdateBossHealth();

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

    public void SpawnEnemy(EnemyData enemyToSpawn, bool isBoss)
    {
        Vector3 position = UtilityTool.GenerateRandomPositionSquarePattern(spawnArea);

        position += player.transform.position;

        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;

        Enemy newEnemyComponent = newEnemy.GetComponent<Enemy>();
        newEnemyComponent.SetTarget(player);
        newEnemyComponent.Setstats(enemyToSpawn.stats);
        newEnemyComponent.UpdateStatsForProgress(stageProcess.Progress);

        if(isBoss == true)
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

        this.listEnemy.Add(newEnemyComponent);
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
