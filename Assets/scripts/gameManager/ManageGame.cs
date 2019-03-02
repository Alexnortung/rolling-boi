﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] public bool GameWon { get; set; }

    [SerializeField] private int currentLevel;

    [SerializeField] public bool IsLevelWon = false;

    [SerializeField] public bool HasGameBegun = true;

    public float Timer = 0;


	// Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	    currentLevel = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    if (IsLevelWon)
	    {
            ChangeLevel();
	        IsLevelWon = false;
	    }

	    Timer += Time.deltaTime;
	}

    private void ChangeLevel()
    {
        if (currentLevel <= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentLevel, LoadSceneMode.Single);
            currentLevel++;
        }
        else
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void SelectLevel(int levelInt)
    {
        SceneManager.LoadScene(levelInt, LoadSceneMode.Single);
        currentLevel = levelInt + 1;
    }

    
}
