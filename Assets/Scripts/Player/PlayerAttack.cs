using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int joystickNumber;
    public KeyCode FireButton;
    public string EnemyName;
    public float offset = 100f;
    public float AttackRange = 2f;
    public float AttackAngle = 45f;
    public float AttackFrequency = 2.5f;
    public float KnockSpeed = 5f;
    public float KnockPeriod = 2.5f;

    public KeyCode DownButton;

    private float timer = 0f;
    private bool attackable = true;
    private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find(EnemyName);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackable && GetComponent<PlayerController>().isAttackable())
        {
            Fire();
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > AttackFrequency)
            {
                attackable = true;
                timer = 0;
            }
        }
    }

    private void Fire()
    {
        string joystickString = joystickNumber.ToString();
        if ((Input.GetAxis("Fire" + joystickString) != 0) || Input.GetKey(FireButton))
        {
            Debug.Log("Fire" + joystickString);
            attackable = false;
            float horizontalInput = Input.GetAxis("Horizontal" + joystickString);
            float verticalInput = Input.GetAxis("Vertical" + joystickString);
            // Get Attack Direction
            Vector2 attackDirection = Vector2.zero;
            if (horizontalInput != 0 || verticalInput != 0)
            {
                attackDirection = new Vector2(horizontalInput, verticalInput);
            }
            else
            {
                if (Input.GetKey(GetComponent<PlayerController>().JumpButton))
                {
                    attackDirection += new Vector2(0f, 1f);
                }
                if (Input.GetKey(DownButton))
                {
                    attackDirection += new Vector2(0f, -1f);
                }
                if (Input.GetKey(GetComponent<PlayerController>().LeftButton))
                {
                    attackDirection += new Vector2(-1f, 0f);
                }
                if (Input.GetKey(GetComponent<PlayerController>().RightButton))
                {
                    attackDirection += new Vector2(1f, 0f);
                }
            }
            attackDirection = attackDirection.normalized;
            if (inRange(attackDirection))
            {
                enemy.GetComponent<PlayerController>().KnockBack(getDirection() * getRatio() * KnockSpeed, KnockPeriod);
            }
        }
    }

    private float getRatio()
    {
        int score = GetComponent<PlayerScore>().getScore();
        int other = enemy.GetComponent<PlayerScore>().getScore();
        return Mathf.Max((other + offset) / (score + offset), 1f);
    }

    private float getDistance()
    {
        return Vector2.Distance(transform.position, enemy.transform.position);
    }

    private Vector2 getDirection()
    {
        Vector2 distdiff = enemy.transform.position - transform.position;
        return distdiff.normalized;
    }

    private bool inRange(Vector2 attackDirection)
    {
        if (getDistance() > AttackRange)
        {
            return false;
        }
        if (Vector2.Angle(getDirection(), attackDirection) > AttackAngle)
        {
            return false;
        }
        return true;
    }

    public void reset()
    {
        timer = 0f;
        attackable = true;
    }
}
