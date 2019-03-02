using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_player_enemy : enemy {

    private GameObject player;
    
	// Use this for initialization
	void Start () {
        defaultContructor();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (player.transform.position.x < gameObject.transform.position.x && isFacingRight)
        {
                turnAround();
            
        }
        else if(!isFacingRight && player.transform.position.x > gameObject.transform.position.x)
        {
            turnAround();
        }

        standardMovement();
	}
}
