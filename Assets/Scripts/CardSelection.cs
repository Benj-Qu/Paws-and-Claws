using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Switch;
using UnityEngine.UIElements;

public class CardSelection : MonoBehaviour
{
    // each round player will select one card and give the other one to the other player
    public int round = 2;
    public int whichPlayer;
    public Inventory inventory1;
    public Inventory inventory2;
    public GameObject progressBar;
    public bool roundUp = false;
    public blockController BlockController = null;

    private GameObject _card1;
    private GameObject _card2;
    private Card _card1Script;
    private Card _card2Script;
    private bool _cardAppear = false;
    private int _currentRound = 0;
    private bool _timeUp = false;
    private bool _beginTimeUpSelection = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (BlockController == null)
        {
            Debug.LogWarning("no blockController !");
            GameObject block = GameObject.Find("Block");
            if (block) BlockController = block.GetComponent<blockController>();
        }
        
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "Choice1")
            {
                foreach (Transform child_ in child.gameObject.transform)
                {
                    _card1 = child_.gameObject;
                    _card1Script = _card1.GetComponent<Card>();
                }
            }
            else
            {
                foreach (Transform child_ in child.gameObject.transform)
                {
                    _card2 = child_.gameObject;
                    _card2Script = _card2.GetComponent<Card>();
                }
            }
        }
        
        Round();
    }

    // Update is called once per frame
    private void Update()
    {
        if (roundUp)
        {
            Debug.Log("round up");
            _card1Script.CardDisappear();
            _card2Script.CardDisappear();
            
            // destroy progressbar if existed
            if (progressBar) Destroy(progressBar);
            return;
        }
        
        if (!_cardAppear)
        {
            return;
        }
        
        if (_timeUp)
        {
            if (!_beginTimeUpSelection)
            {
                _beginTimeUpSelection = true;
                StartCoroutine(RoundUpSelection());
            }
            return;
        }

        // left player
        if (whichPlayer == 1)
        {
            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                // TODO: left selected
                Debug.Log("left player left");
                AddToInventory(1, _card1Script.block_id, true);
                AddToInventory(2, _card2Script.block_id, false);
                // next round
                Round();
            }
            else if (Keyboard.current.dKey.wasPressedThisFrame)
            {
                // TODO: right selected
                Debug.Log("left player right");
                AddToInventory(1, _card2Script.block_id, true);
                AddToInventory(2, _card1Script.block_id, false);
                // next round
                Round();
            }
        }
        // right player
        else
        {
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
            {
                // TODO: left selected
                Debug.Log("right player left");
                AddToInventory(2, _card1Script.block_id, true);
                AddToInventory(1, _card2Script.block_id, false);
                // next round
                Round();
            }
            else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                // TODO: right selected
                Debug.Log("right player right");
                AddToInventory(2, _card2Script.block_id, true);
                AddToInventory(1, _card1Script.block_id, false);
                // next round
                Round();
            }
        }
    }

    // TODO: need a animation for selecting the card goes to the player's inventory
    private IEnumerator RoundUpSelection()
    {
        while (_currentRound <= round)
        {
            yield return new WaitForSeconds(1f);
        
            Debug.Log("time up random draw");
            int seed = Random.Range(0, 2);
            if (seed == 0)
            {
                if (whichPlayer == 1)
                {
                    AddToInventory(1, _card1Script.block_id, true);
                    AddToInventory(2, _card2Script.block_id, false);
                }
                else
                {
                    AddToInventory(2, _card1Script.block_id, true);
                    AddToInventory(1, _card2Script.block_id, false);
                }
            }
            else
            {
                if (whichPlayer == 1)
                {
                    AddToInventory(1, _card2Script.block_id, true);
                    AddToInventory(2, _card1Script.block_id, false);
                }
                else
                {
                    AddToInventory(2, _card2Script.block_id, true);
                    AddToInventory(1, _card1Script.block_id, false);
                }
            }
            
            Round();
            yield return new WaitForSeconds(1f);
        }
    }

    private void Round()
    {
        _card1Script.CardDisappear();
        _card2Script.CardDisappear();
        _cardAppear = false;
        _currentRound += 1;
        
        // check if reach the final round
        if (_currentRound > round)
        {
            roundUp = true;
            return;
        }
        StartCoroutine(DrawCard());
    }

    private IEnumerator DrawCard()
    {
        yield return new WaitForSeconds(1f);
        
        // TODO: adjust how the card is drawn from the card pool
        if (_currentRound == 1)
        {
            if (whichPlayer == 1)
            {
                _card1Script.SetCard(0);
                _card2Script.SetCard(1);
            }
            else
            {
                _card1Script.SetCard(4);
                _card2Script.SetCard(5);
            }
        }
        else if (_currentRound == 2)
        {
            if (whichPlayer == 2)
            {
                _card1Script.SetCard(2);
                _card2Script.SetCard(3);
            }
            else
            {
                _card1Script.SetCard(6);
                _card2Script.SetCard(7);
            }
            
        }
        
        _card1Script.CardAppear();
        _card2Script.CardAppear();
        _cardAppear = true;
    }

    public void SetTimeUp()
    {
        _timeUp = true;
    }

    public void AddToInventory(int whichInventory, int blockID, bool atFront)
    {
        if (whichInventory == 1)
        {
            if (BlockController) BlockController.Player1GetBlock(blockID);
            
            if (atFront)
            {
                inventory1.cards[inventory1.cardAddedFront].SetCard(blockID);
                inventory1.cards[inventory1.cardAddedFront].CardAppear();
                // count the pair of card added, only count the one added at the front
                inventory1.cardAddedFront += 1;
            }
            else
            {
                inventory1.cards[inventory1.cards.Count - inventory1.cardAddedBack - 1].SetCard(blockID);
                inventory1.cards[inventory1.cards.Count - inventory1.cardAddedBack - 1].CardAppear();
                inventory1.cardAddedBack += 1;
            }
            
        }
        else
        {
            if (BlockController) BlockController.Player2GetBlock(blockID);
            
            if (atFront)
            {
                inventory2.cards[inventory2.cardAddedFront].SetCard(blockID);
                inventory2.cards[inventory2.cardAddedFront].CardAppear(); 
                inventory2.cardAddedFront += 1;
            }
            else
            {
                inventory2.cards[inventory2.cards.Count - inventory2.cardAddedBack - 1].SetCard(blockID);
                inventory2.cards[inventory2.cards.Count - inventory2.cardAddedBack - 1].CardAppear();
                inventory2.cardAddedBack += 1;
            }
        }
    }
}
