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

    [SerializeField] float progressTimeRate = 30f;
    [SerializeField] float progressPerSplit = 0.2f;
    [SerializeField] float nightVeilRate = 5.0f;

    private void Start()
    {
        
    }
    public float Progress
    {
        get {
            //return 1f + stageTime.time / progressTimeRate * progressPerSplit;
            return 1f;
        }
    }

    public float NightVeilProgress
    {

        get{
            //return 1f * (day/10);
            return nightVeilRate;
        }
    }

}
