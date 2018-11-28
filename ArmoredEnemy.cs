using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredEnemy : Health {

    float modHealth = 3f;
    float modArmor = 2f;
    float modShield = 0f;

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

        if (health <= 0)
            Die();
	}

    void Die()
    {
        Destroy(gameObject);
    }
}