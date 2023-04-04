using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;
    private float count = 3;
    PlayerWinManager playerWinManager;

    private void Start()
    {
        playerWinManager = FindObjectOfType<PlayerWinManager>();
    }
    void Update()
    {   
        if (currentTeleporter != null)
        {           
            count -= Time.deltaTime;
            if(count < 0)
            {
                transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
                playerWinManager.Win();
                count = 3;
            }
            
        }
        else
        {
            count = 3;
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.CompareTag("Teleporter"))
        {       
            currentTeleporter = collision.gameObject;
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
                
            }
        }
    }
}