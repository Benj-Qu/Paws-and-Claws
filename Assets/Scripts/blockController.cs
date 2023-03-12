using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockController : MonoBehaviour
{
    public List<blockMovement> bm;

    private bool finished = false;
    // Start is called before the first frame update
    void Start()
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            bm.Add(transform.GetChild(i).GetComponent<blockMovement>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            int set_block_cnt = 0;
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                if (bm[i].set)
                {
                    set_block_cnt += 1;
                }
            }
            if (set_block_cnt == transform.childCount)
            {
                GameController.instance.StartGame();
                finished = true;
                //    for (var i = transform.childCount - 1; i >= 0; i--)
                //    {
                //        transform.GetChild(i).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
                //        transform.GetChild(i).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                //        // transform.GetChild(i).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                //    }
            }
        }
    }

    public void Player1GetBlock(int id)
    {
        // mark the block to player_1's
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            bm[i].Player1GetBlock(id);
        }
    }

    public void Player2GetBlock(int id)
    {
        // mark the block to player_2's
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            bm[i].Player2GetBlock(id);
        }
    }

    public void SelectBlock(int id)
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            bm[i].SelectBlock(id);
        }
    }

    public void UnSelectBlock(int id)
    {
        // mark the block to be unselected, can't move
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            bm[i].UnSelectBlock(id);
        }
    }

}
