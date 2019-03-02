using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpingEnemy : enemy
{

    private GameObject player;
    private ManageGame gameManager;

    private float xThredshold = 1f;
    [SerializeField] private float lastGroundHit = 0;
    private float timeForTurn = 1;

    void Start()
    {
        defaultContructor();
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManageGame>();
    }

    void Update()
    {
        bool isGrounded = IsGrounded();
        if (player.transform.position.x < gameObject.transform.position.x && isFacingRight)
        {
            // få det her til at virke
            if (Mathf.Abs(player.transform.position.x - transform.position.x) > xThredshold)
            {
                turnAround();
            }

        }
        else if (!isFacingRight && player.transform.position.x > gameObject.transform.position.x)
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) > xThredshold)
            {
                turnAround();
            }
                
        }

        jumpingMovement(isGrounded);
    }

    private void jumpingMovement(bool isGrounded)
    {
        float movementX = -movementSpeed;

        if (isFacingRight)
        {
            movementX = movementSpeed;
        }

        if (isGrounded && gameManager.Timer - lastGroundHit > xThredshold)
        {
            rb2d.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            lastGroundHit = gameManager.Timer;
        }

        if (player.transform.position.x > transform.position.x - (xThredshold / 2) &&
            player.transform.position.x < transform.position.x + (xThredshold / 2) &&
            !isGrounded)
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        else if (!isGrounded)
        {
            rb2d.velocity = new Vector2(movementX, rb2d.velocity.y);
        }
        
    }

    private bool IsGrounded()
    {
        float rayLength = circleCollider.radius * 1.1f;
        int layerMask = 1 << 9;
        RaycastHit2D ray = Physics2D.Raycast(gameObject.transform.position, Vector2.down, rayLength, ~layerMask);
        Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.magenta, 0f, false);

        if (ray.collider != null)
        {
            return true;
        }

        return false;
    }
}
