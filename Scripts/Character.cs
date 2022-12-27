using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp = 100;
    public int armor = 0;
    public float hpRegenerationRate = 1f;
    public float hpRegenerationTimer;
    [SerializeField] StatusBar hpBar;
    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;
    public bool isDead;
    float i = 3;

    [Header("Dissolve")]
    Material materialCharacter;
    float fade = 1f;
    Color colorFade = Color.white;
    bool isDissolving = false;

    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }

    public void Start()
    {
        hpBar.SetState(currentHp, maxHp);
        materialCharacter = GetComponentInChildren<SpriteRenderer>().material;
    }
    private void Update()
    {
        materialCharacter.SetFloat("_Fade", fade);
        materialCharacter.SetColor("_Color", colorFade);

        hpRegenerationTimer += Time.deltaTime * hpRegenerationRate;
        if (hpRegenerationTimer > 1f)
        {
            Heal(1);
            hpRegenerationTimer -= 1f;
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

        currentHp -= damage;

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
