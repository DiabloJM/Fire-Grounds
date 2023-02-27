using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class StoreManager : MonoBehaviour
{
    //Public Variables
    public int storeCoins;
    public int health;
    public int maxHealth;
    public float speed;
    public float damage;

    [Header("Health Properties")]
    public GameObject[] healthStacks;
    public int healthIndex = 2;
    public Text healthCoinText;

    [Header("Speed Properties")]
    public GameObject[] speedStacks;
    public int speedIndex = 1;
    public Text speedCoinText;

    [Header("Damage Properties")]
    public GameObject[] damageStacks;
    public int damageIndex = 2;
    public Text damageCoinText;

    [Header("Cards")]
    public GameObject[] cards;

    [Header("UI Elements")]
    public Text coinsText;
    public Text healthText;
    public Text maxHealthText;

    //Private Variables
    private int healthPrice = 35;
    private int speedPrice = 15;
    private int damagePrice = 35;

    private PlayerController player;
    private GameManager gameManager;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        OpenStore();
    }

    public void FirstAid(int price)
    {
        if(storeCoins >= price)
        {
            storeCoins -= price;
            health += 15;

            if(health > maxHealth)
            {
                health = maxHealth;
            }

            healthText.text = health.ToString();
            coinsText.text = storeCoins.ToString();
        }
    }

    public void IncreaseHealth()
    {
        if(storeCoins >= healthPrice && healthIndex < 4)
        {
            storeCoins -= healthPrice;

            int amountoFHealth = maxHealth - health;
            maxHealth += 25;
            health = maxHealth - amountoFHealth;

            healthStacks[healthIndex].gameObject.SetActive(true);
            healthIndex++;

            if(healthPrice == 15)
            {
                healthPrice = 35;
            }
            else
            {
                healthPrice = 50;
            }

            coinsText.text = storeCoins.ToString();
            healthText.text = health.ToString();
            maxHealthText.text = maxHealth.ToString();
            healthCoinText.text = healthPrice.ToString();
        }
    }

    public void IncreaseSpeed()
    {
        if(storeCoins >= speedPrice && speedIndex < 4)
        {
            storeCoins -= speedPrice;

            speed += 1.0f;
            speedStacks[speedIndex].gameObject.SetActive(true);
            speedIndex++;

            if(speedPrice == 15)
            {
                speedPrice = 35;
            }
            else
            {
                speedPrice = 50;
            }

            coinsText.text = storeCoins.ToString();
            speedCoinText.text = speedPrice.ToString();
        }
    }

    public void IncreaseDamage()
    {
        if (storeCoins >= damagePrice && damageIndex < 4)
        {
            storeCoins -= damagePrice;

            damage += 0.5f;
            damageStacks[damageIndex].gameObject.SetActive(true);
            damageIndex++;

            if (damagePrice == 15)
            {
                damagePrice = 35;
            }
            else
            {
                damagePrice = 50;
            }

            coinsText.text = storeCoins.ToString();
            damageCoinText.text = damagePrice.ToString();
        }
    }

    public void DamageCard()
    {
        if(storeCoins >= 50)
        {
            storeCoins -= 50;
            player.damageMultiplayer = 1.5f;
            cards[1].SetActive(false);

            coinsText.text = storeCoins.ToString();
        }
    }

    public void RecoilCard()
    {
        if (storeCoins >= 50)
        {
            storeCoins -= 50;
            player.bulletRecoil -= 0.1f;
            cards[0].SetActive(false);

            coinsText.text = storeCoins.ToString();
        }
    }

    public void CloseStore()
    {
        player.health = health;
        player.maxHealth = maxHealth;
        player.speed = speed;
        player.bulletDamage = damage;
        gameManager.coins = storeCoins;
        player.UpdateHealthText();
        gameManager.NextRound();
    }

    public void OpenStore()
    {
        health = player.health;
        maxHealth = player.maxHealth;
        speed = player.speed;
        damage = player.bulletDamage;
        storeCoins = gameManager.coins;

        healthText.text = health.ToString();
        maxHealthText.text = maxHealth.ToString();
        coinsText.text = storeCoins.ToString();
    }
}
