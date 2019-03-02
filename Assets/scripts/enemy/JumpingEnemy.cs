using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : MonoBehaviour {

    [Header("Movement Speed")]
    public float movementSpeed = 2.5f;

    [Header("Facing right")]
    public bool isFacingRight = false;

    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;
    private float rayLength = 0.2f;
    private CircleCollider2D circleCollider;
    private LayerMask playerLayerMask;

    // Use this for initialization
    void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.flipX = isFacingRight;
        circleCollider = GetComponent<CircleCollider2D>();
        playerLayerMask = 1 << 8;
    }
	
	// Update is called once per frame
	void Update () {
	    if (isColliderInFront())
	    {
	        turnAround();
	    }

	    float newMovementX = -movementSpeed;

	    if (isFacingRight)
	    {
	        newMovementX = movementSpeed;
	    }

	    rb2d.velocity = new Vector2(newMovementX, rb2d.velocity.y);
    }

    void turnAround()
    {
        isFacingRight = !isFacingRight;
        sprite.flipX = isFacingRight;
    }

    bool isColliderInFront()
    {
        float rayOriginXMargin = circleCollider.radius + 0.01f;
        float rayOriginX = gameObject.transform.position.x;
        float rayOriginY = gameObject.transform.position.y;
        Vector2 facingVector = isFacingRight ? Vector2.right : Vector2.left;

        if (isFacingRight)
        {
            rayOriginX += rayOriginXMargin;
        }
        else
        {
            rayOriginX -= rayOriginXMargin;
        }

        // should not turn if player



        Vector2 rayOrigin = new Vector2(rayOriginX, rayOriginY);
        RaycastHit2D ray = Physics2D.Raycast(rayOrigin, facingVector, rayLength, ~playerLayerMask);
        Debug.DrawRay(rayOrigin, facingVector * rayLength, Color.magenta, 0f, false);

        if (ray.collider != null)
        {
            return true;
        }

        return false;
    }
}
