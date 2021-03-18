using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universal : MonoBehaviour
{
    private int hp;
    private int stun;
    private int meter;
    private int currentHP;
    private int currentStun;
    private int currentMeter;
    private float spd;
    private float backSpeed;
    private int jumpSpeed;
    List<int> inputList = new List<int>();
    private int direction;
    private int prevDirec;
    private float popCounter;
    private float jump;
    private float walk;
    private bool isBlocking;
    private bool isCrouching;

    public void Stats(int hp, int stun, int meter, float spd, float backSpeed, int jumpSpeed)
    {
        this.hp = hp;
        this.stun = stun;
        this.meter = meter;
        currentHP = hp;
        currentStun = 0;
        currentMeter = 0;
        this.spd = spd;
        this.backSpeed = backSpeed;
        this.jumpSpeed = jumpSpeed;
        popCounter = 0.2f;
        setPrevDirec(0);
        inputList.Add(0);
    }

    public void Movement(CharacterController controller, Animator anim)
    {
        //Sets the directional inputs to an interger value
        if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") > 0)
        {
            setPrevDirec(getDirection());
            setDirection(9);
        }
        else if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") < 0)
        {
            setPrevDirec(getDirection());
            setDirection(7);
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") > 0)
        {
            setPrevDirec(getDirection());
            setDirection(3);
        }
        else if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") < 0)
        {
            setPrevDirec(getDirection());
            setDirection(1);
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            setPrevDirec(getDirection());
            setDirection(8);
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            setPrevDirec(getDirection());
            setDirection(2);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            setPrevDirec(getDirection());
            setDirection(6);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            setPrevDirec(getDirection());
            setDirection(4);
        }
        else
        {
            setPrevDirec(getDirection());
            setDirection(0);
        }

        if (controller.isGrounded && Input.GetAxisRaw("Vertical") > 0)
        {
            jump = getJumpSpeed();
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            setBlocking(true);
        }
        else
        {
            setBlocking(false);
        }

        if (controller.isGrounded && Input.GetAxisRaw("Vertical") < 0)
        {
            setCrouching(true);
        }
        else
        {
            setCrouching(false);
        }

        walk = Input.GetAxis("Horizontal") * getSpeed(getCrouching(), getBlocking());
        jump += Physics.gravity.y * Time.deltaTime;
        Vector3 speed = new Vector3(walk, jump, 0);
        controller.Move(speed * Time.deltaTime);

        AddToInput();

        //Removes the first item in the list after a set amount of time
        popCounter -= Time.deltaTime;
        if (popCounter <= 0)
        {
            if (inputList.Count > 0)
            {
                inputList.RemoveAt(0);
            }
            popCounter = 0.2f;
        }

        anim.SetFloat("Speed", walk);
    }

    public float getSpeed(bool isCrouching, bool isBlocking)
    {
        if (isCrouching)
        {
            return 0.0f;
        }
        else if (isBlocking)
        {
            return backSpeed;
        }
        else
        {
            return spd;
        }
    }

    public int getJumpSpeed()
    {
        return jumpSpeed;
    }

    //Adds the directional input into a list
    void AddToInput()
    {
        int size = inputList.Count;

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

    public string Attack(Animator anim, string attack, int[] special1, int[] special2, int[] special3, int[] super)
    {
        int input = 0;
        int sp1 = 0;
        int sp2 = 0;
        int sp3 = 0;
        int su = 0;
        int pointer = 0;
        string type = "fireball";
        bool result = false;

        //Throw command
        if (Input.GetKeyDown(KeyCode.Joystick1Button0) && Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            //Start animation for throw
            attack = "grab";
        }
        //Jab command
        else if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            input = 0;
            attack = "Jab";

            attack = InputPunchCommands(special1, special2, special3, super, sp1, sp2, sp3, su, pointer, input, attack);

            result = attack.Equals(type);
            if (result)
            {
                return attack;
            }
            else
            {
                anim.SetTrigger(attack);
            }
        }
        //Light kick command
        else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            input = 10;
            //InputKickCommands();
            attack = "kick";
        }

        return attack;
    }

    //Checks for special moves and returns the animation of the attack that requires to be played
    public string InputPunchCommands(int[] special1, int[] special2, int[] special3, int[] super, int sp1, int sp2, int sp3, int su, int pointer, int input, string attack)
    {
        if (inputList[pointer] == special1[sp1])
        {
            sp1++;
        }
        if (inputList[pointer] == special2[sp2])
        {
            sp2++;
        }
        if (inputList[pointer] == special3[sp3])
        {
            sp3++;
        }

        pointer++;

        if (special1[sp1] == input)
        {
            sp1++;
            if (special1[sp1] == 15)
            {
                return attack;
            }
            else if (special1[sp1] == 26)
            {
                return attack;
            }
            else
            {
                return attack;
            }
        }
        else if (special2[sp2] == input)
        {
            sp2++;
            if (special2[sp2] == 15)
            {
                attack = "fireball";
                return attack;
            }
            else if (special2[sp2] == 26)
            {
                return attack;
            }
            else
            {
                return attack;
            }
        }
        else if (pointer >= inputList.Count)
        {
            return attack;
        }

        return attack = InputPunchCommands(special1, special2, special3, super, sp1, sp2, sp3, su, pointer, input, attack);
    }

    public void setBlocking(bool isBlocking)
    {
        this.isBlocking = isBlocking;
    }

    public void setCrouching(bool isCrouching)
    {
        this.isCrouching = isCrouching;
    }

    public void setDirection(int direction)
    {
        this.direction = direction;
    }

    public void setPrevDirec(int prevDirec)
    {
        this.prevDirec = prevDirec;
    }

    public bool getBlocking()
    {
        return isBlocking;
    }

    public bool getCrouching()
    {
        return isCrouching;
    }

    public int getDirection()
    {
        return direction;
    }

    public int getPrevDirection()
    {
        return prevDirec;
    }


    public int getHP()
    {
        return currentHP;
    }

    public int getStun()
    {
        return currentStun;
    }

    public int getMeter()
    {
        return currentMeter;
    }

    public void calculateDamage(int attack)
    {
        currentHP -= attack;
    }

    public void calculateStun(int value)
    {
        currentStun += value;
    }

    public void calculateMeter(int value)
    {
        currentMeter += value;
    }
}
