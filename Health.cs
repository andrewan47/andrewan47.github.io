using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public const float baseHealth = 100.0f;
    public const float baseArmor = 100.0f;
    public const float baseShield = 100.0f;

    float currentHealth;
    float currentArmor;
    float currentShield;
    public float maxHealth;
    public float maxArmor;
    public float maxShield;

    public float debuff;

    public bool shields;
    public bool ignorArmor;
    public float shieldTime;
    public float shieldRegen;

	// Use this for initialization
	void Start () {
        shieldTime = 3.0f;
        shieldRegen = 30.0f;
        shields = false;
        ignorArmor = false;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public float MaxHealth(float modH)
    {
        maxHealth = currentHealth = modH * baseHealth;
        return currentHealth;
    }

    public float MaxArmor(float modA)
    {
        maxArmor = currentArmor = modA * baseArmor;
        return currentArmor;
    }

    public float MaxShield(float modS)
    {
        if (modS > 0)
        {
            shields = true;
        }

        maxShield = currentShield = modS * baseShield;
        return currentShield;
    }

    public float GetMaxS()
    {
        return maxShield;
    }

    public float GetMaxH()
    {
        return maxHealth;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public float GetArmor()
    {
        return currentArmor;
    }

    public float GetShield()
    {
        return currentShield;
    }

    //Damage calculation
    public float RecieveDamage(float amt)
    {
        amt *= debuff;
        float ultGained = 0f;

        //Damage goes in order Shield, Armor, Health
        if (currentShield > 0)
        {
            currentShield -= amt;
            ultGained += amt;
            if (currentShield < 0)
            {
                if (currentArmor > 0)
                {
                    currentArmor += currentShield;
                    ultGained -= currentShield;
                    if (currentArmor < 0)
                    {
                        currentHealth += currentArmor;
                        ultGained -= currentArmor;
                        currentArmor = 0;
                    }
                }
                else
                {
                    currentHealth += currentShield;
                    ultGained -= currentShield;
                    currentShield = 0;
                }
            }
        }
        else if (currentArmor > 0 && ignorArmor == false)
        {
            float reducedDamage = amt / 2;
            if (reducedDamage > 5)
            {
                currentArmor -= amt - 5;
                ultGained += amt;
            }
            else
            {
                currentArmor -= reducedDamage;
                ultGained += reducedDamage;
            }
            if (currentArmor < 0)
            {
                currentHealth += currentArmor;
                ultGained -= currentArmor;
                currentArmor = 0;
            }
        }
        else
        {
            currentHealth -= amt;
            ultGained += amt;
        }

        if (currentHealth <= 0)
            currentHealth = 0;

        shieldTime = 3.0f;

        return ultGained;
    }

    //Recover health is for health packs
    //and plan to expand it to healing from allies
    public void RecoverHealth(float amt)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += amt;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
        else if (currentArmor < maxArmor)
        {
            currentArmor += amt;
            if (currentArmor > maxArmor)
            {
                currentArmor = maxArmor;
            }
        }
        else if (currentShield < maxShield)
        {
            currentShield += amt;
            if (currentShield > maxShield)
            {
                currentShield = maxShield;
            }
        }
    }

    //Regenerates shields after a set time of no damage taken
    public void ShieldRegen()
    {
        shieldTime -= Time.deltaTime;
        if (shieldTime <= 0)
        {
            currentShield += shieldRegen * Time.deltaTime;
            if (currentShield >= maxShield)
            {
                currentShield = maxShield;
            }
        }
    }

    //Takes Buff and Debuff into damage calculations such as Mercy's damage boost and Zen's Discord.
    public void DebuffOn(float nerf)
    {
        debuff *= nerf;
    }

    public void DebuffOff(float nerf)
    {
        debuff /= nerf;
    }

    public void IgnoreArmor()
    {
        ignorArmor = true;
    }

    public void StopIgnorArmor()
    {
        ignorArmor = false;
    }
}