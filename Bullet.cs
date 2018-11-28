using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float damage = 14f;
    public float buff = 1f;
    float lifespan = 10.0f;

    // Use this for initialization
    void Start () {
        buff = 1f;
        damage = 14f;
    }
	
	// Update is called once per frame
	void Update () {
        //Destroys the object after a set amount of time it was active
        lifespan -= Time.deltaTime;

        if (lifespan <= 0)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.gameObject;

        //Checks if it hit a player and does the appropriate damage
        if(player.name == "Player")
        {
            Health enemyHealth = player.GetComponent<Health>();
            enemyHealth.RecieveDamage(damage);
            Debug.Log("Hit the player.");
        }

        //Destroy the game object on collision
        Destroy(gameObject);
    }

    void BuffOn(float boost)
    {
        buff *= boost;
    }

    void BuffOff(float boost)
    {
        buff /= boost;
    }
}