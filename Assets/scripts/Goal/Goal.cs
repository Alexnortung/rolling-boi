using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    [SerializeField] private ManageGame gameManager;

    public void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManageGame>();
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            gameManager.IsLevelWon = true;
        }
    }
}
