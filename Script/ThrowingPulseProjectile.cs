using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingPulseProjectile : MonoBehaviour
{
    EnerPulseWeapon enerPulseWeapon;   
    Vector3 direction;
    [SerializeField] float speed;
    public int damage;

    float ttl = 2f;
    private void Start()
    {
        enerPulseWeapon = FindObjectOfType<EnerPulseWeapon>();
        ttl = enerPulseWeapon.weaponStats.duration;
    }
    public void SetDirection(float dir_x,float dir_y)
    {
        direction = new Vector3(dir_x, dir_y);
        if (dir_x < 0)
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
        damage = enerPulseWeapon.damage;
        speed = enerPulseWeapon.moveSpeed;
        if (Time.frameCount % 6 == 0)
        {
            transform.position += direction * speed * Time.deltaTime;
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 0.7f);
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
            ttl = enerPulseWeapon.weaponStats.duration;
        }
    }

    public void PostDamage(int damage, Vector3 worldPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), worldPosition);
    }
}
