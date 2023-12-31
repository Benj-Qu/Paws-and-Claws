using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public bool Activate;

    public float Speed;
    public float JumpSpeed;
    public float SlideSpeed;

    public int MaxJumpTimes;
    public float JumpDeactivePeriod;
    private float DieAltitude = -5.5f;

    public float AirKnockCoef;
    public float KnockBackPeriod;
    public float speedDecade = 4f;

    public float LeftBorder = -8f;
    public float RightBorder = 8f;

    public KeyCode LeftButton;
    public KeyCode RightButton;
    public KeyCode JumpButton;

    public AudioClip player_die;

    public ShowAddScore showAddScore;

    private bool alive = true;
    private bool active = false;
    private bool attacked = false;
    private bool jumping = false;
    private bool invincible = false;
    private bool onFloor = true;
    private bool onLeftWall = false;
    private bool onRightWall = false;
    private bool onCable = false;
    private float floorV = 0f;
    private int jumpTimes;
    private bool onIce = false;

    public float DeathPenalty = -5f;

    public float WallRatio = 3f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private GameController gc;
    private bool collectInvinciblePowerUp = false;
    private AudioSource pas;
    private Animator anim;
    private float sizeup = 1;

    public int joystickNumber;

    // For attack
    public float offset = 100f;
    public float AttackedSpeed = 5f;
    public float AttackedPeriod = 1.2f;
    public float OriginalScale = 0.2f;
    public float MaxRatio = 2f;
    public float MinRatio = 1f;
    public string EnemyName;

    private GameObject enemy;

    private RedFlash rf;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pas = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        enemy = GameObject.Find(EnemyName);
        rf = GetComponent<RedFlash>();
    }

    private void Start()
    {
        jumpTimes = MaxJumpTimes;
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
        else if (!alive)
        {
            rb.velocity = Vector2.zero;
            resetAnim();
        }
        else
        {
            resetAnim();
        }
        // TODO: change for cable, need check
        if (gameObject.transform.position.y < DieAltitude && onCable == false)
        {
            Die();
        }
        RoundWorld();
    }

    private void RoundWorld()
    {
        if (transform.position.x < LeftBorder - 0.1f && onCable == false && transform.position.x > LeftBorder - 0.5f)
        {
            Debug.Log("round");
            transform.position = new Vector2(RightBorder - 0.1f, transform.position.y);
            onFloor = false;
            onLeftWall = false;
            onRightWall = false;
            onIce = false;
            onCable = false;
            floorV = 0;
        }
        if (transform.position.x > RightBorder + 0.1f && onCable == false && transform.position.x < RightBorder + 0.5f)
        {
            Debug.Log("round");
            transform.position = new Vector2(LeftBorder + 0.1f, transform.position.y);
            onFloor = false;
            onLeftWall = false;
            onRightWall = false;
            onIce = false;
            onCable = false;
            floorV = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        // Jump Times
        if (isTerrain(other))
        {
            active = true;
            if (collision.gameObject.CompareTag("Ice"))
            {
                onIce = true;
            }

            if (collision.gameObject.CompareTag("cable"))
            {
                onCable = true;
            }

            ContactPoint2D hitpos = collision.GetContact(0);
            // Touch Floor
            if ((hitpos.normal.y > 0) && (hitpos.normal.y > Mathf.Abs(hitpos.normal.x) / WallRatio))
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
            active = true;
            ContactPoint2D hitpos = collision.GetContact(0);
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            string otherJoystickString = pc.joystickNumber.ToString();
            float otherJoystickInput = Input.GetAxis("Horizontal" + otherJoystickString);
            // Knock On Below Player
            if (hitpos.normal.y > 0 && (hitpos.normal.y > Mathf.Abs(hitpos.normal.x) / WallRatio))
            {
                // If Other Player On Floor
                if (!onFloor && pc.OnFloor())
                {
                    if (collision.rigidbody)
                    {
                        floorV = pc.getFloorV();
                    }
                    else
                    {
                        floorV = 0f;
                    }
                }
                onFloor = true;
                jumpTimes = MaxJumpTimes;
            }
            // Knock On Left Player
            else if (hitpos.normal.x > 0 && !invincible)
            {
                if (Input.GetKey(pc.RightButton) || otherJoystickInput > 0)
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
            else if (hitpos.normal.x < 0 && !invincible)
            {
                if (Input.GetKey(pc.LeftButton) || otherJoystickInput < 0)
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
            if (collision.gameObject.CompareTag("Ice"))
            {
                onIce = true;
            }

            if (collision.gameObject.CompareTag("cable"))
            {
                onCable = true;
            }

            ContactPoint2D hitpos = collision.GetContact(0);
            // Touch Floor
            if (hitpos.normal.y > 0 && (hitpos.normal.y > Mathf.Abs(hitpos.normal.x) / WallRatio))
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
                if (collision.rigidbody)
                {
                    floorV = collision.rigidbody.velocity.x;
                }
            }
            // Touch Right Wall
            else if (hitpos.normal.x < 0)
            {
                onRightWall = true;
                jumpTimes = MaxJumpTimes;
                if (collision.rigidbody)
                {
                    floorV = collision.rigidbody.velocity.x;
                }
            }
        }
        else if (isPlayer(other))
        {
            ContactPoint2D hitpos = collision.GetContact(0);
            if (hitpos.normal.y > 0 && (hitpos.normal.y > Mathf.Abs(hitpos.normal.x) / WallRatio))
            {
                onFloor = true;
                jumpTimes = MaxJumpTimes;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onFloor = false;
        onLeftWall = false;
        onRightWall = false;
        onIce = false;
        onCable = false;
        floorV = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!invincible && collision.gameObject.CompareTag("Spike"))
        {
            Die();
        }
        if (!invincible && collision.gameObject.CompareTag("trigger") && onCable == false)
        {
            Die();
        }
        // Attacked by Player
        if (!invincible && collision.gameObject.CompareTag("Sword"))
        {
            SwordController sword = collision.gameObject.GetComponent<SwordController>();
            if (sword.owner != gameObject)
            {
                Attacked();
            }
        }
    }

    private void UpdateVelocity()
    {
        string joystickString = joystickNumber.ToString();
        float joystickInput = Input.GetAxis("Horizontal" + joystickString) * Speed;
        //Debug.Log("Hor: " + joystickInput);

        // Update Horizontal Velocity
        if (Input.GetKey(LeftButton) || joystickInput < 0)
        {
            anim.SetBool("walk", true);
            if (joystickInput < 0)
            {
                rb.velocity = new Vector2(floorV + joystickInput, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-Speed + floorV, rb.velocity.y);
            }
            sr.flipX = true;
            if (onRightWall)
            {
                onRightWall = false;
            }
        }
        else if (Input.GetKey(RightButton) || joystickInput > 0)
        {
            anim.SetBool("walk", true);
            if (joystickInput < 0)
            {
                rb.velocity = new Vector2(floorV + joystickInput, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Speed + floorV, rb.velocity.y);
            }
            sr.flipX = false;
            if (onLeftWall)
            {
                onLeftWall = false;
            }
        }
        else
        {
            anim.SetBool("walk", false);
            resetAnim();
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
                        rb.velocity = new Vector2(floorV, rb.velocity.y);
                    }
                }
            }
            //rb.velocity = new Vector2(floorV, rb.velocity.y);
        }
        // Handle Jumping
        if ((Input.GetKeyDown(JumpButton) || Input.GetButtonDown("A" + joystickString)) && jumpable())
        // if ((Input.GetKeyDown(JumpButton)) && jumpable())
        {
            Debug.Log("A" + joystickString);
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
        return other.CompareTag("Block") || other.CompareTag("Mountain") || other.CompareTag("Wall") || other.CompareTag("Ice") || other.CompareTag("cable");
    }

    private bool isPlayer(GameObject other)
    {
        return other.CompareTag("Player");
    }

    public void jump()
    {
        if (onFloor)
        {
            anim.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, JumpSpeed);
            jumpTimes--;
            onFloor = false;
        }
        else if (onLeftWall)
        {
            anim.SetTrigger("jump");
            onLeftWall = false;
            rb.velocity = new Vector2(Speed, JumpSpeed);
            sr.flipX = false;
            jumpTimes--;
            StartCoroutine(JumpCoroutine());
        }
        else if (onRightWall)
        {
            anim.SetTrigger("jump");
            onRightWall = false;
            rb.velocity = new Vector2(-Speed, JumpSpeed);
            sr.flipX = true;
            jumpTimes--;
            StartCoroutine(JumpCoroutine());
        }
    }

    public IEnumerator JumpCoroutine()
    {
        jumping = true;
        yield return new WaitForSeconds(JumpDeactivePeriod);
        jumping = false;
    }

    public IEnumerator KnockBack(Vector2 direction)
    {
        active = false;
        redflash(KnockBackPeriod);
        rb.velocity = direction;
        yield return new WaitForSeconds(KnockBackPeriod);
        active = true;
    }

    public bool jumpable()
    {
        return (jumpTimes > 0);
    }

    public void Die()
    {
        if (alive)
        {
            GetComponent<PlayerAttack>().reset();
            showAddScore.ShowScore();
            GetComponent<PlayerScore>().updateScore(DeathPenalty);
            pas.PlayOneShot(player_die, 0.5f);
            Debug.Log("Trying to Deactivate RedFlash");
            StartCoroutine(KilledAnimation());
        }
    }

    private IEnumerator KilledAnimation()
    {
        invincible = true;
        // Stop Red Flashing
        rf.enabled = false;
        // Die and Freeze
        alive = false;
        rb.velocity = Vector2.zero;
        //flash();
        yield return new WaitForSeconds(0.2f);
        GetComponent<Collider2D>().isTrigger = true;
        //var pathname = "Animations/magic_circle";
        //var controllerCurr = this.GetComponent<Animator>().runtimeAnimatorController;
        //this.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(pathname);
        //yield return new WaitForSeconds(0.5f);
        //this.GetComponent<Animator>().runtimeAnimatorController = controllerCurr;
        anim.SetTrigger("die");
        this.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
        StopCoroutine(FlashCoroutine());
        StartCoroutine(Large());
        if (this.name == "player_2")
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        } else
        {
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        // Rebirth and Freeze
        gc.Killed(gameObject);
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        //yield return new WaitForSeconds(0.3f);
    }

    IEnumerator Large()
    {
        for (int i = 0; i < 4; i++)
        {
            Debug.Log("Turning big");
            yield return new WaitForSeconds(0.09f);
            this.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
        }
    }

    public void animReset()
    {
        Debug.Log("--+++++====----");
        StartCoroutine(animreset2());
    }

    IEnumerator animreset2()
    {
        GetComponent<Collider2D>().isTrigger = false;
        reset();
        // Rebirth Invincible
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        invincible = true;
        flash();
        yield return new WaitForSeconds(0.8f);
        invincible = false;
    }

    public void reset()
    {
        alive = true;
        onFloor = true;
        onLeftWall = false;
        onRightWall = false;
        attacked = false;
        jumping = false;
        jumpTimes = MaxJumpTimes;
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        floorV = 0f;
        onIce = false;
        onCable = false;
        rb.velocity = Vector2.zero;
        this.transform.localScale = new Vector3(OriginalScale, OriginalScale, OriginalScale);
        this.transform.localScale *= sizeup;
    }

    public bool OnFloor()
    {
        return onFloor;
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
        for (int i = 0; i < 4; i++)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.3f);
            yield return new WaitForSeconds(0.1f);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public bool isActive()
    {
        return alive && active && !jumping && !attacked;
    }

    public bool isAttackable()
    {
        return alive && active;
    }

    public void activate()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        active = true;
    }

    public void deactivate()
    {
        active = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void LeaveFloor()
    {
        floorV = 0f;
        onFloor = false;
    }

    public bool Flying()
    {
        return !(onFloor || onLeftWall || onRightWall);
    }

    public void Attacked()
    {
        Vector2 direction = getDirection() * getRatio() * AttackedSpeed;
        StartCoroutine(AttackedCoroutine(direction, AttackedPeriod));
    }

    private IEnumerator AttackedCoroutine(Vector2 direction, float time)
    {
        attacked = true;
        active = false;
        rb.velocity = direction;
        redflash(time/2);
        yield return new WaitForSeconds(time/2);
        if (!active && alive)
        {
            redflash(time / 2);
            yield return new WaitForSeconds(time / 2);
            active = true;
        }
        attacked = false;
    }

    private void redflash(float time)
    {
        StartCoroutine(RedFlashCoroutine(time));
    }

    private IEnumerator RedFlashCoroutine(float time)
    {
        rf.enabled = true;
        yield return new WaitForSeconds(time);
        rf.enabled = false;
    }

    public void PowerUp(float period, float SpeedUp, float JumpUp, float SizeUp, bool Invincible)
    {
        StartCoroutine(PowerUpCoroutine(period, SpeedUp, JumpUp, SizeUp, Invincible));
    }

    private IEnumerator PowerUpCoroutine(float period, float SpeedUp, float JumpUp, float SizeUp, bool Invincible)
    {
        sizeup *= SizeUp;
        Speed *= SpeedUp;
        JumpSpeed *= JumpUp;
        rb.mass *= SizeUp;
        transform.localScale *= SizeUp;
        invincible = Invincible;
        if (collectInvinciblePowerUp == false && Invincible)
        {
            collectInvinciblePowerUp = true;
        }
        Debug.Log("Powerup!!");
        yield return new WaitForSeconds(period);
        Debug.Log("Powerup Time Up.");
        Speed /= SpeedUp;
        JumpSpeed /= JumpUp;
        invincible = false;
        rb.mass /= SizeUp;
        transform.localScale /= SizeUp;
        sizeup /= SizeUp;
        if (collectInvinciblePowerUp && invincible == false)
        {
            collectInvinciblePowerUp = false;
        }
    }

    private float getRatio()
    {
        int score = GetComponent<PlayerScore>().getScore();
        int other = enemy.GetComponent<PlayerScore>().getScore();
        float ratio = Mathf.Min(MaxRatio, Mathf.Max((score + offset) / (other + offset), MinRatio));
        return ratio * (enemy.transform.localScale.x / transform.localScale.x);
    }

    private Vector2 getDirection()
    {
        Vector2 distdiff = transform.position - enemy.transform.position;
        if (distdiff.y > 0)
        {
            distdiff = new Vector2(distdiff.x, 2 * distdiff.y);
        }
        return distdiff.normalized;
    }

    public bool GetInvincible()
    {
        return collectInvinciblePowerUp;
    }

    public bool isAttacked()
    {
        return attacked;
    }

    public void cableTrans()
    {
        if (onCable == true)
        {
            this.gameObject.transform.position = new Vector3(2.4f, 5f, 0f);
        }
    }

    public void cableTrans2()
    {
        if (onCable == true)
        {
            this.gameObject.transform.position = new Vector3(-11.3f, -0.2f, 0f);
        }
    }

    public bool GetFlip()
    {
        return sr.flipX;
    }

    public void resetAnim()
    {
        anim.SetTrigger("idle");
    }

    public float getFloorV()
    {
        return floorV;
    }

}
