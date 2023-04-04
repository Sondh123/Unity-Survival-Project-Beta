using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DaysUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateDay(int day)
    {
        if(day < 10)
        {
            text.text = "0" + day.ToString();
        }
        else
        {
            text.text = day.ToString();
        }
        

    }
}
