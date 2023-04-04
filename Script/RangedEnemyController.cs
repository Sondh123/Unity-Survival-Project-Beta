using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private Transform player;
    [SerializeField] Transform shotPoint;
    [SerializeField] Transform gun;

    [SerializeField] GameObject enemyProjectile;

    [SerializeField] float followPlayerRange;
    private bool inRange;
    [SerializeField] float attackRange;

    [SerializeField] float startTimeBtwnShots;
    private float timeBtwnShots;

    // Update is called once per frame

    private void Start()
    {
        player = GameManager.instance.playerTransform;
    }
    void Update()
    {
        Vector3 differance = player.position - gun.transform.position;
        float rotZ = Mathf.Atan2(differance.y, differance.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if (Vector2.Distance(transform.position, player.position) <= followPlayerRange && Vector2.Distance(transform.position, player.position) > attackRange)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

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

    void FixedUpdate()
    {
        if (inRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, followPlayerRange);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}
}
