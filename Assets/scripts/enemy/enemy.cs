using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    [Header("Movement Speed")]
    public float movementSpeed = 2.5f;

    [Header("Facing right")]
    public bool isFacingRight = false;

    protected Rigidbody2D rb2d;
    protected SpriteRenderer sprite;
    protected float rayLength = 0.2f;
    protected CircleCollider2D circleCollider;
    public LayerMask playerLayerMask = 1 << 8;
    public Transform kapowPrefab;

    public bool isReversedGravity = false;
    public float normalGravityScale = 0.7f;

    // Use this for initialization
    void Start () {
        defaultContructor();
        
    }

    protected void defaultContructor()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();

        if(isReversedGravity)
        {
            ReverseGravity();
        } else
        {
            NormalGravity();
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (isColliderInFront())
        {
            turnAround();
        }
        standardMovement();
	}


    protected void standardMovement()
    {
        

        float newMovementX = -movementSpeed;

        if (isFacingRight)
        {
            newMovementX = movementSpeed;
        }

        rb2d.velocity = new Vector2(newMovementX, rb2d.velocity.y);
    }

    protected void turnAround()
    {
        isFacingRight = !isFacingRight;
        sprite.flipX = !sprite.flipX;
    }

    public void die()
    {
        Instantiate(kapowPrefab, gameObject.transform.position, Quaternion.identity);
        Debug.Log(gameObject.transform);
        Destroy(gameObject);
    }

    protected bool isColliderInFront()
    {
        float rayOriginXMargin = circleCollider.radius + 0.01f;
        float rayOriginX = gameObject.transform.position.x;
        float rayOriginY = gameObject.transform.position.y;
        Vector2 facingVector = isFacingRight ? Vector2.right : Vector2.left;

        if(isFacingRight)
        {
            rayOriginX += rayOriginXMargin;
        }
        else
        {
            rayOriginX -= rayOriginXMargin;
        }

        // should not turn if player

        int obstaclesLayer = 1 << 10;

        int layermask = playerLayerMask | obstaclesLayer;

        Vector2 rayOrigin = new Vector2(rayOriginX, rayOriginY);
        RaycastHit2D ray = Physics2D.Raycast(rayOrigin, facingVector, rayLength, ~layermask);
        Debug.DrawRay(rayOrigin, facingVector * rayLength, Color.magenta, 0f, false);

        if (ray.collider != null)
        {
            return true;
        }

        return false;
    }

    public void ReverseGravity()
    {
        isReversedGravity = true;
        rb2d.gravityScale = -normalGravityScale;
        sprite.flipY = true;
    }

    public void NormalGravity()
    {
        isReversedGravity = false;
        rb2d.gravityScale = normalGravityScale;
        sprite.flipY = false;
    }
}
