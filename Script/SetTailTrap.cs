using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTailTrap : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] float speed;
    public int damage;

    private float count;
    [SerializeField] float ttl;
    TailTrapWeapon tailTrapWeapon;
    private void Start()
    {       
        tailTrapWeapon = FindObjectOfType<TailTrapWeapon>();
        transform.localScale *= tailTrapWeapon.weaponStats.area;
        damage = tailTrapWeapon.damage;
        ttl = tailTrapWeapon.weaponStats.duration;
    }
    public void SetDirection(float dir_x, float dir_y)
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
        count -= Time.deltaTime;
        if (Time.frameCount % 6 == 0)
        {
            transform.position += direction * speed * Time.deltaTime;
            
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x * 0.75f); 
            foreach (Collider2D c in hit)
            {
                IDDamageable enemy = c.GetComponent<IDDamageable>();
                if (enemy != null)
                {
                    count -= Time.deltaTime;
                    if (count < 0)
                    {

                        PostDamage(damage, transform.position);
                        hitDetected = true;
                        enemy.TakeDamage(damage);
                        break;
                    }
                    count = 0.5f;
                    
                }
            }
            if (hitDetected == true)
            {

                //Destroy(gameObject);
            }
        }

        ttl -= Time.deltaTime;
        if (ttl < 0f)
        {
            Destroy(gameObject);
        }
    }

    public void PostDamage(int damage, Vector3 worldPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), worldPosition);
    }
}
