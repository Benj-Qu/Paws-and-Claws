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
    private bool onFloor;
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
        onFloor = true;
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
        // Jump Times
        if (isTerrain(collision))
        {
            jumpTimes = MaxJumpTimes;
        }
        // Slide Wall
        Collider2D collider = collision.collider;
        Collider2D otherCollider = collision.otherCollider;
        if (isRightCollision(collider, otherCollider))
        {
            onRightWall = true;
        }
        if (isLeftCollision(collider, otherCollider))
        {
            onLeftWall = true;
        }
        else
        {
            onFloor = true;
            onRightWall = false;
            onLeftWall = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
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
        // Wall Sliding
        if (onWall())
        {
            rb.velocity = new Vector2(0f, -SlideSpeed);
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
            onLeftWall = false;
            rb.velocity = new Vector2(Speed, JumpSpeed);
            sr.flipX = false;
            StartCoroutine(JumpCoroutine());
        }
        else if (onRightWall)
        {
            onRightWall = false;
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

    public void Die()
    {
        alive = false;
        /*gc.Killed(gameObject);*/
    }

    private bool onWall()
    {
        return (!onFloor) && (onLeftWall || onRightWall);
    }

    private bool isRightCollision(Collider2D collider1, Collider2D collider2)
    {
        int c1RightEdge = (int)collider1.bounds.max.x;

        int c1TopEdge = (int)collider1.bounds.max.y;
        int c1BottomEdge = (int)collider1.bounds.min.y;

        int c2LeftEdge = (int)collider2.bounds.min.x;

        int c2TopEdge = (int)collider2.bounds.max.y;
        int c2BottomEdge = (int)collider2.bounds.min.y;

        return (c1RightEdge == c2LeftEdge) && (c1BottomEdge != c2TopEdge) && (c1TopEdge != c2BottomEdge);
    }

    private bool isLeftCollision(Collider2D collider1, Collider2D collider2)
    {
        int c1LeftEdge = (int)collider1.bounds.min.x;

        int c1TopEdge = (int)collider1.bounds.max.y;
        int c1BottomEdge = (int)collider1.bounds.min.y;

        int c2RightEdge = (int)collider2.bounds.max.x;

        int c2TopEdge = (int)collider2.bounds.max.y;
        int c2BottomEdge = (int)collider2.bounds.min.y;

        return (c1LeftEdge == c2RightEdge) && (c1BottomEdge != c2TopEdge) && (c1TopEdge != c2BottomEdge);
    }

    public bool isActive()
    {
        return alive && active;
    }

    public void activate()
    {
        active = true;
    }

    public void deactivate()
    {
        active = false;
    }

    // alpha
    public void flash()
    {

    }
}
