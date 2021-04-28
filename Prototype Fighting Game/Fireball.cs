using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private int damage = 60;
    private int chip = 0;
    private int stun = 80;
    private int counter = 66;
    private int counterStun = 94;
    private int meter = 15;
    private int whiff = 10;
    private int onHit = 5;

    //Hit detection for the fireball
    private void OnCollisionEnter(Collision collision)
    {
        GameObject opponent = collision.gameObject;

        Universal opponentHealth = opponent.GetComponent<Universal>();

        if (opponent.name == "Opponent")
        {
            opponentHealth.calculateCombo(opponentHealth.getHitStun(), onHit);
            opponentHealth.calculateDamage(damage);
            Destroy(gameObject);
        }
    }
}
