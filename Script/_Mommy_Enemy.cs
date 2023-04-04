using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Mommy_Enemy : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    [SerializeField] EnemyData childrenPrefab;
    EnemiesManager enemiesManager;
    [SerializeField] GameObject enemy;
    GameObject player;

    private void Awake()
    {
        enemiesManager = GetComponent<EnemiesManager>();
    }
    private void Start()
    {
        player = GameManager.instance.playerTransform.gameObject;
 
    }
    private void OnDisable()
    {
        // If object will destroy in the end of current frame..
        if (gameObject.activeInHierarchy)
        {
            
            GiveBirth(childrenPrefab);           
            
        }
    }

    private void GiveBirth(EnemyData childrenPrefabs)
    {
        Debug.Log("ok");
        for (int i = 0; i < 4; i++)
        {
            Spawn(childrenPrefab);
        }
    }

    private void Spawn(EnemyData enemyToSpawn)
    {
        Vector3 position = new Vector3();

        position += gameObject.transform.position;

        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;

        //////
        /////

        Enemy newEnemyComponent = newEnemy.GetComponent<Enemy>();
        newEnemyComponent.SetTarget(player);
        newEnemyComponent.Setstats(enemyToSpawn.stats);

        newEnemy.transform.parent = transform;

        //spawning sprite object of the enemy
        GameObject spriteObject = Instantiate(enemyToSpawn.animatedPrefab);
        spriteObject.transform.parent = newEnemy.transform;
        spriteObject.transform.localPosition = Vector3.zero;
    }



}
