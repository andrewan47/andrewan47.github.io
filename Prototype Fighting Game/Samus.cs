using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samus : Universal
{
    CharacterController controller;
    Animator anim;
    public GameObject SamusFB;
    public int Combo;

    //Character stats
    private int maxHP = 975;
    private int maxMeter = 100;
    private int maxStun = 1000;

    //Character mobility stats
    private float speed = 4.0f;
    private float backSpd = 3.0f ;
    private int jumpSpd = 7;

    //Character light punch
    private int jabDamage = 30;
    private int jabChip = 0;
    private int jabStun = 70;
    private int jabCounter = 36;
    private int jabCounterStun = 84;
    private int jabMeter = 20;
    private int jabWhiff = 0;
    private int jabOnHit = 60;
    private int jabAtk = 15;
    //OnHit = 14

    //Character medium punch
    private int MPDamage = 60;
    private int MPChip = 0;
    private int MPStun = 100;
    private int MPCounter = 66;
    private int MPCounterStun = 114;
    private int MPMeter = 30;
    private int MPWhiff = 0;
    private int MPOnHit = 23;
    private int MPAtk = 23;

    //Character hard punch
    private int HPDamage = 80;
    private int HPChip = 0;
    private int HPStun = 150;
    private int HPCounter = 86;
    private int HPCounterStun = 164;
    private int HPMeter = 50;
    private int HPWhiff = 0;
    private int HPOnHit = 26;
    private int HPAtk = 34;

    //Character light kick
    private int kickDamage = 40;
    private int kickChip = 0;
    private int kickStun = 70;
    private int kickCounter = 46;
    private int kickCounterStun = 84;
    private int kickMeter = 20;
    private int kickWhiff = 0;
    private int kickOnHit = 5;
    private int kickAtk = 18;
    //OnHit = 15

    //Character medium kick
    private int MKDamage = 60;
    private int MKChip = 0;
    private int MKStun = 100;
    private int MKCounter = 66;
    private int MKCounterStun = 114;
    private int MKMeter = 30;
    private int MKWhiff = 0;
    private int MKOnHit = 20;
    private int MKAtk = 25;

    //Character hard kick
    private int HKDamage = 80;
    private int HKChip = 0;
    private int HKStun = 150;
    private int HKCounter = 86;
    private int HKCounterStun = 164;
    private int HKMeter = 50;
    private int HKWhiff = 0;
    private int HKOnHit = 26;
    private int HKAtk = 37;

    //Attack sets which animation to play out along with assisting in getting the proper stats of the move once damage calculations are added in.
    public string attack;

    //Each array holds the input command necessary to activate a special move.
    //Might be switched to an enum instead of an array
    int[] fb = new int[] { 2, 3, 6, 0, 15, 60, 120, 15 };
    int[] hk = new int[] { 2, 1, 4, 10, 18, 90, 150, 40 };
    int[] dp = new int[] { 6, 2, 3, 0, 18, 120, 150, 50 };
    int[] su = new int[] { 2, 3, 6, 2, 3, 6, 0, 15, 340 };

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        Stats(maxHP, maxStun, maxMeter, speed, backSpd, jumpSpd);

        attack = "None";
        Combo = getCombo();
    }

    // Update is called once per frame
    void Update()
    {
        Combo = getCombo();
        Movement(controller, anim);
        attack = Attack(anim, attack, dp, fb, hk, su, jabAtk, MPAtk, HPAtk, kickAtk, MKAtk, HKAtk);
        if (attack.Equals("fireball"))
        {
            Shoot();
        }
     
        Meterbar.setMeterBarValue(getMeter(), maxMeter);
    }

    //Creates a shpere which is the characters fireball
    public void Shoot()
    {
        GameObject FireBall = (GameObject)Instantiate(SamusFB, this.transform.position + this.transform.forward + this.transform.up, this.transform.rotation);
        Rigidbody rb = FireBall.GetComponent<Rigidbody>();
        rb.AddForce(this.transform.forward * 5, ForceMode.Impulse);
        attack = "None";
    }

    //Hit detection for the player's attack
    private void OnCollisionEnter(Collision collision)
    {
        GameObject opponent = collision.gameObject;

        Universal opponentHealth = opponent.GetComponent<Universal>();

        if (opponent.name == "Opponent")
        {
            //Switch case to determine which attack and damage variables to call.
            switch (attack)
            {
                case "Jab":
                    calculateCombo(opponentHealth.getHitStun(), jabOnHit);
                    opponentHealth.calculateDamage(jabDamage);
                    opponentHealth.calculateStun(jabStun);
                    calculateMeter(jabMeter);
                    break;
                case "MP":
                    calculateCombo(opponentHealth.getHitStun(), MPOnHit);
                    opponentHealth.calculateDamage(MPDamage);
                    opponentHealth.calculateStun(MPStun);
                    calculateMeter(MPMeter);
                    break;
                case "HP":
                    calculateCombo(opponentHealth.getHitStun(), HPOnHit);
                    opponentHealth.calculateDamage(HPDamage);
                    opponentHealth.calculateStun(HPStun);
                    calculateMeter(HPMeter);
                    break;
                case "Kick":
                    calculateCombo(opponentHealth.getHitStun(), kickOnHit);
                    opponentHealth.calculateDamage(kickDamage);
                    opponentHealth.calculateStun(kickStun);
                    calculateMeter(kickMeter);
                    break;
                case "MK":
                    calculateCombo(opponentHealth.getHitStun(), MKOnHit);
                    opponentHealth.calculateDamage(MKDamage);
                    opponentHealth.calculateStun(MKStun);
                    calculateMeter(MKMeter);
                    break;
                case "HK":
                    calculateCombo(opponentHealth.getHitStun(), HKOnHit);
                    opponentHealth.calculateDamage(HKDamage);
                    opponentHealth.calculateStun(HKStun);
                    calculateMeter(HKMeter);
                    break;
                case "grab":
                    Debug.Log(attack);
                    break;
                default:
                    break;
            }

        }
    }
}
