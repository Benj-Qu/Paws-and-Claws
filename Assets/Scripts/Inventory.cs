using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    Subscription<BlockSetEvent> block_set_event_subscription;
    
    public List<Card> cards;
    public List<int> amounts;
    public List<TextMeshProUGUI> texts;
    public int cardAddedFront = 0;
    public int cardAddedBack = 0;
    public Selection selection;
    public int whichPlayer;
    public blockController BlockController;

    private bool _doneDim = false;
    private int _selectedIndex = 0;
    private bool _allSet = false;

    private void Awake()
    {
        // subscription
        block_set_event_subscription = EventBus.Subscribe<BlockSetEvent>(OnBlockSetEvent);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (BlockController == null)
        {
            GameObject block = GameObject.Find("Block");
            if (block) BlockController = block.GetComponent<blockController>();
            else
            {
                Debug.LogWarning("no blockController");
            }
        }
        
        cards = new List<Card>();
        amounts = new List<int>();
        // texts = new List<TextMeshProUGUI>();
        
        foreach (Transform child in transform)
        {
            cards.Add(child.GetChild(0).GetComponent<Card>());
            amounts.Add(0);
        }
    }

    
    // after set roll to the next
    private void OnBlockSetEvent(BlockSetEvent e)
    {
        if (GameController.instance.stage == 1)
        {
            if (e.whichPlayer == whichPlayer)
            {
                RollToNextUnset();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.stage == 1 && selection.done && !_doneDim)
        {
            // only called once
            _doneDim = true;
            foreach (Card i in cards)
            {
                i.SetCardBrightness(0.3f);
            }
            cards[0].SetCardBrightness(1f);
            if (BlockController) BlockController.SelectBlock(cards[_selectedIndex].index);
        }

        
        // if (GameController.instance.stage == 1 && whichPlayer == 1 && Keyboard.current.zKey.wasPressedThisFrame)
        // {
        //     Debug.Log("player1 inven z ");
        //     RollToNextUnset();
        // }
        //
        // if (GameController.instance.stage == 1 && whichPlayer == 2 && Keyboard.current.mKey.wasPressedThisFrame)
        // {
        //     RollToNextUnset();
        // }
        
        
        // select the desire block
        if (GameController.instance.stage == 1 && whichPlayer == 1 && Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            // if (BlockController) BlockController.UnSelectBlock(cards[_selectedIndex].index);
            // RotateIndex();
            // if (BlockController) BlockController.SelectBlock(cards[_selectedIndex].index);
            RollToNextUnset();
        }
        
        if (GameController.instance.stage == 1 && whichPlayer == 2 && Keyboard.current.rightShiftKey.wasPressedThisFrame)
        {
            // if (BlockController) BlockController.UnSelectBlock(cards[_selectedIndex].index);
            // RotateIndex();
            // if (BlockController) BlockController.SelectBlock(cards[_selectedIndex].index);
            RollToNextUnset();
        }
    }
    
    // if the current block in the inventory is already set, then roll to the next unset block, if no unset block, all
    // the block in inventory become dim and none of then can be shifted to.
    private void RollToNextUnset()
    {
        if (BlockController)
        {
            int oriSelectedIndex = _selectedIndex;
            // if (BlockController) BlockController.UnSelectBlock(cards[_selectedIndex].index);
            // Debug.Log("here" + BlockController.GetBlockSetStatus(cards[_selectedIndex].index));
            RotateIndex();
            while (BlockController.GetBlockSetStatus(cards[_selectedIndex].index))
            {
                // if back to itself (go through a round all set)
                if (_selectedIndex == oriSelectedIndex)
                {
                    // make them all dim and none of them can be selected
                    foreach (Card card in cards)
                    {
                        card.SetCardBrightness(0f);
                    }

                    _allSet = true;
                    break;
                }
                
                RotateIndex();
            }
            
            if (!_allSet)
            {
                if (BlockController) BlockController.UnSelectBlock(cards[oriSelectedIndex].index);
                if (BlockController) BlockController.SelectBlock(cards[_selectedIndex].index);
            }
        }
    }

    private void RotateIndex()
    {
        Debug.Log("rotate");
        if (BlockController)
        {
            cards[_selectedIndex].SetCardBrightness(0f);
        }

        if (!BlockController.GetBlockSetStatus(cards[_selectedIndex].index))
        {
            cards[_selectedIndex].SetCardBrightness(0.3f);
        }
        
        if (_selectedIndex == cards.Count - 1)
        {
            _selectedIndex = 0;
        }
        else
        {
            _selectedIndex += 1;
        }
        
        if (BlockController)
        {
            cards[_selectedIndex].SetCardBrightness(0f);
        }

        if (!BlockController.GetBlockSetStatus(cards[_selectedIndex].index))
        {
            cards[_selectedIndex].SetCardBrightness(1f);
        }
    }
    
    private void OnDestroy()
    {
        EventBus.Unsubscribe(block_set_event_subscription);
    }
}
