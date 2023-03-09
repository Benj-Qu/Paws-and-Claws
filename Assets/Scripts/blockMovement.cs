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

    private Vector3 defaultPos = new Vector3(0, 0, (float)-0.15);

    void Start()
    {
        // initially set to inactive 
        gameObject.SetActive(false);
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
                    // press Z to fix the position of this block, can't move any more
                    if (!collisionDetected)
                    {
                        set = true;
                    }
                }
                Vector3 newPos = defaultPos;
                bool checkPos = false;
                if (Input.GetKeyDown(KeyCode.A))
                {
                    newPos = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    newPos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    newPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    newPos = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
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
                    if (!collisionDetected)
                    {
                        set = true;
                    }
                }
                Vector3 newPos = defaultPos;
                bool checkPos = false;
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    newPos = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    newPos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    newPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                    checkPos = true;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    newPos = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                    checkPos = true;
                }
                if (checkPos == true && validBlockPosition(newPos))
                {
                    transform.position = newPos;
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
        if(pos.x >= -3 && pos.x <= 2 && pos.y <= -1)
        {
            // if collide with the mountain, return false
            return false;
        }
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if collide with other blocks
        if (other.CompareTag("Block"))
        {
            collisionDetected = true;
        }
    }

    //private void OnTriggerStay(Collider other)
    //{

    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            collisionDetected = false;
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
