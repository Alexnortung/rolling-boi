using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour {

    public Rigidbody2D rb2d;
    public ManageGame gameManager;
    public float movementSpeed = 5;
    public float accelerationAir = 5;
    public float jumpHeight = 7;
    public float maxVelocityX = 5;
    public float fireVelocity = 3.75f;

    public float normalGravityScale = 0.7f;

    public float fireAngularVel = 10f;

    private List<KeyCode> jumpKeys = new List<KeyCode>();
    private RaycastHit2D rayHit;
    private CircleCollider2D body;
    private float defaultDrag = 0;
    private GameObject fire;

    [SerializeField] public bool isReversed = false;
    [SerializeField] public bool isAbleToWalkOnSpikes = false;



	// Use this for initialization
	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        body = gameObject.GetComponent<CircleCollider2D>();
        jumpKeys.Add(KeyCode.Space);
        jumpKeys.Add(KeyCode.W);
        jumpKeys.Add(KeyCode.UpArrow);
	    gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManageGame>();
        fire = transform.GetChild(0).gameObject;
	}

	// Update is called once per frame
	void Update () {
        float horizontalSpeed = Input.GetAxis("Horizontal");
        bool grounded = IsGrounded();

        // the multiplyer that want to be set -1 if controls be reversed
	    int moveMultiplier = 1;

	    if (isReversed)
	    {
	        moveMultiplier = -1;
	    }

        if (grounded)
        {
            rb2d.velocity = new Vector2(horizontalSpeed * movementSpeed * moveMultiplier, rb2d.velocity.y);
        }
        else
        {
            if (horizontalSpeed == 0)
            {
                // air resistance
                rb2d.AddForce(new Vector2(-rb2d.velocity.x * 3 /10, 0), ForceMode2D.Force);
            }
            else
            {
                rb2d.AddForce(new Vector2(horizontalSpeed * accelerationAir * 5, 0), ForceMode2D.Force);
            }
        }

        float velx = rb2d.velocity.x;

        if (Mathf.Abs(velx) > maxVelocityX)
        {
            //set speed in x direction
            float newXVel = Mathf.Sign(velx) * maxVelocityX;
            rb2d.velocity = new Vector2(newXVel, rb2d.velocity.y);
        }





        if (grounded && IfOneOfMultipleKeyDown(jumpKeys))
        {
            rb2d.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }

        // Debug.Log(rb2d.velocity);

        setFireIfAngularVel();
	}

    public void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.collider.tag)
        {
            case "Spikes":
                if(!isAbleToWalkOnSpikes) gameManager.RestartLevel();
                break;
            case "Enemy":
                gameManager.RestartLevel();
                break;
            default:
                break;;
        }
    }
    
    void setFireIfAngularVel()
    {
        if (Mathf.Abs(rb2d.angularVelocity) > fireAngularVel)
        {

            float fireDir = Mathf.Sign(rb2d.angularVelocity);
            fire.transform.localScale = new Vector3(-0.3f * fireDir, 0.3f, -0.5f);
            float fireRotation = -transform.eulerAngles.z * Mathf.Deg2Rad;
            fire.transform.eulerAngles = new Vector3(0, 0, fireRotation);
            Vector2 newFirePos = new Vector2(-0.55f * fireDir, 0);
            float newX = Mathf.Cos(fireRotation) * newFirePos.magnitude;
            float newY = Mathf.Sin(fireRotation) * newFirePos.magnitude;
            newFirePos = new Vector2(newX * fireDir, newY * fireDir);
            //Debug.Log(fireRotation);
            fire.transform.localPosition = newFirePos;
            fire.transform.position.Set(fire.transform.position.x, fire.transform.position.y, -0.1f);

        } else
        {
            fire.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "AlienBeam":
                AlienBeamBehaviour();
                break;
            default:
                break;
        }
        //Debug.Log("entered");
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "AlienBeam":
                ReverseAlienBeamBehaviour();
                break;
            default:
                break;
        }
    }

    private void AlienBeamBehaviour()
    {
        rb2d.gravityScale = -normalGravityScale;
    }

    private void ReverseAlienBeamBehaviour()
    {
        rb2d.gravityScale = normalGravityScale;
    }

    private bool IfOneOfMultipleKeyDown(List<KeyCode> list)
    {
        foreach (KeyCode key in list)
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsGrounded()
    {
        float rayLength = body.radius * 1.1f;
        int layerMask = 1 << 8;
        RaycastHit2D ray = Physics2D.Raycast(gameObject.transform.position, Vector2.down, rayLength, ~layerMask);
        Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.magenta, 0f, false);

        if (ray.collider != null)
        {
            return true;
        }

        return false;
    }


}
