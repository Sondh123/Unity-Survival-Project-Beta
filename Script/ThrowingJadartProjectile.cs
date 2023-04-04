using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingJadartProjectile : MonoBehaviour
{
    Vector3 direction;
    Vector3 _targetPosition;
    Vector3 trueDirection;
    [SerializeField] float speed;
    [HideInInspector]
    public int damage = 4;

    float ttl = 6f;
    JadartWeapon jadartWeapon;

    private void Start()
    {
        jadartWeapon = FindObjectOfType<JadartWeapon>();
        gameObject.AddComponent<Bouncing>();
        ttl = jadartWeapon.weaponStats.duration;
    }
    public void SetDirection(float dir_x, float dir_y)
    {       
        direction = new Vector3(dir_x, dir_y).normalized;
        if (dir_x < 0 )
        {
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            transform.localScale = scale;
            
        }   
    }
    bool hitDetected = false;
    private void Update()
    {
        damage = jadartWeapon.damage;
        speed = jadartWeapon.speed;
        if (Time.frameCount % 6 == 0)
        {
            transform.position += direction * speed * Time.deltaTime;
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 0.7f); //vi tri collider

            foreach (Collider2D c in hit)
            {
                IDDamageable enemy = c.GetComponent<IDDamageable>();
                if (enemy != null)
                {
                    PostDamage(damage, transform.position);
                    hitDetected = true;
                    enemy.TakeDamage(damage);
                    break;
                }
            }
            if (hitDetected == true)
            {
                Destroy(gameObject);

            }
        }
        
        ttl -= Time.deltaTime;
        if (ttl < 0f)
        {
            Destroy(gameObject);
            ttl = jadartWeapon.weaponStats.duration;
        }
    }
    public void PostDamage(int damage, Vector3 worldPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), worldPosition);
    }


}
