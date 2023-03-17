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

    public float KnockBackPeriod;

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
        GameObject other = collision.gameObject;
        // Jump Times
        if (isTerrain(other))
        {
            ContactPoint2D hitpos = collision.GetContact(0);
            // Touch Floor
            if (hitpos.normal.y > 0)
            {
                onFloor = true;
                jumpTimes = MaxJumpTimes;
            }
            // Touch Left Wall
            else if (hitpos.normal.x > 0)
            {
                onLeftWall = true;
                jumpTimes = MaxJumpTimes;
            }
            // Touch Right Wall
            else if (hitpos.normal.x < 0)
            {
                onRightWall = true;
                jumpTimes = MaxJumpTimes;
            }
        }
        else if (isPlayer(other))
        {
            ContactPoint2D hitpos = collision.GetContact(0);
            // Knock On Below Player
            if (hitpos.normal.y > 0)
            {
                onFloor = true;
                jumpTimes = MaxJumpTimes;
            }
            // Knock On Left Player
            else if (hitpos.normal.x > 0)
            {
                PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
                if (Input.GetKey(pc.RightButton))
                {
                    StartCoroutine(KnockBack(new Vector2(pc.Speed, rb.velocity.y)));
                }
                else
                {
                    StartCoroutine(KnockBack(new Vector2(0f, rb.velocity.y)));
                }
            }
            // Knock On Right Player
            else if (hitpos.normal.x < 0)
            {
                PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
                if (Input.GetKey(pc.LeftButton))
                {
                    StartCoroutine(KnockBack(new Vector2(-pc.Speed, rb.velocity.y)));
                }
                else
                {
                    StartCoroutine(KnockBack(new Vector2(0f, rb.velocity.y)));
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        // Jump Times
        if (isTerrain(other))
        {
            ContactPoint2D hitpos = collision.GetContact(0);
            // Touch Floor
            if (hitpos.normal.y > 0)
            {
                onFloor = true;
                jumpTimes = MaxJumpTimes;
            }
            // Touch Left Floor
            else if (hitpos.normal.x > 0)
            {
                onLeftWall = true;
                jumpTimes = MaxJumpTimes;
            }
            // Touch Right Floor
            else if (hitpos.normal.x < 0)
            {
                onRightWall = true;
                jumpTimes = MaxJumpTimes;
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

    private bool isTerrain(GameObject other)
    {
        return other.CompareTag("Block") || other.CompareTag("Mountain");
    }

    private bool isPlayer(GameObject other)
    {
        return other.CompareTag("Player");
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

    public IEnumerator KnockBack(Vector2 direction)
    {
        active = false;
        rb.velocity = direction;
        yield return new WaitForSeconds(KnockBackPeriod);
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
}
