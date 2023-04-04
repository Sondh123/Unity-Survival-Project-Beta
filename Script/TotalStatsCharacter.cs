using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TotalStatsCharacter : MonoBehaviour
{
    public static TotalStatsCharacter instanceCharacter;


    [SerializeField] DataContainer dataContainer;
    [SerializeField] PlayerPersistentUpgrades upgrade;

    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI dmgText;
    [SerializeField] TextMeshProUGUI spdText;

    [SerializeField] public int hp = 50;
    [SerializeField] public int armor = 0;
    [SerializeField] public float spd = 2f;

    private void Awake()
    {
        instanceCharacter = this;
    }
    public void UpgradeTotal(PlayerUpgrades playerUpgrades)
    {
        if(playerUpgrades == dataContainer.upgrades[0])
        {
            dataContainer.hp += dataContainer.hp / 10;
        }
        if(playerUpgrades == dataContainer.upgrades[1])
        {
            dataContainer.armor += 2;
        }
        if(playerUpgrades == dataContainer.upgrades[2])
        {
            dataContainer.moveSpeed += + 0.4f;
        }      
    }

    public void UpdateTotal()
    {
        hpText.text = dataContainer.hp.ToString();
        dmgText.text = dataContainer.armor.ToString();
        spdText.text = dataContainer.moveSpeed.ToString();
    }
    private void Update()
    {
        hp = dataContainer.hp;
        armor = dataContainer.armor;
        spd = dataContainer.moveSpeed;
    }
}
