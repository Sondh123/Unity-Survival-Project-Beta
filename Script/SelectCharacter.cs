using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    [SerializeField] List<GameObject> characterObject;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject prevButton;

    public int characCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void NextButton()
    {
        characCount += 1;
        for (int i = 0; i<= characterObject.Count; i++)
        {    
            characterObject[i].transform.position -= new Vector3(180, 0, 0);                                  
        }
        characterObject[characCount].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);

    }
    public void PreviousButton()
    {
        characCount -= 1;
        for (int i = characterObject.Count; i >= 0; i--)
        {
            characterObject[i].transform.position += new Vector3(180, 0, 0);
         
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (characCount == 0)
        {
            prevButton.SetActive(false);
        }
        else
        {
            prevButton.SetActive(true);
        }

        if (characCount == characterObject.Count - 1)
        {
            nextButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
        }
    }
}
