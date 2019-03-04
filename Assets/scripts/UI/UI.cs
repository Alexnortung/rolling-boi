using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public ManageGame gameManager;
    public Text LevelText;
    public Text TimeText;
    public Text baddiesKilledText;
    public GameObject pauseMenu;

    public void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManageGame>();
        LevelText.text = (gameManager.GetCurrentLevel() - 1).ToString();
    }

    public void Update()
    {
        TimeText.text = gameManager.Timer.ToString("0.0");
        baddiesKilledText.text = gameManager.baddiesKilled.ToString();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            togglePauseMenu();
        }
    }

    public void setStatePauseMenu(bool state)
    {
        //Debug.Log("setting pause menu state");
        pauseMenu.SetActive(state);
        if(state)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
        
    }

    public void togglePauseMenu()
    {
        setStatePauseMenu( !pauseMenu.activeSelf);
        //Debug.Log("TOGGLED");
    }

    public void exitGame ()
    {
        Application.Quit();
    }



    
}
