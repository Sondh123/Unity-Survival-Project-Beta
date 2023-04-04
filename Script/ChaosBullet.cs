using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosBullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] float lifeTime;
    [SerializeField] int dame = 10;
    GameObject targetGameobject;
    Character targetCharacter;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        targetGameobject = GameManager.instance.playerTransform.gameObject;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
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
        targetCharacter.TakeDamage(dame);

        Destroy(gameObject);

    }
}
