using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class blockMovement : MonoBehaviour
{
    public int block_id;
    public bool block_side; // false: player_1, true: player_2
    public bool selected;
    public bool set = false;
    public bool collisionDetected = false;
    public bool isBomb = false;
    public float alpha = 0.5f;
    public int block_status = 0; // 0: green, >0: red
    public float follower_distance;
    public float follower_distance_x = 0;

    private Vector3 defaultPos = new Vector3(0, 0, (float)-0.15);
    private Collider2D Collider2d;

    private float notRespondTime = 0.08f;
    private float notRespondTimer = 0f;
    private bool respond = true;

    private float joystickInputx = 0f;
    private float joystickInputy = 0f;

    public float UpOffset = 0f;
    public float DownOffset = 0f;
    public float LeftOffset = 0f;
    public float RightOffset = 0f;
    private Subscription<CameraEvent> camera_event;
    private bool cameraIsMoving = false;

    private void Awake()
    {
        camera_event = EventBus.Subscribe<CameraEvent>(OnCameraDone);
    }

    private void OnCameraDone(CameraEvent e)
    {
        cameraIsMoving = e.startOrFinish;
    }


    void Start()
    {
        // initially set to inactive
        Collider2d = GetComponent<BoxCollider2D>();
        gameObject.SetActive(false);
        gameObject.transform.position += GetComponent<BlockInfo>().BornShift;

        // change the block to be half transparent
        if(GetComponent<SpriteRenderer>() != null)
        {
            Color temp = GetComponent<SpriteRenderer>().color;
            temp.a = 0.5f;
            GetComponent<SpriteRenderer>().color = temp;

        }
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            if(transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
            {
                if(transform.GetChild(i).name == "box")
                {
                    transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(0.71f, 0.98f, 0.67f, 0.5f);
                }
                else {
                    Color temp = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
                    temp.a = 0.5f;
                    transform.GetChild(i).GetComponent<SpriteRenderer>().color = temp;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!set)
        {
            if (block_status == 0)
            {
                // block not collide with others, show green box
                if (name == "LanternPoint(Clone)") Debug.Log("LanternPoint(Clone)" + block_status);
                collisionDetected = false;
                for (var i = transform.childCount - 1; i >= 0; i--)
                {
                    if (transform.GetChild(i).name == "box")
                    {
                        transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(0.71f, 0.98f, 0.67f, 0.6f);
                    }
                }
            }
            else if(block_status > 0)
            {
                // block collide with others, show red box
                if (name == "LanternPoint(Clone)") Debug.Log("LanternPoint(Clone)" + block_status);
                collisionDetected = true;
                for (var i = transform.childCount - 1; i >= 0; i--)
                {
                    if (transform.GetChild(i).name == "box")
                    {
                        transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.5f);
                    }
                }
            }
            
            // after the camera movement is done
            if (cameraIsMoving)
            {
                return;
            }
            
            if (block_side == false & selected)
            {
                // player_1 selected blocks
                joystickInputx = Input.GetAxis("Horizontal1");
                joystickInputy = Input.GetAxis("Vertical1");
                if (respond)
                {
                    if (Mathf.Abs(joystickInputx) > Mathf.Abs(joystickInputy))
                    {
                        joystickInputy = 0;
                    }
                    else
                    {
                        joystickInputx = 0;
                    }
                    respond = false;
                }
                else
                {
                    if (joystickInputx != 0 || joystickInputy != 0)
                    {
                        notRespondTimer += Time.deltaTime;
                        if (notRespondTimer >= notRespondTime)
                        {
                            notRespondTimer = 0f;
                            respond = true;
                        }
                    }
                    joystickInputx = 0;
                    joystickInputy = 0;
                }
                
                GameObject player_1_follower = GameObject.Find("DogFollower");
                player_1_follower.transform.position = new Vector3(transform.position.x + follower_distance_x, transform.position.y + follower_distance, transform.position.z);
                if (Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("A1"))
                {
                    Debug.Log("player 1 z block");
                    // added by zeyi
                    if (isBomb || (!isBomb && !collisionDetected))
                    {
                        // press Z to fix the position of this block, can't move any more
                        freezeBlock();
                        EventBus.Publish<BlockSetEvent>(new BlockSetEvent(block_id, 1));
                    } 
                }
                Vector3 newPos = defaultPos;
                bool checkPos = false;
                if (Input.GetKeyDown(KeyCode.A) || joystickInputx < 0)
                {
                    newPos = new Vector3(transform.position.x - (float)0.5, transform.position.y, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.D) || joystickInputx > 0)
                {
                    newPos = new Vector3(transform.position.x + (float)0.5, transform.position.y, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.W) || joystickInputy < 0)
                {
                    newPos = new Vector3(transform.position.x, transform.position.y + (float)0.5, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.S) || joystickInputy > 0)
                {
                    newPos = new Vector3(transform.position.x, transform.position.y - (float)0.5, transform.position.z);
                    checkPos = true;
                }
                if (checkPos == true && validBlockPosition(newPos))
                {
                    transform.position = newPos;
                }
            }
            if (block_side == true & selected)
            {
                //player_2 selected blocks
                joystickInputx = Input.GetAxis("Horizontal2");
                joystickInputy = Input.GetAxis("Vertical2");
                if (respond)
                {
                    if (Mathf.Abs(joystickInputx) > Mathf.Abs(joystickInputy))
                    {
                        joystickInputy = 0;
                    }
                    else
                    {
                        joystickInputx = 0;
                    }
                    respond = false;
                }
                else
                {
                    if (joystickInputx != 0 || joystickInputy != 0)
                    {
                        notRespondTimer += Time.deltaTime;
                        if (notRespondTimer >= notRespondTime)
                        {
                            notRespondTimer = 0f;
                            respond = true;
                        }
                    }

                    joystickInputx = 0;
                    joystickInputy = 0;
                }
                
                GameObject player_2_follower = GameObject.Find("CatFollower");
                player_2_follower.transform.position = new Vector3(transform.position.x + follower_distance_x, transform.position.y + follower_distance, transform.position.z);
                if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonDown("A2"))
                {
                    // added by zeyi
                    if (isBomb || (!isBomb && !collisionDetected))
                    {
                        // press Z to fix the position of this block, can't move any more
                        freezeBlock();
                        EventBus.Publish<BlockSetEvent>(new BlockSetEvent(block_id, 2));
                    } 
                }
                Vector3 newPos = defaultPos;
                bool checkPos = false;
                if (Input.GetKeyDown(KeyCode.LeftArrow) || joystickInputx < 0 )
                {
                    newPos = new Vector3(transform.position.x - (float)0.5, transform.position.y, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) || joystickInputx > 0)
                {
                    newPos = new Vector3(transform.position.x + (float)0.5, transform.position.y, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.UpArrow) || joystickInputy < 0)
                {
                    newPos = new Vector3(transform.position.x, transform.position.y + (float)0.5, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow) || joystickInputy > 0)
                {
                    newPos = new Vector3(transform.position.x, transform.position.y - (float)0.5, transform.position.z);
                    checkPos = true;
                }
                if (checkPos == true && validBlockPosition(newPos))
                {
                    transform.position = newPos;
                }
            }
        }
    }

    private void freezeBlock()
    {
        if (isBomb == true || gameObject.CompareTag("Collectable"))
        {
            set = true;
            // Collider2d.isTrigger = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            // change the block to be solid
            if (GetComponent<SpriteRenderer>() != null)
            {
                Color temp = GetComponent<SpriteRenderer>().color;
                temp.a = 1f;
                GetComponent<SpriteRenderer>().color = temp;

            }
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                if (transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                {
                    if(transform.GetChild(i).name == "box")
                    {
                        transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    }
                    else
                    {
                        Color temp = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
                        temp.a = 1f;
                        transform.GetChild(i).GetComponent<SpriteRenderer>().color = temp;
                    }
                }
            }
        }
        else
        {
            if (!collisionDetected)
            {
                set = true;
                Collider2d.isTrigger = false;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                // change the block to be solid
                if (GetComponent<SpriteRenderer>() != null)
                {
                    Color temp = GetComponent<SpriteRenderer>().color;
                    temp.a = 1f;
                    GetComponent<SpriteRenderer>().color = temp;
                }
                for (var i = transform.childCount - 1; i >= 0; i--)
                {
                    if (transform.GetChild(i).CompareTag("Block") && transform.GetChild(i).GetComponent<BoxCollider2D>() != null)
                    {
                        transform.GetChild(i).GetComponent<BoxCollider2D>().isTrigger = false;
                    }
                    if (transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                    {
                        if (transform.GetChild(i).name == "box")
                        {
                            transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                        }
                        else
                        {
                            Color temp = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
                            temp.a = 1f;
                            transform.GetChild(i).GetComponent<SpriteRenderer>().color = temp;
                        }
                    }
                }
            }
        }
    }

    private bool validBlockPosition(Vector3 pos)
    {
        if(pos.x < (-7 - LeftOffset) || pos.x > (7 + RightOffset) || pos.y < (-4 - DownOffset) || pos.y > (4 + UpOffset))
        {
            // if newPos is out of border, return false
            return false;
        }
        //if(pos.x > -2.5 && pos.x < 2 && pos.y <= -1 && pos.y > -3.5)
        //{
        //    // if collide with the upper mountain, return false
        //    return false;
        //}
        //if(pos.x > -3 && pos.x <= 3 && pos.y <= -3.5)
        //{
        //    // if collide with the lower mountain, return false
        //    return false;
        //}
        return true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // if collide with other blocks
        if (name == "Lantern(Clone)" && other.name == "LanternBox") return;
        if (transform.tag == "Bomb")
        {
            // if current block is bomb, can't be placed on mountain
            if(!set && other.CompareTag("Mountain"))
            {
                block_status++;
                collisionDetected = true;
                for (var i = transform.childCount - 1; i >= 0; i--)
                {
                    if (transform.GetChild(i).name == "box")
                    {
                        transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.5f);
                    }
                }
            }
        }
        else
        {
            // if current block is normal block, can't be placed on mountain, box, block && ice
            if(!set && (other.CompareTag("Block") || other.CompareTag("Box") || other.CompareTag("Mountain") || other.CompareTag("Collectable") || other.CompareTag("Player") || other.CompareTag("Ice")))
            {
                block_status++;
                collisionDetected = true;
                for (var i = transform.childCount - 1; i >= 0; i--)
                {
                    if (transform.GetChild(i).name == "box")
                    {
                        transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.5f);
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // if collide with other blocks
        if (name == "Lantern(Clone)" && other.name == "LanternBox") return;
        if (transform.tag == "Bomb")
        {
            // if current block is bomb, can't be placed on mountain
            if (!set && other.CompareTag("Mountain"))
            {
                collisionDetected = true;
                for (var i = transform.childCount - 1; i >= 0; i--)
                {
                    if (transform.GetChild(i).name == "box")
                    {
                        transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.5f);
                    }
                }
            }
        }
        else
        {
            // if current block is normal block, can't be placed on mountain, box, block && ice
            if (!set && (other.CompareTag("Block") || other.CompareTag("Box") || other.CompareTag("Mountain") || other.CompareTag("Collectable") || other.CompareTag("Player") || other.CompareTag("Ice")))
            {
                collisionDetected = true;
                for (var i = transform.childCount - 1; i >= 0; i--)
                {
                    if (transform.GetChild(i).name == "box")
                    {
                        transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0.5f);
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // if collide with other blocks
        if (name == "Lantern(Clone)" && other.name == "LanternBox") return;
        if (transform.tag == "Bomb")
        {
            // if current block is bomb, can't be placed on mountain
            if (!set && other.CompareTag("Mountain"))
            {
                block_status--;
                collisionDetected = false;
                for (var i = transform.childCount - 1; i >= 0; i--)
                {
                    if (transform.GetChild(i).name == "box")
                    {
                        transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(0.71f, 0.98f, 0.67f, 0.6f);
                    }
                }
            }
        }
        else
        {
            // if current block is normal block, can't be placed on mountain, box, block && ice
            if (!set && (other.CompareTag("Block") || other.CompareTag("Box") || other.CompareTag("Mountain") || other.CompareTag("Collectable") || other.CompareTag("Player") || other.CompareTag("Ice")))
            {
                block_status--;
                collisionDetected = false;
                for (var i = transform.childCount - 1; i >= 0; i--)
                {
                    if (transform.GetChild(i).name == "box")
                    {
                        transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(0.71f, 0.98f, 0.67f, 0.6f);
                    }
                }
            }
        }
    }

    public void Player1GetBlock(int id)
    {
        // mark the block to player_1's
        if (id == block_id) block_side = false;
    }

    public void Player2GetBlock(int id)
    {
        // mark the block to player_2's
        if (id == block_id) block_side = true;
    }

    public void SelectBlock(int i)
    {
        // mark the block to be selected, now can move
        if (i == block_id)
        {
            selected = true;
            // if are selected and not set then setActive true
            if (!set) gameObject.SetActive(true); 
        }
    }

    public void UnSelectBlock(int i)
    {
        // mark the block to be unselected, can't move
        if (i == block_id)
        {
            selected = false;
            // if are unselected and not set then setActive false
            if (!set) gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(camera_event);
    }
}

// added by zeyi, using eventbus
public class BlockSetEvent
{
    public int block_id = 0;
    public int whichPlayer = 1;

    public BlockSetEvent(int _block_id, int _whichPlayer)
    {
        block_id = _block_id;
        whichPlayer = _whichPlayer;
    }

    public override string ToString()
    {
        return "Block id " + block_id + " set ";
    }
}
