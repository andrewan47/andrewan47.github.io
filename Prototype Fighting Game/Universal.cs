using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universal : MonoBehaviour
{
    public class Stats
    {
        private int health;
        private int stun;
        private int meter;
        private int speed;
        private int backSpeed;
        private int jumpSpeed;
        private bool isCrouching;
        private bool isBlocking;

        public Stats(int hp, int stn, int mtr, int spd, int bspd, int jspd)
        {
            health = hp;
            stun = stn;
            meter = mtr;
            speed = spd;
            backSpeed = bspd;
            jumpSpeed = jspd;
            isCrouching = false;
            isBlocking = false;
        }

        public Stats()
        {
            health = 1;
            stun = 1;
            meter = 1;
            speed = 1;
            backSpeed = 1;
            jumpSpeed = 1;
            isCrouching = false;
            isBlocking = false;
        }

        public void setStun(int stn)
        {
            stun = stn;
        }

        public void setMeter(int mtr)
        {
            meter = mtr;
        }

        public void setCrouching(bool crouch)
        {
            isCrouching = crouch;
        }

        public void setBlocking(bool block)
        {
            isBlocking = block;
        }

        public int getHP()
        {
            return health;
        }

        public int getStun()
        {
            return stun;
        }

        public int getMeter()
        {
            return meter;
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

        public int getJumpSpeed()
        {
            return jumpSpeed;
        }

        public bool getBlock()
        {
            return isBlocking;
        }

        public bool getCrouching()
        {
            return isCrouching;
        }
    }

    public class Jab
    {
        private int damage;
        private int chip;
        private int stunDamage;
        private int counterDamage;
        private int counterStun;
        private int meterGain;

        public Jab(int dmg, int ch, int sdmg, int cdmg, int cstn, int mtrg)
        {
            damage = dmg;
            chip = ch;
            stunDamage = sdmg;
            counterDamage = cdmg;
            counterStun = cstn;
            meterGain = mtrg;
        }

        public Jab()
        {
            damage = 1;
            chip = 1;
            stunDamage = 1;
            counterDamage = 1;
            counterStun = 1;
            meterGain = 1;
        }
    }
}
