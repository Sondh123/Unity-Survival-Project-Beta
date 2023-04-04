using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusic : MonoBehaviour
{ 
    Character character;

    private void Start()
    {
        
        character = FindObjectOfType<Character>();

    }
    private void Update()
    {
        if(character.isDead == true)
        {
            FindObjectOfType<AudioManager>().Stop("Map 1 SoundStrack");
        }
    }
}
