using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageProcess : MonoBehaviour
{
    StageTime stageTime;
    

    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
        
    }

    [SerializeField] float progressTimeRate = 20f;
    [SerializeField] float progressPerSplit = 1f;
    [SerializeField] float nightVeilRate = 2.0f;

    private void Start()
    {
        
    }
    public float Progress
    {
        get {
            return 1f + stageTime.time / progressTimeRate * progressPerSplit;          
        }
    }

    public float NightVeilProgress
    {

        get{           
            return nightVeilRate;
        }
    }

}
