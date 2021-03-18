using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samus : Universal
{
    CharacterController controller;
    Animator anim;
    public GameObject SamusFB;

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
    private int jabMeter = 5;
    private int jabWhiff = 0;

    //Character fireball stats
    private int fbDamage = 60;
    private int fbChip = 10;
    private int fbStun = 120;
    private int fbCounter = 66;
    private int fbCounterStun = 134;
    private int fbMeter = 15;
    private int fbWhiff = 10;

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
    }

    // Update is called once per frame
    void Update()
    {
        //Movement();
        Movement(controller, anim);
        attack = Attack(anim, attack, dp, fb, hk, su);
        if (attack.Equals("fireball"))
        {
            Shoot();
        }
        Meterbar.setMeterBarValue(getMeter(), maxMeter);
    }

    //Creates a shpere which is the characters fireball
    public void Shoot()
    {
        var fb = new Fireball();
        fb.FBStats(fbDamage, fbStun, fbMeter);
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
                    opponentHealth.calculateDamage(jabDamage);
                    opponentHealth.calculateStun(jabStun);
                    calculateMeter(jabMeter);
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
