using System.Collections;
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


	// Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	    currentLevel = 1;
	}
	
	// Update is called once per frame
	void Update () {
	    if (IsLevelWon)
	    {
            ChangeLevel();
	        IsLevelWon = false;
	    }
	}

    private void ChangeLevel()
    {
        if (currentLevel <= SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentLevel, LoadSceneMode.Single);
            currentLevel++;
        }

    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    
}
