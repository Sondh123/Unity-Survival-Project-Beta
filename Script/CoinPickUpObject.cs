using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CoinPickUpObject : MonoBehaviour, IPickUpObject
{
    [SerializeField] int count;
    
    public void OnPickUp(Character character)
    {
        character.coins.Add(count);
        if(count < 20)
        {
            FindObjectOfType<AudioManager>().Play("Coin Pick Up");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Large Coin Pick Up");
        }
              

    }
   
}
