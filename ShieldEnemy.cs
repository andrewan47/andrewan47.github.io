using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : Health {

    float modHealth = 1f;
    float modArmor = 0f;
    float modShield = 1.5f;

    public float health;
    public float armor;
    public float shield;

    // Use this for initialization
    void Start () {
        health = MaxHealth(modHealth);
        armor = MaxArmor(modArmor);
        shield = MaxShield(modShield);
    }
	
	// Update is called once per frame
	void Update () {
        health = GetHealth();
        armor = GetArmor();
        shield = GetShield();
	}

    void Die()
    {
        Destroy(gameObject);
    }
}