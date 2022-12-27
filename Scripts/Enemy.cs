using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    public float moveSpeed = 1f;

    public int hp = 90;
    public int damage = 10;
    public int experience_reward = 400;
    

    public EnemyStats(EnemyStats stats)
    {
        this.hp = stats.hp;
        this.damage = stats.damage;
        this.experience_reward = stats.experience_reward;
        this.moveSpeed = stats.moveSpeed;
    }

    internal void ApplyProgress(float progress)
    {
        this.hp = (int)(hp * progress);
        this.damage = (int)(damage * progress);
    }

    internal void ApplyNightVeilProgress(float x)
    {
        Debug.Log("Stat Enemy OK");
        this.moveSpeed = moveSpeed * x;
        this.damage = (int)(damage * 0.25f * x);
    }
}

public class Enemy : MonoBehaviour, IDDamageable
{
    public Transform targetDestination;
    GameObject targetGameobject;
    Character targetCharacter;

    Rigidbody2D rgbd2d;

    public EnemyStats stats;
    public bool isToBeDestroyed = false;

    [Header("Dissolve")]
    Material materialEnemy;
    float fade = 1f;
    bool isDissolving = false;

    private void Awake()
    {
        
        rgbd2d = GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        materialEnemy = GetComponentInChildren<SpriteRenderer>().material;
       
    }
    public void SetTarget(GameObject target)
    {
        targetGameobject = target;
        targetDestination = target.transform;
    }

    internal void UpdateStatsForNightVeilProgress(float x)
    {
        
        stats.ApplyNightVeilProgress(x);
    }

    internal void UpdateStatsForProgress(float progress)
    {
        stats.ApplyProgress(progress);
    }

    private void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rgbd2d.velocity = direction * stats.moveSpeed;
        
    }

    internal void Setstats(EnemyStats stats)
    {
        this.stats = new EnemyStats(stats);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameobject)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (targetCharacter == null)
        {
           
            targetCharacter = targetGameobject.GetComponent<Character>();
        }

        targetCharacter.TakeDamage(stats.damage);
    }
    public void TakeDamage(int damage)
    {
        stats.hp -= damage;
        
        if (stats.hp < 1)
        {
            isDissolving = true;

            targetGameobject.GetComponent<Level>().AddExperience(stats.experience_reward);
                  
            GetComponent<DropOnDestroy>().CheckDrop();

            isToBeDestroyed = true;




        }
    }

    private void Update()
    {
        materialEnemy.SetFloat("_Fade", fade);

        if (isDissolving == true)
        {
            
            fade -= Time.deltaTime;
            if (fade <= 0f)
            {

                fade = 0f;
                Destroy(gameObject);
                isDissolving = false;

            }

            
        }

    }

}
