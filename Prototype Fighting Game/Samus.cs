using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samus : Universal
{
    CharacterController controller;
    Animator anim;
    public Stats SamusStats;
    public Jab SamusJab;
    Controls SamusControl;
    public GameObject SamusFB;

    //Character stats
    private int HP = 975;
    private int meter = 100;
    private int stun = 1000;

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

    //Attack sets which animation to play out along with assisting in getting the proper stats of the move once damage calculations are added in.
    private string attack;

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
        SamusStats = new Stats(HP, stun, meter);
        SamusControl = new Controls(speed, backSpd, jumpSpd,
                                    jabDamage, jabChip, jabStun, jabCounter, jabCounterStun, jabMeter);

        attack = "None";
    }

    // Update is called once per frame
    void Update()
    {
        //Movement();
        SamusControl.Movement(controller, anim);
        attack = SamusControl.Attack(anim, attack, dp, fb, hk, su);
        if (attack.Equals("fireball"))
        {
            Shoot();
        }
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

        if (opponent.name == "Opponent")
        {
            Debug.Log(attack);
        }
    }
}
