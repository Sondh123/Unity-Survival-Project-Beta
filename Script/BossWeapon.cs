using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private Transform player;
    [SerializeField] Transform shotPoint;
    [SerializeField] Transform gun;

    [SerializeField] GameObject enemyProjectile;

    [SerializeField] float followPlayerRange;
    private bool inRange;
    [SerializeField] float attackRange;
    [SerializeField] int damage = 120;

    [SerializeField] float startTimeBtwnShots;
    private float timeBtwnShots;
    Character targetCharacter;

    // Update is called once per frame

    private void Start()
    {
        player = GameManager.instance.playerTransform;
        targetCharacter = FindObjectOfType<Character>();
    }
    void Update()
    {
        Vector3 differance = player.position - gun.transform.position;
        float rotZ = Mathf.Atan2(differance.y, differance.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            if (timeBtwnShots <= 0)
            {
                Instantiate(enemyProjectile, shotPoint.position, shotPoint.transform.rotation);
                timeBtwnShots = startTimeBtwnShots;
            }
            else
            {
                timeBtwnShots -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Attack();
        }
    }

    private void Attack()
    { 

        targetCharacter.TakeDamage(damage);       
    }
}
