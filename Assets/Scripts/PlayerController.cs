using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float JumpSpeed;
    public float SlideSpeed;

    public int MaxJumpTimes;
    public float JumpDeactivePeriod;
    public float DieAltitude;

    public KeyCode LeftButton;
    public KeyCode RightButton;
    public KeyCode JumpButton;

    private bool alive;
    private bool active;
    private bool onLeftWall;
    private bool onRightWall;
    private int jumpTimes;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    /*private GameController gc;*/

    void Start()
    {
        alive = true;
        active = true;
        onLeftWall = false;
        onRightWall = false;
        jumpTimes = MaxJumpTimes;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        /*gc = GameObject.Find("GameController").GetComponent<GameController>();*/
    }

    void Update()
    {
        if (isActive())
        {
            UpdateVelocity();
        }
        if (gameObject.transform.position.y < DieAltitude)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isTerrain(collision))
        {
            jumpTimes = MaxJumpTimes;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        Collider2D otherCollider = collision.otherCollider;
        if (isHorizontalCollision(collider, otherCollider))
        {
            Debug.Log("Sliding");
            rb.velocity = new Vector2(0f, -SlideSpeed);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onLeftWall = false;
        onRightWall = false;
    }

    private void UpdateVelocity()
    {
        // Update Horizontal Velocity
        if (Input.GetKey(LeftButton))
        {
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
            sr.flipX = true;
        }
        else if (Input.GetKey(RightButton))
        {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
            sr.flipX = false;
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        // Handle Jumping
        if (Input.GetKeyDown(JumpButton) && jumpable())
        {
            jump();
        }
    }

    private bool isTerrain(Collision2D collision)
    {
        return collision.gameObject.CompareTag("Block");
        //    || collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Wall");
    }

    private void jump()
    {
        jumpTimes--;
        if (onLeftWall)
        {
            rb.velocity = new Vector2(Speed, JumpSpeed);
            sr.flipX = false;
            StartCoroutine(JumpCoroutine());
        }
        else if (onRightWall)
        {
            rb.velocity = new Vector2(-Speed, JumpSpeed);
            sr.flipX = true;
            StartCoroutine(JumpCoroutine());
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpSpeed);
        }
    }

    public IEnumerator JumpCoroutine()
    {
        active = false;
        yield return new WaitForSeconds(JumpDeactivePeriod);
        active = true;
    }

    private bool jumpable()
    {
        return (jumpTimes > 0);
    }

    private bool detectWall(Vector2 direction)
    {
        // Mask Player Layer
        int layerMask = 1 << 3;
        layerMask = ~layerMask;
        // Does the ray intersect any objects excluding the player layer
        return Physics.Raycast(transform.position, transform.TransformDirection(direction), bc.size.x/2, layerMask);
    }

    public void Die()
    {
        alive = false;
        /*gc.Killed(gameObject);*/
    }

    public bool isActive()
    {
        return alive && active;
    }

    private bool isHorizontalCollision(Collider2D collider1, Collider2D collider2)
    {
        int c1RightEdge = (int)collider1.bounds.max.x;
        int c1LeftEdge = (int)collider1.bounds.min.x;

        int c2RightEdge = (int)collider2.bounds.max.x;
        int c2LeftEdge = (int)collider2.bounds.min.x;

        return (c1RightEdge == c2LeftEdge) || (c1LeftEdge == c2RightEdge);
    }
}
