using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnemy : MonoBehaviour
{
    public Vector3 size;
    public Vector2 offSet;
    public float scale;

    private void Awake()
    {
        size = GetComponent<BoxCollider2D>().size;
        offSet = GetComponent<BoxCollider2D>().offset;

        scale = GetComponent<Transform>().localScale.x;
    }
}
