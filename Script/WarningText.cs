using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WarningText : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI warningText;
    [SerializeField] GameObject panel;

    [SerializeField] float timeWarning = 5.0f;
    [SerializeField] float count;

    [Header("Float Text")]
    [SerializeField] float BlinkFadeInTime = 0.5f;
    [SerializeField] float BlinkStayTime = 0.8f;
    [SerializeField] float BlinkFadeOutTime = 0.7f;
    private float timeChecker = 0;
    private Color color;

    DayNightSystem2D dayNightSystem2D;
    private void Start()
    {
        dayNightSystem2D = DayNightSystem2D.instance;
        count = timeWarning;

        color = warningText.color;
    }

    public void ShowWarningText(string word)
    {

        panel.SetActive(true);
        
        warningText.text = "! " + word.ToString() + " !";



        count -= Time.deltaTime;
        if (count < 0f)
        {          
            panel.SetActive(false);
            
        }       
    }

    private void Update()
    {


        if (dayNightSystem2D.nightVeil == true)
        {
            ShowWarningText("Nightmare is Coming");
        }
        else
        {
            count = timeWarning;
        }
        
    }

    private void FloatText()
    {
        timeChecker += Time.deltaTime;
        if (timeChecker < BlinkFadeInTime)
        {
            warningText.color = new Color(color.r, color.g, color.b, timeChecker / BlinkFadeInTime);
        }
        else if (timeChecker < BlinkFadeInTime + BlinkStayTime)
        {
            warningText.color = new Color(color.r, color.g, color.b, 1);
        }
        else if (timeChecker < BlinkFadeInTime + BlinkStayTime + BlinkFadeOutTime)
        {
            warningText.color = new Color(color.r, color.g, color.b, 1 - (timeChecker - (BlinkFadeInTime + BlinkStayTime)) / BlinkFadeOutTime);
        }
    }
}
