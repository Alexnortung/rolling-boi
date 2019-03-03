using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGravity : MonoBehaviour {

    private player_controller playerController;

    // Use this for initialization
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<player_controller>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (col.GetComponent<player_controller>() != null)
            {
                col.GetComponent<player_controller>().NormalGravity();
            }
        }
    }
}
