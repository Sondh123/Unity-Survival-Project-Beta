using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGameover : MonoBehaviour
{
    public GameObject gameOverPanel;
    //PauseManager pauseManager;
    //[SerializeField] GameObject weaponParent;

    //private void Start()
    //{
    //    pauseManager = GetComponent<PauseManager>();
    //}
    public void GameOver()
    {
        Debug.Log("Game Over");

        GetComponent<PlayerMove>().enabled = false;
        //pauseManager.PauseGame();
        //GetComponent<Character>().currentHp -= GetComponent<Character>().currentHp;
        FindObjectOfType<AudioManager>().Play("Lose Game Panel");
        GetComponent<Character>().enabled = false;
        gameOverPanel.SetActive(true);
        //weaponParent.SetActive(false);
    }
}
