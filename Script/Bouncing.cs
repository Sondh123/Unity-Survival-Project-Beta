using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncing : MonoBehaviour
{
    private Rigidbody2D rb;

    Vector3 lastVeclocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        lastVeclocity = rb.velocity;
    }
    private void OnCollisionStay2D(Collision2D coll)
    {
        var speed = lastVeclocity.magnitude;
        Debug.Log(speed);
        var direction = Vector3.Reflect(lastVeclocity.normalized, coll.contacts[0].normal);

        rb.velocity = direction * Mathf.Max(speed, 0f);

    }
}
