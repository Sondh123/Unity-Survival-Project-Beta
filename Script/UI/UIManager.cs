using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    [HeaderAttribute ("MAIN PANEL")]
    [SerializeField] RectTransform mainMenu;
    [SerializeField] RectTransform selectMenu;
    [SerializeField] RectTransform optionMenu;
    [SerializeField] RectTransform creditMenu;

    [HeaderAttribute("Arrow")]
    [SerializeField] GameObject arrowLeft;
    [SerializeField] GameObject arrowRight;
    [HeaderAttribute("MAP PANEL")]
    [SerializeField] List<RectTransform> map ;


    private int i = 0;
    private void Start()
    {
        mainMenu.DOAnchorPos(Vector2.zero, 0.25f);

    }
    public void SelectMenuButton()
    {
        mainMenu.DOAnchorPos(new Vector2(-1600, 0), 0.25f);
        selectMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
    }
    public void CloseSelectMenuButton()
    {
        mainMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
        selectMenu.DOAnchorPos(new Vector2(0, 927), 0.25f);
    }
    public void OptionMenuButton()
    {
        mainMenu.DOAnchorPos(new Vector2(-1600, 0), 0.25f);
        optionMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
    }
    public void CloseOptionMenuButton()
    {
        mainMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
        optionMenu.DOAnchorPos(new Vector2(1600, 0), 0.25f);
    }
    public void CreditButton()
    {
        mainMenu.DOAnchorPos(new Vector2(-1600, 0), 0.25f);
        creditMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
    }
    public void CloseCreditButton()
    {
        mainMenu.DOAnchorPos(new Vector2(0, 0), 0.25f);
        creditMenu.DOAnchorPos(new Vector2(0, -927), 0.25f);
    }

    public void NextButton()
    {
        map[i + 1].DOAnchorPos(new Vector2(0, 0), 0.25f);
        map[i].DOAnchorPos(new Vector2(-1000, 0), 0.25f);
        i += 1;
    }
    public void PreviousButton()
    {
        map[i - 1].DOAnchorPos(new Vector2(0, 0), 0.25f);
        map[i].DOAnchorPos(new Vector2(1000, 0), 0.25f);
        i -= 1;
    }

    private void Update()
    {
        if(i == 0)
        {
            arrowLeft.SetActive(false);
        }
        else
        {
            arrowLeft.SetActive(true);
        }

        if(i == map.Count-1)
        {
            arrowRight.SetActive(false);
        }
        else
        {
            arrowRight.SetActive(true);
        }
    }
}
