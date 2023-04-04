using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickUpRange : MonoBehaviour
{
    GameObject player;
    float distance;

    Rigidbody2D rgdb2d;
    private void Start()
    {
        player = GameManager.instance.playerTransform.gameObject;
        distance = FindObjectOfType<Character>().pickUpRange;
        rgdb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float range = Vector3.Distance(transform.position, player.transform.position);
        if(rgdb2d != null)
        {
            if (range < distance)
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                rgdb2d.velocity = direction * 25f;
            }
        }

    }
}
