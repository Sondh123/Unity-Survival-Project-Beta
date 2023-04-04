using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Boss Appear");
        FindObjectOfType<AudioManager>().Play("Boss Fight");
    }

    private void Update()
    {
        FindObjectOfType<AudioManager>().Stop("Night");
        FindObjectOfType<AudioManager>().Stop("Map 1 SoundStrack");
    }
    private void OnDisable()
    {
        // If object will destroy in the end of current frame..
        if (gameObject.activeInHierarchy)
        {

            FindObjectOfType<AudioManager>().Stop("Boss Fight");
            FindObjectOfType<AudioManager>().Play("Boss Death");
        }
    }
}
