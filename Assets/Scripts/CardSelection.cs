using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CardSelection : MonoBehaviour
{
    // each round player will select one card and give the other one to the other player
    public int round = 2;
    public int whichPlayer;

    private GameObject _card1;
    private GameObject _card2;

    private Card _card1Script;
    private Card _card2Script;
    
    private bool _cardAppear = false;
    private int _currentRound = 0;
    private bool _timeUp = false;
    private bool _roundUp = false;

    // Start is called before the first frame update
    void Start()
    {
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
        if (_timeUp)
        {
            // TODO: randomly done the remaining selection
            // TODO: need a animation for selecting the card goes to the player's inventory
            Debug.Log("time up random draw");
            int seed = Random.Range(0, 2);
            if (seed == 0)
            {
                Debug.Log("draw left");
            }
            else
            {
                Debug.Log("draw right");
            }
            
            _card1Script.CardDisappear();
            _card2Script.CardDisappear();
            return;
        }

        if (_roundUp)
        {
            Debug.Log("round up");
            _card1Script.CardDisappear();
            _card2Script.CardDisappear();
            return;
        }
        
        if (!_cardAppear)
        {
            return;
        }

        // left player
        if (whichPlayer == 1)
        {
            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                // TODO: left selected
                Debug.Log("left player left");
                Round();
            }
            else if (Keyboard.current.dKey.wasPressedThisFrame)
            {
                // TODO: right selected
                Debug.Log("left player right");
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
                Round();
            }
            else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
            {
                // TODO: right selected
                Debug.Log("right player right");
                Round();
            }
        }
    }

    private void Round()
    {
        _cardAppear = false;
        _card1Script.CardDisappear();
        _card2Script.CardDisappear();
        _currentRound += 1;
        
        // check if reach the final round
        if (_currentRound > round)
        {
            _roundUp = true;
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
            _card1Script.SetCard(0);
            _card2Script.SetCard(1);
        }
        else if (_currentRound == 2)
        {
            _card1Script.SetCard(2);
            _card2Script.SetCard(3);
        }

        _cardAppear = true;
        _card1Script.CardAppear();
        _card2Script.CardAppear();
    }

    public void SetTimeUp()
    {
        _timeUp = true;
    }
}
