using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boos : MonoBehaviour {

    public GameObject player;
    private player_controller Player_Controller;
    public GameObject bottomPath;
    public GameObject topPath;
    public GameObject leftMostWall;
    public GameObject rightMostWall;
    public ManageGame manageGame;
    public GameObject[] auras;
    public float castTime = 7;
    public GameObject portalPrefab;
    public float portalSpawnRadiusMax = 5;
    public float portalSpawnRadiusMin = 3;

    private List<GameObject> instantiatedAuras;
    private Random rnd = new Random();
    private float lastTimeAttack = 0;
    



    // Use this for initialization
    void Start () {
        manageGame = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManageGame>();
        lastTimeAttack = manageGame.Timer;
        Player_Controller = player.GetComponent<player_controller>();



    }
	
	// Update is called once per frame
	void Update () {
		

        if(castTime < (manageGame.Timer - lastTimeAttack))
        {
            //attackk
            summonPortal();
            throwAura();

        }
        
	}

    void summonPortal()
    {
        Vector2 portalPosition = Vector2.up;
        portalPosition *= Random.Range(portalSpawnRadiusMin, portalSpawnRadiusMax);
        Debug.Log(Random.Range(portalSpawnRadiusMin, portalSpawnRadiusMax));
        Debug.Log(portalPosition);
        float angle = Random.Range(0, 90) * Mathf.Deg2Rad;
        if(Player_Controller.isReversedGravity)
        {
            angle += 90f * Mathf.Deg2Rad;
        }

        portalPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * portalPosition.magnitude;
        portalPosition = portalPosition + (Vector2)player.transform.position;

        float newPortalPositionY = portalPosition.y;
        float newPortalPositionX = portalPosition.x;

        if(topPath.transform.position.y < portalPosition.y)
        {
            newPortalPositionY = topPath.transform.position.y - 0.5f;
        }
        if(rightMostWall.transform.position.x < portalPosition.x)
        {
            newPortalPositionX = rightMostWall.transform.position.x - 0.5f;
        }

        portalPosition = new Vector2(portalSpawnRadiusMin + newPortalPositionX, newPortalPositionY);

        Instantiate(portalPrefab, portalPosition, Quaternion.identity);

        Debug.Log(portalPosition);



        lastTimeAttack = manageGame.Timer;
    }

    void throwAura()
    {
        lastTimeAttack = manageGame.Timer;
        GameObject selectedAura = auras[Random.Range(0, auras.Length - 1)];
        Vector2 auraDirection = (player.transform.position - gameObject.transform.position);
        auraDirection.Normalize();


        GameObject aura = Instantiate<GameObject>(selectedAura, gameObject.transform.position, Quaternion.identity).gameObject;
        //instantiatedAuras.Add(aura);
        Rigidbody2D auraBody = aura.AddComponent<Rigidbody2D>();
        auraBody.gravityScale = 0f;
        auraBody.angularDrag = 0;

        float auraSpeed = 10f;

        auraBody.velocity = auraDirection * auraSpeed;

        
    }
}
