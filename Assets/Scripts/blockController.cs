using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockController : MonoBehaviour
{
    public List<blockMovement> bm;
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
