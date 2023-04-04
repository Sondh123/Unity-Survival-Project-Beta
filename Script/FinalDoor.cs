using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    [SerializeField] GameObject door;
    StageTime stageTime;
    [SerializeField] float count = 10;
    // Start is called before the first frame update
    void Start()
    {
        stageTime = FindObjectOfType<StageTime>();
    }

    // Update is called once per frame
    private void Update()
    {
        door.SetActive(false);
        
        if(stageTime.time > count)
        {
            door.SetActive(true);
        }
    }
}
