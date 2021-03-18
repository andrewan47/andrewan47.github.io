using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : Universal
{
    private int maxHP = 1000;
    private int maxStun = 1000;
    private int maxMeter = 100;

    public int HP;
    public int meter;
    public int stun;

    // Start is called before the first frame update
    void Start()
    {
        Stats(maxHP, maxStun, maxMeter, 0, 0, 0);
        stun = 0;
        meter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        HP = getHP();
        stun = getStun();
        Healthbar.setHealthBarValue(HP, maxHP);
    }
}
