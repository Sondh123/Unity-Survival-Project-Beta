using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform destination;
    public Color myColor;
   
    public Color remainColor;
    private void Start()
    {
        
        remainColor = GetComponent<SpriteRenderer>().color;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = myColor;
        }
          
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<SpriteRenderer>().color = remainColor;
        }

    }
    public Transform GetDestination()
    {
        return destination;
    }
    
}