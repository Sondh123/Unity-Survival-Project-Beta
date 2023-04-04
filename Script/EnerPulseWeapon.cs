using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnerPulseWeapon : WeaponBase
{
    [SerializeField]
    int numberOfProjectiles;
    [SerializeField]
    GameObject projectile;
    Vector2 startPoint;

    public float radius, moveSpeed;
    public int damage;
    public float ttl = 2f;

    Transform spriteRotation;

    void Start()
    {
        radius = 5f;
        moveSpeed = weaponStats.speed;
        spriteRotation = GetComponentInChildren<Transform>();
    }
    public override void Attack()
    {
        damage = weaponStats.damage;
        moveSpeed = weaponStats.speed;

        startPoint = GameManager.instance.playerTransform.position;
        UpdateVectorOfAttack();

        float anglestep = 360f / weaponStats.numberOfAttack;
        float angle = 0f;

        for (int i = 0; i <= weaponStats.numberOfAttack - 1; i++)
        {
            float projectileDirxposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
            FindObjectOfType<AudioManager>().Play("EnergyPulse");
            Vector2 projectilevector = new Vector2(projectileDirxposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectilevector - startPoint).normalized * moveSpeed;

            var proj = Instantiate(projectile, startPoint, Quaternion.identity);

            proj.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            proj.GetComponentInChildren<Transform>().eulerAngles -= new Vector3(0f, 0f, -90f + angle); 

            angle += anglestep;

        }
        
        //ttl -= Time.deltaTime;
        //if (ttl < 0f)
        //{
        //    Destroy(gameObject);
        //    ttl = weaponStats.duration;
        //}

    }
}
