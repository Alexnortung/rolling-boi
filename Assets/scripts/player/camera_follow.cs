using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour
{

    [SerializeField] private GameObject Player;

    [SerializeField] private int Tolerence = 1;

	// Use this for initialization
	void Start ()
	{
	    Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
    }
}
