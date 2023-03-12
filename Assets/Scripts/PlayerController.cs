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
    private bool invincible;
    private bool onFloor;
    private bool onLeftWall;
    private bool onRightWall;
    private int jumpTimes;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private GameController gc;

    void Start()
    {
        alive = true;
        active = false;
        invincible = false;
        onFloor = true;
        onLeftWall = false;
        onRightWall = false;
        jumpTimes = MaxJumpTimes;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        if (gc.stage == 2)
        {
            active = true;
        }
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
            ContactPoint2D hitpos = collision.GetContact(0);
            if (hitpos.normal.y > 0)
            {
                Debug.Log("On Floor");
                onFloor = true;
                jumpTimes = MaxJumpTimes;
            }
            else if (hitpos.normal.x > 0)
            {
                Debug.Log("On Left Wall");
                onLeftWall = true;
                jumpTimes = MaxJumpTimes;
            }
            else if (hitpos.normal.x < 0)
            {
                Debug.Log("On Right Wall");
                onRightWall = true;
                jumpTimes = MaxJumpTimes;
            }
            else
            {
                Debug.Log("Ouch!");
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Jump Times
        if (isTerrain(collision))
        {
            ContactPoint2D hitpos = collision.GetContact(0);
            if (hitpos.normal.y > 0)
            {
                Debug.Log("On Floor");
                onFloor = true;
                jumpTimes = MaxJumpTimes;
            }
            else if (hitpos.normal.x > 0)
            {
                Debug.Log("On Left Wall");
                onLeftWall = true;
                jumpTimes = MaxJumpTimes;
            }
            else if (hitpos.normal.x < 0)
            {
                Debug.Log("On Right Wall");
                onRightWall = true;
                jumpTimes = MaxJumpTimes;
            }
            else
            {
                Debug.Log("Ouch!");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onFloor = false;
        onLeftWall = false;
        onRightWall = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!invincible && collision.gameObject.CompareTag("Spike"))
        {
            Die();
        }
    }

    private void UpdateVelocity()
    {
        // Update Horizontal Velocity
        if (Input.GetKey(LeftButton))
        {
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
            sr.flipX = true;
            if (onRightWall)
            {
                onRightWall = false;
            }
        }
        else if (Input.GetKey(RightButton))
        {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
            sr.flipX = false;
            if (onLeftWall)
            {
                onLeftWall = false;
            }
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
        if (onWall() && rb.velocity.y < 0)
        {
            jumpTimes = MaxJumpTimes;
            rb.velocity = new Vector2(0f, -SlideSpeed);
        }
    }

    private bool isTerrain(Collision2D collision)
    {
        return collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Mountain");
    }

    private void jump()
    {
        if (onFloor)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpSpeed);
            jumpTimes--;
            onFloor = false;
        }
        else if (onLeftWall)
        {
            onLeftWall = false;
            rb.velocity = new Vector2(Speed, JumpSpeed);
            sr.flipX = false;
            jumpTimes--;
            StartCoroutine(JumpCoroutine());
        }
        else if (onRightWall)
        {
            onRightWall = false;
            rb.velocity = new Vector2(-Speed, JumpSpeed);
            sr.flipX = true;
            jumpTimes--;
            StartCoroutine(JumpCoroutine());
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
        StartCoroutine(KilledAnimation());
    }

    private IEnumerator KilledAnimation()
    {
        alive = false;
        rb.velocity = Vector2.zero;
        flash();
        yield return new WaitForSeconds(0.2f);
        gc.Killed(gameObject);
        invincible = true;
        yield return new WaitForSeconds(0.3f);
        alive = true;
        onFloor = true;
        onLeftWall = false;
        onRightWall = false;
        jumpTimes = MaxJumpTimes;
        yield return new WaitForSeconds(0.5f);
        invincible = false;
    }

    private bool onWall()
    {
        return (!onFloor) && ((onLeftWall && sr.flipX) || (onRightWall && !sr.flipX));
    }

    private void flash()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        for (int i = 0; i < 5; i++)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.3f);
            yield return new WaitForSeconds(0.1f);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            yield return new WaitForSeconds(0.1f);
        }
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

    // public void SetAlive(bool state)
    // {
    //     alive = state;
    // }

    // alpha
}
