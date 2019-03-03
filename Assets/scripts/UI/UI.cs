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

    public void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManageGame>();
        LevelText.text = (gameManager.GetCurrentLevel() - 1).ToString();
    }

    public void Update()
    {
        TimeText.text = gameManager.Timer.ToString("0.0");
        baddiesKilledText.text = gameManager.baddiesKilled.ToString();
    }
}
