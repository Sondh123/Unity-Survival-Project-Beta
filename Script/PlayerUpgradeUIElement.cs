using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerUpgradeUIElement : MonoBehaviour
{
    TotalStatsCharacter totalStatsCharacter;
    [SerializeField] PlayerPersistentUpgrades upgrade;

    [SerializeField] TextMeshProUGUI upgradeName;
    [SerializeField] TextMeshProUGUI level;
    
    [SerializeField] TextMeshProUGUI price;

    [SerializeField] DataContainer dataContainer;
    private void Start()
    {
        totalStatsCharacter = FindObjectOfType<TotalStatsCharacter>();
        UpdateElement();
        totalStatsCharacter.UpdateTotal();
    }
    public void Upgrade()
    {
        PlayerUpgrades playerUpgrades = dataContainer.upgrades[(int)upgrade];

        if(dataContainer.coins >= playerUpgrades.costToUpgrade)
        {
            dataContainer.coins -= playerUpgrades.costToUpgrade;
            playerUpgrades.level += 1;
            playerUpgrades.costToUpgrade = playerUpgrades.costToUpgrade + 10 * playerUpgrades.level;
            totalStatsCharacter.UpgradeTotal(playerUpgrades);
            totalStatsCharacter.UpdateTotal();
            UpdateElement();
        }
    }
    public void UpdateElement()
    {
        PlayerUpgrades playerUpgrades = dataContainer.upgrades[(int)upgrade];

        upgradeName.text = upgrade.ToString();
        level.text = playerUpgrades.level.ToString();
        price.text = playerUpgrades.costToUpgrade.ToString();
       
    }
}
