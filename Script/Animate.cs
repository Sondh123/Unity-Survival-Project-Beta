using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    Animator animator;

    public float horizontal;

    public float vertical;

    public bool standing;
     
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (horizontal == 0 && vertical == 0)
        {
            standing = true;
        }
        else
        {

            standing = false;
        }
        animator.SetFloat("Horizontal", horizontal); // tao chuyen dong
        animator.SetFloat("Vertical", vertical);
        animator.SetBool("Standing", standing);
    }
}
