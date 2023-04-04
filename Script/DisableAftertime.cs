using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAftertime : MonoBehaviour
{
    float timeToDisable = 0.8f;
    float timer;

    private void OnEnable()
    {
        timer = timeToDisable;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0f)
        {
            gameObject.SetActive(false);
        }
    }
}

