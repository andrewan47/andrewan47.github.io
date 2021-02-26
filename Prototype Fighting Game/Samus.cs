using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samus : Universal
{
    CharacterController controller;
    Animator anim;
    public Stats SamusStats = new Stats(975, 1000, 100, 4, 3, 7);
    public Jab SamusJab = new Jab(30, 0, 70, 36, 84, 5);
    public GameObject SamusFB;

    private int dpc;
    private int fbc;
    private int hkc;
    private int suc;
    private int pointer;
    private int direction;
    private int prevDirec;
    private int size;

    private float jump;
    private float walk;
    private float popCounter;

    public int HP;
    public int Stun;
    public int Meter;

    //Each array holds the input command necessary to activate a special move.
    //Might be switched to an enum instead of an array
    int[] fb = new int[] { 2, 3, 6 };
    int[] hk = new int[] { 2, 1, 4 };
    int[] dp = new int[] { 6, 2, 3 };
    int[] su = new int[] { 2, 3, 6, 2, 3, 6 };

    List<int> inputList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        popCounter = 0.2f;
        inputList.Add(0);
        size = inputList.Count;
        HP = SamusStats.getHP();
        Stun = 0;
        Meter = 0;

        dpc = 0;
        fbc = 0;
        hkc = 0;
        suc = 0;
        pointer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Attack();
    }

    public void Movement()
    {
        //Sets the directional inputs to an interger value
        if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") > 0)
        {
            prevDirec = direction;
            direction = 9;
        }
        else if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") < 0)
        {
            prevDirec = direction;
            direction = 7;
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") > 0)
        {
            prevDirec = direction;
            direction = 3;
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") < 0)
        {
            prevDirec = direction;
            direction = 1;
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            prevDirec = direction;
            direction = 8;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            prevDirec = direction;
            direction = 2;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            prevDirec = direction;
            direction = 6;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            prevDirec = direction;
            direction = 4;
        }
        else
        {
            prevDirec = direction;
            direction = 0;
        }

        if (controller.isGrounded && Input.GetAxisRaw("Vertical") > 0)
        {
            jump = SamusStats.getJumpSpeed();
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            SamusStats.setBlocking(true);
        }
        else
        {
            SamusStats.setBlocking(false);
        }

        if (controller.isGrounded && Input.GetAxisRaw("Vertical") < 0)
        {
            SamusStats.setCrouching(true);
        }
        else
        {
            SamusStats.setCrouching(false);
        }

        walk = Input.GetAxis("Horizontal") * SamusStats.getSpeed();
        jump += Physics.gravity.y * Time.deltaTime;
        Vector3 speed = new Vector3(walk, jump, 0);
        controller.Move(speed * Time.deltaTime);

        AddToInput();

        //Removes the first item in the list after a set amount of time
        popCounter -= Time.deltaTime;
        if (popCounter <= 0)
        {
            if (size > 0)
            {
                inputList.RemoveAt(0);
            }
            popCounter = 0.2f;
        }

        anim.SetFloat("Speed", walk);
    }

    //Adds the directional input into a list
    void AddToInput()
    {
        size = inputList.Count;

        if (size > 0)
        {
            if (inputList[size - 1] != direction)
            {
                inputList.Add(prevDirec);
                inputList.Add(direction);
            }
        }
        else
        {
            inputList.Add(direction);
        }
    }

    public void Attack()
    {
        //Throw command
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            //This was set to quickly reset the booleans

            //Start animation for throw
        }
        //Jab command
        else if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            InputPunchCommands();

            //resets the pointers
            dpc = 0;
            fbc = 0;
            pointer = 0;
        }
        //Light kick command
        else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            //InputKickCommands();
        }
        //Possible inclusion. After attack automatically clear the list but might not be a necessary inclusion though.
    }

    //Checks the inputs before determining which attack animation to play
    void InputPunchCommands()
    {
        if (inputList[pointer] == dp[dpc])
        {
            dpc++;
        }
        if (inputList[pointer] == fb[fbc])
        {
            fbc++;
        }

        pointer++;

        if (dpc == 3)
        {
            return;
        }
        else if (fbc == 3)
        {
            anim.SetTrigger("FBMotion");
            Shoot();
            return;
        }
        else if (pointer >= size)
        {
            anim.SetTrigger("Jab");
            return;
        }

        InputPunchCommands();
    }

    //Creates a shpere which is the characters fireball
    public void Shoot()
    {
        GameObject FireBall = (GameObject)Instantiate(SamusFB, this.transform.position + this.transform.forward + this.transform.up, this.transform.rotation);
        Rigidbody rb = FireBall.GetComponent<Rigidbody>();
        rb.AddForce(this.transform.forward * 5, ForceMode.Impulse);
    }

    //Hit detection for the player's attack
    private void OnCollisionEnter(Collision collision)
    {
        GameObject opponent = collision.gameObject;

        if (opponent.name == "Opponent")
        {
            Debug.Log("Hit the opponent");
        }
    }
}
