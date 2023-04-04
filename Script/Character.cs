using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp = 100;

    public int armor = 0;
    int hpRegenerationRate = 1;
    public float hpRegenerationTimer;
    public float speed = 2f;
    public float pickUpRange = 1.5f;
    [SerializeField] StatusBar hpBar;
    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;
    public bool isDead;
    float i = 3;
    float dem = 0.2f;

    [Header("Dissolve")]
    Material materialCharacter;
    float fade = 1f;
    Color colorFade = Color.white;
    bool isDissolving = false;
    [Header("Partical")]
    public GameObject bloodPartical;
    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }

    public void Start()
    {       
        materialCharacter = GetComponentInChildren<SpriteRenderer>().material;
        maxHp = TotalStatsCharacter.instanceCharacter.hp;
        armor = TotalStatsCharacter.instanceCharacter.armor;
        speed = TotalStatsCharacter.instanceCharacter.spd;
        currentHp = maxHp;

       
        hpBar.SetState(currentHp, maxHp);
    }
    private void Update()
    {
        materialCharacter.SetFloat("_Fade", fade);
        materialCharacter.SetColor("_Color", colorFade);
        hpRegenerationRate = level.level;
        hpRegenerationTimer -= Time.deltaTime;
        if (hpRegenerationTimer < 0f)
        {
            Heal(hpRegenerationRate);
            hpRegenerationTimer = 1.5f;
        }

        if(isDead == true)
        {
            i -= Time.deltaTime;
            currentHp = 0;
            if (i <= 0)
            {
                GetComponent<CharacterGameover>().GameOver();
            }
        }
        
        if (isDissolving == true)
        {
            fade -= Time.deltaTime;
            if (fade <= 0f)
            {
                fade = 0f;               
                isDissolving = false;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        if(isDead == true)
        {
            return;
        }
        


        ApplyArmor(ref damage);
        dem -= Time.deltaTime;
        if (dem < 0f)
        {
            currentHp -= damage;
            Instantiate(bloodPartical, transform.position, Quaternion.identity);
            dem = 0.3f;
        }
        if (currentHp <= 0)
        {
            isDissolving = true;
            FindObjectOfType<AudioManager>().Play("Player Death");                      
            isDead = true;
        }
        hpBar.SetState(currentHp, maxHp);
    }


    private void ApplyArmor(ref int damage)
     {
        damage -= armor;
       if (damage < 0)
        {
            damage = 0;
        }
     }
    public void Heal(int amount)
    {
        if (currentHp <= 0)
        {
            currentHp = 0 + amount;
        }
        currentHp += amount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        hpBar.SetState(currentHp, maxHp);
    }
}
