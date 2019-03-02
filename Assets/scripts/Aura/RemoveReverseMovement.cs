using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveReverseMovement : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Inside");
        if (col.tag == "Player")
        {
            player_controller playerController = col.gameObject.GetComponent<player_controller>();
            if (playerController != null)
            {
                playerController.isReversed = false;
            }
        }
    }
}
