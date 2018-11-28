using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {

    public float recovery;
    public float respawnTimer;
    private Vector3 spawnPoint;
    GameObject pack;
    bool active;
    [SerializeField] private Transform healthPack;

	// Use this for initialization
	void Start () {
        recovery = 75;
        respawnTimer = 5.0f;
        active = true;
        spawnPoint = new Vector3 (-10, 0.5f, 4);
	}
	
	// Update is called once per frame
	void Update () {
        RpcPack();
	}

    //When player makes contact with the health pack it does a check.
    void OnTriggerEnter(Collider collider)
    {
        GameObject player = collider.gameObject;
        Health playerHealth = player.GetComponent<Health>();

        //If player health is below 100% it takes the health pack and heals the player
        if (playerHealth.GetHealth() < playerHealth.GetMaxH() && GetComponent<Renderer>().enabled == true)
        {
            playerHealth.RecoverHealth(recovery);
            active = false;
            GetComponent<Renderer>().enabled = active;
        }
    }

    //respawn timer for the health pack
    void RpcPack()
    {
        if (active == false)
        {
            respawnTimer -= Time.deltaTime;
            if (respawnTimer <= 0)
            {
                active = true;
                GetComponent<Renderer>().enabled = active;
                respawnTimer = 5.0f;
            }
        }
    }
}