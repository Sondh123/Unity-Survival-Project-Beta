using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    public GameObject StartButton;

    public void HideButton()
    {
        if( StartButton != null)
        {
            Animator animator = StartButton.GetComponent<Animator>();
            if( animator != null)
            {
                bool onClick = animator.GetBool("show");
                animator.SetBool("show", !onClick);
            }
        }
    }
}
