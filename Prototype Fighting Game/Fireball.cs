using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    //Hit detection for the fireball
    private void OnCollisionEnter(Collision collision)
    {
        GameObject opponent = collision.gameObject;

        if (opponent.name == "Opponent")
        {
            Debug.Log("Fireball hit enemy");
            Destroy(gameObject);
        }
    }
}
