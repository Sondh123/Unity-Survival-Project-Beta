using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTime : MonoBehaviour
{
    public float time;
    TimerUI timerUI;
    DaysUI daysUI;
    Character isDead;
    DayNightSystem2D dayNightSystem;
    float count;
    int dayCount;
    private void Awake()
    {
        timerUI = FindObjectOfType<TimerUI>();
        isDead = FindObjectOfType<Character>();
        daysUI = FindObjectOfType<DaysUI>();
        dayNightSystem = FindObjectOfType<DayNightSystem2D>();
        time = Time.deltaTime;
        
    }

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Map 1 SoundStrack");
        //if (dayNightSystem.nightVeil == true)
        //{
        //    FindObjectOfType<AudioManager>().Stop("Map 1 SoundStrack");
        //    FindObjectOfType<AudioManager>().Play("Night");
        //}
        //FindObjectOfType<AudioManager>().Stop("Night");
    }
    private void Update()
    {


        dayCount = dayNightSystem.dayCount;
        time += Time.deltaTime;
        timerUI.UpdateTime(time);
        daysUI.UpdateDay(dayCount);
        count = Time.deltaTime;
        if (isDead.isDead == true)
        {
            
            time = 0;
        }


    }
}
