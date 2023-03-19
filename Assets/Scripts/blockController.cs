using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class blockController : MonoBehaviour
{
    // added by zeyi
    Subscription<BlockInstantiateEvent> block_instantiate_event_subscription;
    
    public List<blockMovement> bm;
    public int count;

    private bool finished = false;
    // Start is called before the first frame update
    public void Awake()
    {
        block_instantiate_event_subscription = EventBus.Subscribe<BlockInstantiateEvent>(OnBlockInstantiate);
    }

    void OnBlockInstantiate(BlockInstantiateEvent e)
    {
        ReloadBlock();
    }

    // Added by zeyi
    public void ReloadBlock()
    {
        bm.Clear();
        finished = false;
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
            if (set_block_cnt == bm.Count)
            {
                Debug.Log("stage, blockController");
                // if blocks are all set, start game
                GameController.instance.StartGame();
                RemoveBox();
                finished = true;
            }

            count = set_block_cnt;
        }
    }

    public void RemoveBox()
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            // iterate all blocks
            for(var j = transform.GetChild(i).transform.childCount - 1; j >= 0; j--)
            {
                // iterate all block components
                GameObject component = transform.GetChild(i).transform.GetChild(j).gameObject;
                Debug.Log(component.tag);
                if (component.CompareTag("Box"))
                {
                    Debug.Log("HERER");
                    component.SetActive(false);
                }
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
    
    // added by zeyi
    public bool GetBlockSetStatus(int id)
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            if (bm[i].block_id == id)
            {
                return bm[i].set;
            }
        }
        // otherwise has problem:
        Assert.IsTrue(false);
        return true;
    }
    
    private void OnDestroy()
    {
        EventBus.Unsubscribe(block_instantiate_event_subscription);
    }
    
}
