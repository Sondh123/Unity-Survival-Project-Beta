using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // Start is called before the first frame update
    public int level = 1;
    int experience = 0;

    [SerializeField] ExperienceBar experienceBar;
    [SerializeField] UpgradePanelManager upgradePanel;
    ChangeColorEXP changeColorEXP;

    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> selectedUpgrades;

    [SerializeField]List<UpgradeData> acquiredUpgrades;
   
    WeaponManager weaponManager;
    PassiveItem passiveItem;

    //upgrades start
    [SerializeField] List<UpgradeData> upgradeAvailableOnStart;

    //Tier list chance
    [Header("Tier List")]
    [SerializeField] List<UpgradeData> commonTierList = new List<UpgradeData>();
    [SerializeField] List<UpgradeData> rareTierList = new List<UpgradeData>();
    [SerializeField] [Range(0f, 1f)] float chance = 0.3f;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        passiveItem = GetComponent<PassiveItem>();
    }
    int TO_LEVEL_UP
    {
        get
        {
            return level * 400;
        }
    }

    internal void AddUpgraddesIntoTheListOfAvailableUpgrades(List<UpgradeData> upgradesToAdd)
    {
        if(upgradesToAdd == null)
        {
            return;
        }
        this.upgrades.AddRange(upgradesToAdd);
        ClassifyTierUpgrades(upgradesToAdd);
    }

    private void Start()
    {
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
        experienceBar.SetLevelText(level);
        AddUpgraddesIntoTheListOfAvailableUpgrades(upgradeAvailableOnStart);
        changeColorEXP = FindObjectOfType<ChangeColorEXP>();

    }
    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
        experienceBar.UpdateExperienceSlider(experience, TO_LEVEL_UP);
    }

    public void Upgrade(int selectedUpgradeID)
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeID];

        if(acquiredUpgrades == null)
        {
            acquiredUpgrades = new List<UpgradeData>();
        }

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                weaponManager.UpgradeWeapon(upgradeData);               
                break;
            case UpgradeType.ItemUpgrade:
                passiveItem.UpgradeItem(upgradeData);
                break;
            case UpgradeType.WeaponGet:
                weaponManager.AddWeapon(upgradeData.weaponData);
                commonTierList.Remove(upgradeData);
                break;
            case UpgradeType.ItemGet:
                passiveItem.Equip(upgradeData.item);
                AddUpgraddesIntoTheListOfAvailableUpgrades(upgradeData.item.upgrades);
                commonTierList.Remove(upgradeData);
                break;
        }   
        acquiredUpgrades.Add(upgradeData);       
        if(upgradeData.upgradeTier == UpgradeTier.Common)
        {
            commonTierList.Remove(upgradeData);
        }
        else
        {
            rareTierList.Remove(upgradeData);
        }

        upgrades.Remove(upgradeData);
    }


    public void CheckLevelUp()
    {
        if(experience >= TO_LEVEL_UP)
        {
            FindObjectOfType<AudioManager>().Play("Level Up");
            LevelUp();
        }
    }

    public void ClassifyTierUpgrades(List<UpgradeData> listUpgrade)
    {      
        for (int i = 0; i < listUpgrade.Count; i++)
        {
            switch (listUpgrade[i].upgradeTier)
            {
                case UpgradeTier.Common:
                    commonTierList.Add(listUpgrade[i]);
                    break;
                case UpgradeTier.Rare:
                    rareTierList.Add(listUpgrade[i]);
                    break;

            }
        }
    }

    public void LevelUp()
    {
        if (selectedUpgrades == null)
        {
            selectedUpgrades = new List<UpgradeData>();
        }
        selectedUpgrades.Clear();
        changeColorEXP.EXPBarFill();
        selectedUpgrades.AddRange(GetUpgrades(3));
        upgradePanel.OpenPanel(selectedUpgrades);
        experience -= TO_LEVEL_UP;
        level += 1;
        
        experienceBar.SetLevelText(level);
    }
    public void ShuffleUpgrades(List<UpgradeData> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int x = Random.Range(0, i + 1);
            UpgradeData shuffleElement = list[i];
            list[i] = list[x];
            list[x] = shuffleElement;
        }
    }
    public List<UpgradeData> GetUpgrades(int count)
    {
        ShuffleUpgrades(commonTierList);
        ShuffleUpgrades(rareTierList);
        List<UpgradeData> upgradesList = new List<UpgradeData>();

        if (count > upgrades.Count)
        {
            count = upgrades.Count;
        }

        if(Random.value > chance)
        {
            for (int i = 0; i < count; i++)
            {
                //upgradesList.Add(commonTierList[Random.Range(0, commonTierList.Count)]);
                upgradesList.Add(commonTierList[i]);
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                //upgradesList.Add(rareTierList[Random.Range(0, rareTierList.Count)]);
                upgradesList.Add(rareTierList[i]);
            }
        }

        return upgradesList;
    }
}
