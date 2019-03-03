using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanagerCreator : MonoBehaviour {

    public GameObject manageGame;

	// Use this for initialization
	void Awake () {
        GameObject other = GameObject.FindGameObjectWithTag("GameManager");
        if (other == null) Instantiate(manageGame);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
