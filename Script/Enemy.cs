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
        this.hp = (int)(hp * x);
        this.moveSpeed = moveSpeed * x;
        this.damage = (int)(damage * x);
    }
}

public class Enemy : MonoBehaviour, IDDamageable
{
    public Transform targetDestination;
    GameObject targetGameobject;
    Character targetCharacter;

    Rigidbody2D rgbd2d;


    Vector3 spriteCollider2DSize;
    Vector2 spriteCollider2DOffSet;
    float scale;
    public Vector3 enemyCollider2DSize;
    public Vector2 enemyCollider2DOffSet;
    BoxCollider2D boxCollider2D;

    
    public EnemyStats stats;
    public bool isToBeDestroyed = false;
    bool isKnockBack = false;
    float tim = 0.7f;

    [Header("Dissolve")]
    Material materialEnemy1;
    Material materialEnemy2;
    List<Material> material;
    private float count;
    [SerializeField] float fade = 1f;
    [SerializeField] bool isDissolving = false;
    

    private float thrust;
    private void Awake()
    {
        
        rgbd2d = GetComponent<Rigidbody2D>();

    }
    
    private void Start()
    {
        count = GetComponentsInChildren<SpriteRenderer>().Length;
        
        if (material == null)
        {
            material = new List<Material>();
        }
        for (int i = 0; i < count; i++)
        {
            //material.Add(GetComponentsInChildren<SpriteRenderer>()[i].material);
            material.Add(GetComponentsInChildren<SpriteRenderer>()[i].material);
           
        }


        thrust = GetComponentInChildren<KnockbackEnemy>().thrust;

        spriteCollider2DSize = GetComponentInChildren<ColliderEnemy>().size;
        spriteCollider2DOffSet = GetComponentInChildren<ColliderEnemy>().offSet;
        scale = GetComponentInChildren<ColliderEnemy>().scale;
        if(transform.gameObject.GetComponent<BoxCollider2D>() != null)
        {
            boxCollider2D = transform.gameObject.GetComponent<BoxCollider2D>();
            boxCollider2D.size = spriteCollider2DSize * scale;

            boxCollider2D.offset = spriteCollider2DOffSet * scale;
        }        
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
    internal void SetCollider()
    {
  
    }

    internal void UpdateStatsForProgress(float progress)
    {
        stats.ApplyProgress(progress);
    }

    private void FixedUpdate()
    {
        if(isKnockBack == false)
        {
            Vector3 direction = (targetDestination.position - transform.position).normalized;
            rgbd2d.velocity = direction * stats.moveSpeed;
        }
        else
        {
            StartCoroutine(KnockCoroutine());
        }

       
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
        isKnockBack = true;
        if (stats.hp < 1)
        {
            boxCollider2D.isTrigger = true;
            isDissolving = true;            
            //StartCoroutine(DropCoroutine());
            isToBeDestroyed = true;
          
        }

        tim -= Time.deltaTime;
        if (tim <= 0)
        {
            return;
        }
    }

    private IEnumerator DropCoroutine()
    {
        
        transform.gameObject.GetComponentInChildren<DropOnDestroy>().CheckDrop();
        //targetGameobject.GetComponent<Level>().AddExperience(stats.experience_reward);
        yield return new WaitForSeconds(1f);
    }
    public void TakeEffect(float effect)
    {
        stats.moveSpeed -= effect * stats.moveSpeed;
    }

    private IEnumerator KnockCoroutine()
    {
        Vector3 forceDirection = (transform.position - targetDestination.position).normalized; ;
 
        rgbd2d.velocity = forceDirection * thrust;
        
        yield return new WaitForSeconds(.15f);

        rgbd2d.velocity = new Vector3();
        isKnockBack = false;
    }

    private void Update()
    {
        for (int i = 1; i < count; i++)
        {
            material[i].SetFloat("_Fade", fade);
        }

        if (isDissolving)
        {
            fade -= 3*Time.deltaTime;
            if (fade <= 0f)
            {               
                fade = 0f;
                Destroy(gameObject);
                isDissolving = false;
                

            }
        }

    }

}
