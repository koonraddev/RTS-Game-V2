using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float maxHealth;
    private float health;
    private float armor;
    private float magicResistance;
    private float healthPointsRegen;
    private float healthPercentsRegen;
    private float healthRegeneration;

    private float physicalDamageMultiplier;
    private float magicDamageMultiplier;

    private float interval = 5f;
    private float timeLeft;
    void Start()
    {
        GameEvents.instance.OnStatisticUpdate += UpdateStats;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0 && (health < maxHealth))
        {
            Heal(healthRegeneration/12);
            timeLeft = interval;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage("Monster",20, 0, 0);
        }
    }
    public void Damage(string enemyName,float physicalDamage, float magicDamage, float trueDamage)
    {
        float totalDamage = physicalDamage * physicalDamageMultiplier + magicDamage * magicDamageMultiplier + trueDamage;
        //Console Log
        ConsolePanel.instance.DamageLog(enemyName, totalDamage);
        health -= totalDamage;
    }


    public void UpdateStats(StatisticType statisticType, float value)
    {
        switch (statisticType)
        {
            case StatisticType.MaxHealth:
                maxHealth = value;
                break;
            case StatisticType.Armor:
                armor = value;
                physicalDamageMultiplier = 100 / (100 + armor);
                break;
            case StatisticType.MagicResistance:
                magicResistance = value;
                magicDamageMultiplier = 100 / (100 + magicResistance);
                break;
            case StatisticType.HealthPercentageRegeneration:
                healthPercentsRegen = value;
                healthRegeneration = healthPointsRegen + (maxHealth * (healthPercentsRegen / 100));
                break;
            case StatisticType.HealthPointsRegeneration:
                healthPointsRegen = value;
                healthRegeneration = healthPointsRegen + (maxHealth * (healthPercentsRegen / 100));
                break;
            default:
                break;
        }
    }

    public void Damage(float healthPoints)
    {
        health -= healthPoints;
        GameEvents.instance.UpdateCurrentHP(health);
    }

    public void Heal(float healthPoints, bool consoleLog = false)
    {
        float beforeHeal = health;
        health += healthPoints;
        health = Mathf.Clamp(health, 0, maxHealth);
        float afterHeal = health;
        float diff = afterHeal - beforeHeal;
        GameEvents.instance.UpdateCurrentHP(health);
        if (consoleLog)
        {
            ConsolePanel.instance.HealLog(diff);
        }
    }

    private void OnDisable()
    {
        GameEvents.instance.OnStatisticUpdate -= UpdateStats;
    }
}
