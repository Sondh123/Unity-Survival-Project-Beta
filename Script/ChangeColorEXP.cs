using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorEXP : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color color;

    Level level;

    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<Level>();
        color.g = 255;
        image.color = color;
    }

    // Update is called once per frame
    public void EXPBarFill()
    {
        color.g = -1;

    }
    private void Update()
    {
        image.color = color;
    }
}
