using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Switch;
using UnityEngine.UIElements;

public class CardSelection : MonoBehaviour
{
    // TODO: use onAwake to automatically added block prefab (with predetermined index) as the children of block, no longer need to manually add
    // each round player will select one card and give the other one to the other player
    public int round = 2; // 1-index
    public int whichPlayer;
    public Inventory inventory1;
    public Inventory inventory2;
    public GameObject progressBar;
    public bool roundUp = false;
    public blockController BlockController = null;
    public CardSelectionController cardSelectionController;

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
            GameObject block = GameObject.Find("Block");
            if (block) BlockController = block.GetComponent<blockController>();
            else
            {
                Debug.LogWarning("no blockController !");
            }
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
            else if (child.gameObject.name == "Choice2")
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
            _card1Script.CardDisappear();
            _card2Script.CardDisappear();
            cardSelectionController.Disappear();
            
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
            // float joystickInput = Input.GetAxis("Horizontal1");
            if (Input.GetButtonDown("A1") || Keyboard.current.zKey.wasPressedThisFrame)
            {
                // if (Keyboard.current.aKey.wasPressedThisFrame || joystickInput < 0)
                if (cardSelectionController.activeButton == 0)
                {
                    // TODO: left selected
                    AddToInventory(1, _card1Script.block_id, true, _card1Script.index);
                    AddToInventory(2, _card2Script.block_id, false, _card2Script.index);
                    // next round
                    Round();
                }
                // else if (Keyboard.current.dKey.wasPressedThisFrame || joystickInput > 0)
                else
                {
                    // TODO: right selected
                    AddToInventory(1, _card2Script.block_id, true, _card2Script.index);
                    AddToInventory(2, _card1Script.block_id, false, _card1Script.index);
                    // next round
                    Round();
                }
                cardSelectionController.Reset();
            }
        }
        // right player
        else
        {
            // float joystickInput = Input.GetAxis("Horizontal2");
            if (Input.GetButtonDown("A2") || Keyboard.current.mKey.wasPressedThisFrame)
            {
                // if (Keyboard.current.leftArrowKey.wasPressedThisFrame || joystickInput < 0)
                if (cardSelectionController.activeButton == 0)
                {
                    // TODO: left selected
                    AddToInventory(2, _card1Script.block_id, true, _card1Script.index);
                    AddToInventory(1, _card2Script.block_id, false, _card2Script.index);
                    // next round
                    Round();
                }
                // else if (Keyboard.current.rightArrowKey.wasPressedThisFrame || joystickInput > 0)
                else
                {
                    // TODO: right selected
                    AddToInventory(2, _card2Script.block_id, true, _card2Script.index);
                    AddToInventory(1, _card1Script.block_id, false, _card1Script.index);
                    // next round
                    Round();
                }
                cardSelectionController.Reset();
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
                    AddToInventory(1, _card1Script.block_id, true, _card1Script.index);
                    AddToInventory(2, _card2Script.block_id, false, _card2Script.index);
                }
                else
                {
                    AddToInventory(2, _card1Script.block_id, true, _card1Script.index);
                    AddToInventory(1, _card2Script.block_id, false, _card2Script.index);
                }
            }
            else
            {
                if (whichPlayer == 1)
                {
                    AddToInventory(1, _card2Script.block_id, true, _card2Script.index);
                    AddToInventory(2, _card1Script.block_id, false, _card1Script.index);
                }
                else
                {
                    AddToInventory(2, _card2Script.block_id, true, _card2Script.index);
                    AddToInventory(1, _card1Script.block_id, false, _card1Script.index);
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
            StartCoroutine(DelayRoundUp());
            return;
        }
        StartCoroutine(DrawCard());
    }

    private IEnumerator DelayRoundUp()
    {
        yield return new WaitForSeconds(0.75f);
        roundUp = true;
    }

    // Use index to identify each card (even the id is the same, indicating the same kind of card)
    private IEnumerator DrawCard()
    {
        yield return new WaitForSeconds(1f);
        
        // TODO: adjust how the card is drawn from the card pool
        List<CardRound> r = new List<CardRound>();
        r = AllCards.cardRoundSetting[GameController.instance.round_big - 1][_currentRound - 1];

        foreach (CardRound i in r)
        {
            if (i.whichPlayer == whichPlayer)
            {
                if (i.leftOrRight == 1) _card1Script.SetIndexAndWhichCard(i.whichCard, i.index);
                else _card2Script.SetIndexAndWhichCard(i.whichCard, i.index);
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
    
    public void AddToInventory(int whichInventory, int blockID, bool atFront, int cardIndex)
    {
        if (whichInventory == 1)
        {
            if (BlockController) BlockController.Player1GetBlock(cardIndex);

            if (atFront)
            {
                inventory1.cards[inventory1.cardAddedFront].StartSelectEffect(1, _card1.transform.position);
                inventory1.cards[inventory1.cardAddedFront].SetCard(blockID);
                inventory1.cards[inventory1.cardAddedFront].index = cardIndex;
                inventory1.cards[inventory1.cardAddedFront].CardAppear();
                // count the pair of card added, only count the one added at the front
                inventory1.cardAddedFront += 1;
            }
            else
            {
                inventory1.cards[inventory1.cards.Count - inventory1.cardAddedBack - 1].StartSelectEffect(1, _card1.transform.position);
                inventory1.cards[inventory1.cards.Count - inventory1.cardAddedBack - 1].SetCard(blockID);
                inventory1.cards[inventory1.cards.Count - inventory1.cardAddedBack - 1].index = cardIndex;
                inventory1.cards[inventory1.cards.Count - inventory1.cardAddedBack - 1].CardAppear();
                inventory1.cardAddedBack += 1;
            }
            
        }
        else
        {
            if (BlockController) BlockController.Player2GetBlock(cardIndex);
            
            if (atFront)
            {
                inventory2.cards[inventory2.cardAddedFront].StartSelectEffect(2, _card2.transform.position);
                inventory2.cards[inventory2.cardAddedFront].SetCard(blockID);
                inventory2.cards[inventory2.cardAddedFront].index = cardIndex;
                inventory2.cards[inventory2.cardAddedFront].CardAppear(); 
                inventory2.cardAddedFront += 1;
            }
            else
            {
                inventory2.cards[inventory2.cards.Count - inventory2.cardAddedBack - 1].StartSelectEffect(2, _card2.transform.position);
                inventory2.cards[inventory2.cards.Count - inventory2.cardAddedBack - 1].SetCard(blockID);
                inventory2.cards[inventory2.cards.Count - inventory2.cardAddedBack - 1].index = cardIndex;
                inventory2.cards[inventory2.cards.Count - inventory2.cardAddedBack - 1].CardAppear();
                inventory2.cardAddedBack += 1;
            }
        }
    }
}
