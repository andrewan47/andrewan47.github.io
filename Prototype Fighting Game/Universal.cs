using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universal : MonoBehaviour
{
    public class Stats
    {
        public int health;
        public int stun;
        public int meter;
        public int speed;
        public int backSpeed;
        public int jumpSpeed;
        public float recovery;
        public float start;
        public float active;
        public float reset;
        public float jumpRecovery;
        public bool isJumping;
        public bool isAttacking;
        public bool isCrouching;
        public bool isBlocking;

        //Display for testing purposes
        public bool isSpecialAttack;

        public Stats(int hp, int stn, int mtr, int spd, int bspd, int jspd, float jrcv)
        {
            health = hp;
            stun = stn;
            meter = mtr;
            speed = spd;
            backSpeed = bspd;
            jumpSpeed = jspd;
            jumpRecovery = jrcv;
        }

        public Stats()
        {
            health = 1;
            stun = 0;
            meter = 1;
            speed = 1;
            backSpeed = 1;
            jumpSpeed = 1;
            recovery = 0f;
            start = 0f;
            active = 0f;
            reset = 0f;
            jumpRecovery = 0f;
            isJumping = false;
            isAttacking = false;
            isCrouching = false;
            isBlocking = false;

            isSpecialAttack = false;
        }

        public void setJumping(bool jump)
        {
            isJumping = jump;
        }

        public void setAttacking(bool attack)
        {
            isAttacking = attack;
        }

        public void setCrouching(bool crouch)
        {
            isCrouching = crouch;
        }

        public void setBlock(bool block)
        {
            isBlocking = block;
        }

        public void setRecovery(float rcv)
        {
            recovery = rcv;
        }

        public void setStart(float str)
        {
            start = str;
        }

        public void setActive(float act)
        {
            active = act;
        }

        public void setReset(float res)
        {
            reset = res;
        }

        public float getRecovery()
        {
            return recovery;
        }

        public int getSpeed()
        {
            if(isCrouching)
            {
                return 0;
            }
            else if(isBlocking)
            {
                return backSpeed;
            }
            else
            {
                return speed;
            }
        }
    }

    public class Jab
    {
        public int damage;
        public int chip;
        public int stunDamage;
        public int counterDamage;
        public int counterStun;
        public int meterGain;
        public float startUp;
        public float active;
        public float recovery;
        public float hitFrames;
        public float blockFrames;

        public Jab(int dmg, int ch, int sdmg, int cdmg, int cstn, int mtrg, float su, float act, float rcv, float onh, float onb)
        {
            damage = dmg;
            chip = ch;
            stunDamage = sdmg;
            counterDamage = cdmg;
            counterStun = cstn;
            meterGain = mtrg;
            startUp = su;
            active = act;
            recovery = rcv;
            hitFrames = onh;
            blockFrames = onb;
        }

        public Jab()
        {
            damage = 1;
            chip = 0;
            stunDamage = 1;
            counterDamage = 1;
            counterStun = 1;
            meterGain = 1;
            startUp = 1.0f;
            active = 1.0f;
            recovery = 1.0f;
            hitFrames = 1.0f;
            blockFrames = 1.0f;
        }
    }

    public class Projectile
    {
        public int damage;
        public int speed;
        public int chip;
        public int stunDamage;
        public int counterDamage;
        public int counterStun;
        public int meterGain;
        public float startUp;
        public float active;
        public float recovery;
        public float hitFrames;
        public float blockFrames;

        public Projectile(int dmg, int spd, int ch, int sdmg, int cdmg, int cstn, int mtrg, float su, float act, float rcv, float onh, float onb)
        {
            damage = dmg;
            speed = spd;
            chip = ch;
            stunDamage = sdmg;
            counterDamage = cdmg;
            counterStun = cstn;
            meterGain = mtrg;
            startUp = su;
            active = act;
            recovery = rcv;
            hitFrames = onh;
            blockFrames = onb;
        }

        public Projectile()
        {
            damage = 1;
            speed = 1;
            chip = 0;
            stunDamage = 1;
            counterDamage = 1;
            counterStun = 1;
            meterGain = 1;
            startUp = 1.0f;
            active = 1.0f;
            recovery = 1.0f;
            hitFrames = 1.0f;
            blockFrames = 1.0f;
        }
    }

    //Add command grab, Supers
    //Move Movement and possible attack commands here?
}