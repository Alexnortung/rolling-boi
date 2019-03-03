using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    [SerializeField] public int goombasToCreate = 5;
    public float timeBetweenGoombas = 2;
    private GameObject player;
    private player_controller Player_Controller;
    public GameObject gumba;
    [SerializeField] private int goombaCounter = 0;

	// Use this for initialization
	void Start () {
        for (float i = 1; i <= goombasToCreate; i++)
        {
            StartCoroutine(CreateEnemies(i));
        }
        
        player = GameObject.FindGameObjectWithTag("Player");
        Player_Controller = player.GetComponent<player_controller>();
	}
	
	// Update is called once per frame
	void Update () {
		if (goombasToCreate == goombaCounter)
        {
            Destroy(gameObject);
        }

	}

    IEnumerator CreateEnemies(float time)
    {
        yield return new WaitForSeconds(time * timeBetweenGoombas);
        GameObject enemyObj = Instantiate<GameObject>(gumba, transform.position, Quaternion.identity);
        enemy enemyScript = enemyObj.GetComponent<enemy>();
        if(player.transform.position.x > gameObject.transform.position.x)
        {
            enemyScript.turnAround();
        }

        if (Player_Controller.isReversedGravity)
        {
            enemyScript.ReverseGravity();
        }

        goombaCounter++;
        //Debug.Log("GOOMBA COUNTED");
        
        
    }


}
