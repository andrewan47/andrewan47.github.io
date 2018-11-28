using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHero : Health
{
    float movementSpeed;
    float mouseSensitivity;
    float upDownRange;
    float jumpSpeed;
    float verticalVelocity;
    float verticalRotation;

    float modHealth;
    float modArmor;
    float modShield;

    public float health;
    public float armor;
    public float shield;

    public float damage;
    public float buff;
    public float marksman;
    public float shotCooldown;
    public int burst;
    public int hitCounter;
    public float burstCooldown;
    public float durationPassive;

    public float abilityRange;
    public float abilityCooldown;
    public bool abilityOne;

    public float durationOne;
    public float armorDebuff;
    public float shieldDamage;
    public float shieldDPS;
    public bool armorAbility;
    public bool shieldAbility;

    public float ultCharge;
    public float ultStatus;

    private float forwardSpeed;
    private float sideSpeed;

    Health enemy;

    CharacterController characterController;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();

        movementSpeed = 12.0f;
        mouseSensitivity = 3.0f;
        upDownRange = 60.0f;
        jumpSpeed = 7.0f;
        verticalVelocity = 0f;
        verticalRotation = 0f;

        modHealth = 2.0f;
        modArmor = 0f;
        modShield = 0f;
        health = MaxHealth(modHealth);
        armor = MaxArmor(modArmor);
        shield = MaxShield(modShield);

        burstCooldown = 0.5f;
        abilityRange = 10f;
        abilityCooldown = 10f;
        abilityOne = true;
        damage = 12f;
        buff = 1f;
        marksman = 0.2f;
        ultCharge = 1250f;
        ultStatus = 0f;

        durationOne = 6f;
        armorDebuff = 1.3f;
        shieldDamage = 20f;
        shieldDPS = 5f;
        armorAbility = false;
        shieldAbility = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
        AbilityOne();
        Ultimate();
        AbilityOneUsage();
        OneCooldown();
        PassiveCooldown();

        health = GetHealth();
        armor = GetArmor();
        shield = GetShield();

        ultStatus += 5 * Time.deltaTime;
        if (ultStatus >= ultCharge)
            ultStatus = ultCharge;

        if (health <= 0)
            Die();
    }

    void Movement()
    {
        //Rotation
        float rotLR = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLR, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        //Movement
        forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded && Input.GetButton("Jump"))
        {
            verticalVelocity = jumpSpeed;
        }

        Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);

        speed = transform.rotation * speed;

        characterController.Move(speed * Time.deltaTime);
    }

    //Hit Scan Shooting
    void Shooting()
    {
        //Mouse 1 down makes you fire a burst of three shots one after the other.
        //total of three shots fire when you press mouse 1. that's what burst represents 
        if (Input.GetButtonDown("Fire1") && shotCooldown <= 0)
        {
            //sets the cooldown so you can't fire right away.
            shotCooldown = 2.0f;
            burst -= 1;
            hitCounter = 0;

            //hit the target you shoot at instantly
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo;

            //if contact is made
            if (Physics.Raycast(ray, out hitInfo))
            {
                //getting information on what you shot
                Vector3 hitPoint = hitInfo.point;
                GameObject go = hitInfo.collider.gameObject;
                Debug.Log("Hit Object: " + go.name);

                //if enemy is damaged counter to the passive ability goes up and applies appropriate damage
                Health enemyHealth = go.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    hitCounter++;
                    if (hitInfo.collider is SphereCollider)
                        ultStatus += enemyHealth.RecieveDamage(damage * 2 * buff);
                    else
                        ultStatus += enemyHealth.RecieveDamage(damage * buff);
                }
            }
        }

        //timer for the cooldowns start to go down.
        if (shotCooldown > 0)
        {
            shotCooldown -= Time.deltaTime;
            burstCooldown -= Time.deltaTime;
            
            //fires after each half second
            if (burstCooldown <= 0 && burst > 0)
            {
                Bursts();
                burstCooldown = 0.5f;
            }
        }

        //resets cooldowns for battlerifle 
        else if (shotCooldown < 0)
        {
            shotCooldown = 0;
            burst = 3;
            burstCooldown = 0.5f;
        }
    }

    //Battle Rifle Burst Fire
    void Bursts()
    {
        //keeps track of how many shots were fired
        burst -= 1;

        //shoots directly at where you are aiming at
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hitInfo;

        //if contact is made
        if (Physics.Raycast(ray, out hitInfo))
        {
            Vector3 hitPoint = hitInfo.point;
            GameObject go = hitInfo.collider.gameObject;
            Debug.Log("Hit Object: " + go.name);

            //if enemy is damaged counter to the passive ability goes up and applies appropriate damage
            Health enemyHealth = go.GetComponent<Health>();
            if (enemyHealth != null)
            {
                hitCounter++;
                if (hitInfo.collider is SphereCollider)
                    ultStatus += enemyHealth.RecieveDamage(damage * 2 * buff);
                else
                    ultStatus += enemyHealth.RecieveDamage(damage * buff);
                if (hitCounter == 3)
                    Marksman();
            }
        }
    }

    //Passive Ability Called when all of your three shots land
    void Marksman()
    {
        buff += marksman;
        durationPassive = 15f;
    }

    //resets marksman buff
    void MarksmanOff()
    {
        buff = 1f;
    }

    //duration for passive ability marksman
    void PassiveCooldown()
    {
        if (durationPassive > 0)
        {
            durationPassive -= Time.deltaTime;
            if (durationPassive <= 0)
                MarksmanOff();
        }
    }

    //E Ability armor and shield damage
    void AbilityOne()
    {
        //shoots the ability when E key is pressed down and its not on cooldown
        if (Input.GetKeyDown(KeyCode.E) && abilityOne == true)
        {
            
            //intsantly hits the enemy with the ability
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, abilityRange))
            {
                Vector3 hitPoint = hitInfo.point;
                GameObject go = hitInfo.collider.gameObject;
                Debug.Log("Hit Object: " + go.name);

                Health enemyHealth = go.GetComponent<Health>();

                //applies the proper ability depending on enemy health
                if (enemyHealth != null)
                {
                    //checks if the target has shields
                    if (enemyHealth.GetShield() > 0)
                    {
                        abilityOne = false;
                        enemy = enemyHealth;
                        ultStatus += enemy.RecieveDamage(shieldDamage);
                        shieldAbility = true;
                    }

                    //checks if the target has armor
                    else if (enemyHealth.GetArmor() > 0)
                    {
                        abilityOne = false;
                        enemy = enemyHealth;
                        enemy.DebuffOn(armorDebuff);
                        enemy.IgnoreArmor();
                        armorAbility = true;
                    }
                }
            }
        }
    }

    void AbilityOneUsage()
    {
        //checks which ability is active shields or armor and sets it
        if (durationOne > 0 && armorAbility == true || shieldAbility == true)
        {
            //duration of both abilities
            durationOne -= Time.deltaTime;
            //if shield ability applies it does damage for set amount of time around a radius
            if (shieldAbility == true && durationOne > 0)
            {
                Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, 10.0f);
                foreach (Collider col in hitColliders)
                {
                    Health opponent = col.GetComponent<Health>();
                    if (col.name != "Player")
                    {
                        ultStatus += opponent.RecieveDamage(shieldDPS * Time.deltaTime);
                    }
                    else
                        Debug.Log("No self damage.");
                }
            }
        }

        //resets ability one
        if (armorAbility == true && durationOne <= 0)
        {
            enemy.DebuffOff(armorDebuff);
            enemy.StopIgnorArmor();
            armorAbility = false;
            durationOne = 6;
        }
        //resets ability one
        else if (shieldAbility == true && durationOne <= 0)
        {
            shieldAbility = false;
            durationOne = 6;
        }
    }

    //cooldown timer for E ability
    void OneCooldown()
    {
        if (abilityOne == false)
        {
            abilityCooldown -= Time.deltaTime;
            if (abilityCooldown <= 0)
            {
                abilityOne = true;
                abilityCooldown = 10;
            }
        }
    }

    //ultimate called when pressed Q key and ult charge is maxed
    void Ultimate()
    {
        if (Input.GetKeyDown(KeyCode.Q) && ultStatus == ultCharge)
        {
            Debug.Log("Ult Activated");
            ultStatus = 0;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}