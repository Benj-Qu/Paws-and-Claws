using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockMovement : MonoBehaviour
{
    public int block_id;
    public bool block_side; // false: player_1, true: player_2
    public bool selected;
    public bool set = false;
    public bool collisionDetected = false;
    public bool isBomb = false;
    public float alpha = 0.5f;

    private Vector3 defaultPos = new Vector3(0, 0, (float)-0.15);
    private Collider2D Collider2d;

    void Start()
    {
        // initially set to inactive
        Collider2d = GetComponent<BoxCollider2D>();
        gameObject.SetActive(false);

        // change the block to be half transparent
        if(GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

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
                    transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!set)
        {
            if (block_side == false & selected)
            {
                // player_1 selected blocks
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    Debug.Log("player 1 z block");
                    // press Z to fix the position of this block, can't move any more
                    freezeBlock();
                    // added by zeyi
                    if (isBomb || (!isBomb && !collisionDetected)) EventBus.Publish<BlockSetEvent>(new BlockSetEvent(block_id, 1)); 
                }
                Vector3 newPos = defaultPos;
                bool checkPos = false;
                if (Input.GetKeyDown(KeyCode.A))
                {
                    newPos = new Vector3(transform.position.x - (float)0.5, transform.position.y, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    newPos = new Vector3(transform.position.x + (float)0.5, transform.position.y, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    newPos = new Vector3(transform.position.x, transform.position.y + (float)0.5, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.S))
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
                if (Input.GetKeyDown(KeyCode.M))
                {
                    // press Z to fix the position of this block, can't move any more
                    freezeBlock();
                    // added by zeyi
                    if (isBomb || (!isBomb && !collisionDetected)) EventBus.Publish<BlockSetEvent>(new BlockSetEvent(block_id, 2)); 
                }
                Vector3 newPos = defaultPos;
                bool checkPos = false;
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    newPos = new Vector3(transform.position.x - (float)0.5, transform.position.y, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    newPos = new Vector3(transform.position.x + (float)0.5, transform.position.y, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    newPos = new Vector3(transform.position.x, transform.position.y + (float)0.5, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
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
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

            }
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                if (transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                {
                    transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
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
                    GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

                }
                for (var i = transform.childCount - 1; i >= 0; i--)
                {
                    if (transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                    {
                        transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    }
                }
            }
        }
    }

    private bool validBlockPosition(Vector3 pos)
    {
        if(pos.x < -7 || pos.x > 7 || pos.y < -4 || pos.y > 4)
        {
            // if newPos is out of border, return false
            return false;
        }
        if(pos.x > -2.5 && pos.x < 2 && pos.y <= -1 && pos.y > -3.5)
        {
            // if collide with the upper mountain, return false
            return false;
        }
        if(pos.x > -3 && pos.x <= 3 && pos.y <= -3.5)
        {
            // if collide with the lower mountain, return false
            return false;
        }
        return true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // if collide with other blocks
        if (!set && (other.CompareTag("Block") || other.CompareTag("Box")))
        {
            collisionDetected = true;
            if(transform.tag != "Bomb")
            {
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
        if (!set && (other.CompareTag("Block") || other.CompareTag("Box")))
        {
            collisionDetected = true;
            if (transform.tag != "Bomb")
            {
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
        if (!set && (other.CompareTag("Block") || other.CompareTag("Box")))
        {
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
