using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    PlayerWinManager playerWin;
    private void Start()
    {
        playerWin = FindObjectOfType<PlayerWinManager>();
    }
    // Update is called once per frame
    private void OnDisable()
    {
        // If object will destroy in the end of current frame..
        if (gameObject.activeInHierarchy)
        {

            playerWin.Win();

        }
    }
}
