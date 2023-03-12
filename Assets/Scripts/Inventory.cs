using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
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

    // Update is called once per frame
    void Update()
    {
        if (selection.done && !_doneDim)
        {
            _doneDim = true;
            foreach (Card i in cards)
            {
                i.SetCardBrightness(0.3f);
            }
            cards[0].SetCardBrightness(1f);
            if (BlockController) BlockController.SelectBlock(cards[_selectedIndex].index);
        }
        
        // select the desire block
        if (whichPlayer == 1 && Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            if (BlockController) BlockController.UnSelectBlock(cards[_selectedIndex].index);
            RotateIndex();
            if (BlockController) BlockController.SelectBlock(cards[_selectedIndex].index);
        }

        if (whichPlayer == 2 && Keyboard.current.rightShiftKey.wasPressedThisFrame)
        {
            if (BlockController) BlockController.UnSelectBlock(cards[_selectedIndex].index);
            RotateIndex();
            if (BlockController) BlockController.SelectBlock(cards[_selectedIndex].index);
        }
    }

    private void RotateIndex()
    {
        cards[_selectedIndex].SetCardBrightness(0.3f);
        if (_selectedIndex == cards.Count - 1)
        {
            _selectedIndex = 0;
        }
        else
        {
            _selectedIndex += 1;
        }
        cards[_selectedIndex].SetCardBrightness(1f);
    }
}
