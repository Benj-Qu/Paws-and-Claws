using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool Activate;

    public float Speed;
    public float JumpSpeed;
    public float SlideSpeed;

    public int MaxJumpTimes;
    public float JumpDeactivePeriod;
    public float DieAltitude;

    public float KnockBackPeriod;
    public float speedDecade = 4f;

    public KeyCode LeftButton;
    public KeyCode RightButton;
    public KeyCode JumpButton;

    public AudioClip player_die;

    public ShowAddScore showAddScore;

    private bool alive = true; 
    private bool active = false;
    private bool invincible = false;
    private bool onFloor = true;
    private bool onLeftWall = false;
    private bool onRightWall = false;
    private float floorV = 0f;
    private int jumpTimes;
    private bool onIce = false;

    public float DeathPenalty = -5f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private GameController gc;

    private void Start()
    {
        jumpTimes = MaxJumpTimes;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        if (gc.stage == 2)
        {
            active = true;
        }
        if (Activate)
        {
            active = true;
        }
    }

    private void Update()
    {
        if (isActive())
        {
            UpdateVelocity();
        }
        if (alive && gameObject.transform.position.y < DieAltitude)
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
            if (collision.gameObject.CompareTag("Ice"))
            {
                onIce = true;
            }
            else
            {
                onIce = false;
            }

            ContactPoint2D hitpos = collision.GetContact(0);
            // Touch Floor
            if (hitpos.normal.y > 0)
            {
                onFloor = true;
                jumpTimes = MaxJumpTimes;
                if (collision.rigidbody)
                {
                    floorV = collision.rigidbody.velocity.x;
                }
                else
                {
                    floorV = 0f;
                }
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
                if (collision.rigidbody)
                {
                    floorV = collision.rigidbody.velocity.x;
                }
                else
                {
                    floorV = 0f;
                }
            }
            // Knock On Left Player
            else if (hitpos.normal.x > 0)
            {
                PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
                if (Input.GetKey(pc.RightButton))
                {
                    float coef = collision.transform.localScale.x / gameObject.transform.localScale.x;
                    StartCoroutine(KnockBack(new Vector2(pc.Speed * coef, rb.velocity.y)));
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
                    float coef = collision.transform.localScale.x / gameObject.transform.localScale.x;
                    StartCoroutine(KnockBack(new Vector2(-pc.Speed * coef, rb.velocity.y)));
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
                if (collision.rigidbody)
                {
                    floorV = collision.rigidbody.velocity.x;
                }
                else
                {
                    floorV = 0f;
                }
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
        floorV = 0;
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
            rb.velocity = new Vector2(-Speed + floorV, rb.velocity.y);
            sr.flipX = true;
            if (onRightWall)
            {
                onRightWall = false;
            }
        }
        else if (Input.GetKey(RightButton))
        {
            rb.velocity = new Vector2(Speed + floorV, rb.velocity.y);
            sr.flipX = false;
            if (onLeftWall)
            {
                onLeftWall = false;
            }
        }
        else
        {
            if (!onIce)
            {
                rb.velocity = new Vector2(floorV, rb.velocity.y);
            }
            else
            {
                if (floorV != 0)
                {
                    rb.velocity = new Vector2(floorV, rb.velocity.y);
                }
                else
                {
                    if (rb.velocity.x > 0.1)
                    {
                        rb.velocity -= new Vector2(speedDecade * Time.deltaTime, 0);
                    }
                    else if (rb.velocity.x < -0.1)
                    {
                        rb.velocity += new Vector2(speedDecade * Time.deltaTime, 0);
                    }
                    else
                    {
                        rb.velocity = new Vector2(floorV * Time.deltaTime, 0);

                    }
                }
            }
            //rb.velocity = new Vector2(floorV, rb.velocity.y);
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
        return other.CompareTag("Block") || other.CompareTag("Mountain") || other.CompareTag("Wall") || other.CompareTag("Ice");
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
        showAddScore.ShowScore();
        AudioSource.PlayClipAtPoint(player_die, Camera.main.transform.position);
        StartCoroutine(KilledAnimation());
    }

    private IEnumerator KilledAnimation()
    {
        gameObject.GetComponent<PlayerScore>().updateScore(DeathPenalty);
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
        floorV = 0f;
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
        Debug.Log("Deactivate");
        active = false;
    }

    public void LeaveFloor()
    {
        floorV = 0f;
        onFloor = false;
    }

    public void PowerUp(float period, float SpeedUp, float JumpUp, float SizeUp, bool Invincible)
    {
        StartCoroutine(PowerUpCoroutine(period, SpeedUp, JumpUp, SizeUp, Invincible));
    }

    private IEnumerator PowerUpCoroutine(float period, float SpeedUp, float JumpUp, float SizeUp, bool Invincible)
    {
        Speed *= SpeedUp;
        JumpSpeed *= JumpUp;
        rb.mass *= SizeUp;
        gameObject.transform.localScale *= SizeUp;
        invincible = Invincible;
        yield return new WaitForSeconds(period);
        Speed /= SpeedUp;
        JumpSpeed /= JumpUp;
        invincible = false;
        rb.mass /= SizeUp;
        gameObject.transform.localScale /= SizeUp;
    }
}
