using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Health {

    float modHealth = 2.5f;
    float modArmor = 0f;
    float modShield = 0f;

    public float health;
    public float armor;
    public float shield;

    public float pause;
    public float fireRate;
    public float burst;

    public GameObject bullet_prefabs;
    public float bulletImpulse;

    // Use this for initialization
    void Start () {
        health = MaxHealth(modHealth);
        armor = MaxArmor(modArmor);
        shield = MaxShield(modShield);

        pause = 10f;
        fireRate = 0.5f;
        burst = 6;

        bulletImpulse = 5f;
    }
	
	// Update is called once per frame
	void Update () {
        health = GetHealth();
        armor = GetArmor();
        shield = GetShield();

        if (health <= 0)
            Die();

        if (burst > 0 && pause == 10)
        {
            fireRate -= Time.deltaTime;
            if (fireRate <= 0)
            {
                shoot();
                fireRate = 0.5f;
            }
        }
        else
        {
            pause -= Time.deltaTime;
            if (pause <= 0)
            {
                burst = 6;
                pause = 10;
            }
        }
    }

    //Enemy fires projectile weapons at a set time
    public void shoot()
    {
        burst -= 1;
        GameObject theBullet = (GameObject)Instantiate(bullet_prefabs, this.transform.position + this.transform.forward + this.transform.up + this.transform.up / 3, this.transform.rotation);
        Rigidbody rb = theBullet.GetComponent<Rigidbody>();
        rb.AddForce(this.transform.forward * bulletImpulse, ForceMode.Impulse);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}