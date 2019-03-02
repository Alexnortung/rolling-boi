using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpingEnemy : enemy
{

    private GameObject player;
    private ManageGame gameManager;
    [SerializeField] private SpriteRenderer spRenderer;
    [SerializeField] private Sprite[] JumpingSprites;

    private float xThredshold = 1f;
    [SerializeField] private float lastGroundHit = 1;
    [SerializeField] private float UnstuckTimer = 3;
    [SerializeField] private float AwakeDistance = 10;
    [SerializeField] private float distanceToPlayer;
    private float timeForTurn = 1;

    void Start()
    {
        defaultContructor();
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManageGame>();
        spRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < AwakeDistance)
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
            Unstuck();
        }
    }

    private void jumpingMovement(bool isGrounded)
    {
        float movementX = -movementSpeed;

        if (isFacingRight)
        {
            movementX = movementSpeed;
        }

        if (rb2d.velocity.y > 0)
        {
            spRenderer.sprite = JumpingSprites[3];
        }
        else if (rb2d.velocity.y < 0)
        {
            spRenderer.sprite = JumpingSprites[2];
        }
        else
        {
            spRenderer.sprite = JumpingSprites[1];
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

    private void Unstuck()
    {
        float rayLength = circleCollider.radius * 1.3f;
        int layerMask = 1 << 9;
        RaycastHit2D rayRight = Physics2D.Raycast(gameObject.transform.position, Vector2.right, rayLength, ~layerMask);
        Debug.DrawRay(transform.position, Vector3.right * rayLength, Color.magenta, 0f, false);
        RaycastHit2D rayLeft = Physics2D.Raycast(gameObject.transform.position, Vector2.left, rayLength, ~layerMask);
        Debug.DrawRay(transform.position, Vector3.left * rayLength, Color.magenta, 0f, false);

        if (rayRight.collider != null && rb2d.velocity.x > 0)
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            
            //rb2d.AddForce(new Vector2(-100,0), ForceMode2D.Impulse);
        }

        if (rayLeft.collider != null && rb2d.velocity.x < 0)
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
    }
}
