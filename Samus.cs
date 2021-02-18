using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samus : Universal
{
    CharacterController characterController;
    public Stats SamusStats = new Stats(975, 1000, 100, 4, 3, 7, 4.0f / 60.0f);
    public Jab LightPunch = new Jab(30, 0, 70, 36, 84, 5, 4.0f / 60.0f, 3.0f / 60.0f, 9.0f / 60.0f, 14.0f / 60.0f, 11.0f / 60.0f);

    private int direction;
    private int prevDirec;
    private int pointer;
    private int dpc;
    private int fbc;
    private int hkc;
    private float popCounter;
    private float walk;
    private float verticalVelocity;
    private float jump;

    //Displayed until GUI is properly set
    public int HP;
    public int Stun;
    public int Meter;

    //Saved inputs
    List<int> inputList = new List<int>();

    //Possible ways to store inputcommands such as fireball motion
    int[] fb = new int[] { 2, 3, 6 };
    int[] hk = new int[] { 2, 1, 4 };
    int[] dp = new int[] { 6, 2, 3 };

    public int num;

    //Display info for testing purposes
    public bool blocking;
    public bool crouching;
    public bool jumping;
    public bool grab;
    public bool attacking;
    public bool fbMotion;
    public bool dpMotion;
    public bool hkMotion;
    public float jabFrames;
    public float start;
    public float active;
    public float recover;
    
    //Possibly put special moves in an enum or list

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        HP = SamusStats.health;
        Stun = 0;
        Meter = 0;
        direction = 0;
        prevDirec = 0;
        dpc = 0;
        fbc = 0;
        hkc = 0;
        pointer = 0;
        popCounter = 0.2f;
        inputList.Add(0);

        //Displayed for testing purposes will be removed once certain animations are added
        grab = false;
        fbMotion = false;
        dpMotion = false;
        hkMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Attack();

        //Displayed for testing purposes and will be removed once certain animations are added
        blocking = SamusStats.isBlocking;
        crouching = SamusStats.isCrouching;
        jumping = SamusStats.isJumping;
        attacking = SamusStats.isAttacking;
        jabFrames = SamusStats.getRecovery();
        start = SamusStats.start;
        active = SamusStats.active;
        recover = SamusStats.reset;
    }

    void Movement()
    {
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
        else if(Input.GetAxisRaw("Vertical") < 0)
        {
            prevDirec = direction;
            direction = 2;
        }
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            prevDirec = direction;
            direction = 6;
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            prevDirec = direction;
            direction = 4;
        }
        else
        {
            prevDirec = direction;
            direction = 0;
        }

        if (characterController.isGrounded && Input.GetAxisRaw("Vertical") > 0 && SamusStats.isJumping == false)
        {
            jump = SamusStats.jumpRecovery;
            SamusStats.setJumping(true);
        }

        if (SamusStats.isJumping)
        {
            jump -= Time.deltaTime;
            if (jump <= 0)
            {
                verticalVelocity = SamusStats.jumpSpeed;
                SamusStats.setJumping(false);
            }
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            SamusStats.setBlock(true);
        }
        else
        {
            SamusStats.setBlock(false);
        }

        if (characterController.isGrounded && Input.GetAxisRaw("Vertical") < 0)
        {
            SamusStats.setCrouching(true);
        }
        else
        {
            SamusStats.setCrouching(false);
        }

        walk = Input.GetAxis("Horizontal") * SamusStats.getSpeed();
        verticalVelocity += Physics.gravity.y * Time.deltaTime;
        Vector3 speed = new Vector3(walk, verticalVelocity, 0);
        characterController.Move(speed * Time.deltaTime);

        AddToInput();
        popCounter -= Time.deltaTime;
        if (popCounter <= 0)
        {
            if (num > 0)
            {
                inputList.RemoveAt(0);
            }
            popCounter = 0.5f;
        }
    }

    void Attack()
    {
        //Throw command
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) && Input.GetKeyDown(KeyCode.Joystick1Button1) && SamusStats.isAttacking == false)
        {
            grab = true;

            //This was set to quickly reset the booleans
            fbMotion = false;
            dpMotion = false;
            hkMotion = false;

            //Start animation for throw
        }
        //Jab command
        else if (Input.GetKeyDown(KeyCode.Joystick1Button0) && SamusStats.isAttacking == false)
        {
            SamusStats.setRecovery(LightPunch.startUp + LightPunch.active + LightPunch.recovery);
            SamusStats.setStart(LightPunch.startUp);
            SamusStats.setActive(LightPunch.active);
            SamusStats.setReset(LightPunch.recovery);
            InputPunchCommands();

            dpc = 0;
            fbc = 0;
            pointer = 0;

            //Start animation for normal/special attacks
        }
        //Light kick command
        else if (Input.GetKeyDown(KeyCode.Joystick1Button1) && SamusStats.isAttacking == false)
        {
            InputKickCommands();

            hkc = 0;
            pointer = 0;

            //Start animation for normal/special attacks
        }
        //Possible inclusion. After attack automatically clear the list but might not be a necessary inclusion though.

        if(SamusStats.isAttacking)
        {
            if(SamusStats.active <= 0)
            {
                SamusStats.reset -= Time.deltaTime;
                if(SamusStats.reset <= 0)
                {
                    SamusStats.setAttacking(false);
                    //end animation here
                }
            }
            else if(SamusStats.start <= 0)
            {
                SamusStats.active -= Time.deltaTime;
                //Activate hit box during this time duration
            }
            else
            {
                SamusStats.start -= Time.deltaTime;
            }
        }
    }

    void AddToInput()
    {
        num = inputList.Count;

        if (num > 0)
        {
            if (inputList[num - 1] != direction)
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

    void InputPunchCommands()
    {
        if (inputList[pointer] == dp[dpc])
        {
            dpc++;
        }
        else if (inputList[pointer] == fb[fbc])
        {
            fbc++;
        }

        if (dpc == 3)
        {
            dpMotion = true;
            return;
        }
        else if (fbc == 3)
        {
            fbMotion = true;
            return;
        }
        else if (pointer == num)
        {
            return;
        }

        pointer++;
        InputPunchCommands();
    }

    void InputKickCommands()
    {
        if (inputList[pointer] == hk[hkc])
        {
            hkc++;
        }

        if (hkc == 3)
        {
            hkMotion = true;
            return;
        }
        else if (pointer == inputList.Count)
        {
            return;
        }

        pointer++;
        InputKickCommands();
    }
}