using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endScreenImageResize : MonoBehaviour {

    public ManageGame manageGame;
    public Text baddies;
    public Text time;
    public Text levelStarted;

	// Use this for initialization
	void Start () {
        manageGame = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManageGame>();
        manageGame.HasGameBegun = false;
        baddies.text = manageGame.baddiesKilled.ToString();
        time.text = manageGame.Timer.ToString();
        levelStarted.text = manageGame.levelStarted.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void restartGame()
    {
        manageGame.gotoMainMenu();
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
