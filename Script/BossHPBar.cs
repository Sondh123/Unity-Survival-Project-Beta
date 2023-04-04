using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI bossText;
    EnemiesManager enemiesManager;

    private void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
    }

    private void Update()
    {
        SetNameBoss(enemiesManager.bossName);
    }
    public void SetNameBoss(string name)
    {
        bossText.text = name.ToString();
    }


}
