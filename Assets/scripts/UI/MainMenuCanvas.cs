using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour {

    public int levelInt;

    private Button buttonComp;
    private ManageGame manageGame;

	// Use this for initialization
	void Start () {
        manageGame = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManageGame>();
        buttonComp = gameObject.GetComponent<Button>();
        buttonComp.onClick.AddListener(delegate { manageGame.SelectLevel(levelInt); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
