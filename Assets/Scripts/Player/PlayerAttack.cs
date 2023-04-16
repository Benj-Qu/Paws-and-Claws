using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int joystickNumber;
    public KeyCode FireButton;
    public KeyCode DownButton;

    public float AttackAngle = 45f;
    public float AttackFrequency = 2.5f;

    public float OriginalScale = 0.2f;

    public GameObject Sword;
    public AudioClip attackAudio;

    private float timer = 0f;
    private bool attackable = true;

    private string joystickString;

    // Start is called before the first frame update
    void Start()
    {
        joystickString = joystickNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackable)
        {
            if (((Input.GetAxis("Fire" + joystickString) != 0) || Input.GetKey(FireButton)) 
                && GetComponent<PlayerController>().isAttackable())
            {
                Fire();
            }
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
            AudioSource.PlayClipAtPoint(attackAudio, Camera.main.transform.position);
            attackable = false;
            float horizontalInput = Input.GetAxis("Horizontal" + joystickString);
            float verticalInput = - Input.GetAxis("Vertical" + joystickString);
            // Get Attack Direction
            Vector2 attackDirection = Vector2.zero;
            if (horizontalInput != 0 || verticalInput != 0)
            {
                attackDirection = new Vector2(horizontalInput, verticalInput);
            }
            else if (Input.GetKey(GetComponent<PlayerController>().JumpButton) ||
                     Input.GetKey(GetComponent<PlayerController>().LeftButton) ||
                     Input.GetKey(GetComponent<PlayerController>().RightButton) ||
                     Input.GetKey(DownButton))
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
            else
            {
                if (GetComponent<SpriteRenderer>().flipX)
                {
                    attackDirection = Vector2.left;
                }
                else
                {
                    attackDirection = Vector2.right;
                }
            }
            attackDirection = attackDirection.normalized;
            GameObject sword = Instantiate(Sword, transform.position, Quaternion.identity);
            sword.transform.eulerAngles = new Vector3(0f, 0f, swordDirection(attackDirection));
            sword.transform.localScale *= (transform.localScale.x / OriginalScale);
            sword.GetComponent<SwordController>().Attack(gameObject);
        }
    }

    private float swordDirection(Vector2 attackDirection)
    {
        if (attackDirection.y >= 0)
        {
            return Vector2.Angle(attackDirection, Vector2.right);
        }
        else
        {
            return -Vector2.Angle(attackDirection, Vector2.right);
        }
    }

    public void reset()
    {
        timer = 0f;
        attackable = true;
    }

    public float getTimeRatio()
    {
        if (timer == 0)
        {
            return 0;
        }
        else return 1 - (timer / AttackFrequency);
    }
}
