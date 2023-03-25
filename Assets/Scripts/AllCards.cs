using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllCards : MonoBehaviour
{
    Subscription<BigRoundIncEvent> big_round_inc_event_subscription;
    
    // id, name
    public static Dictionary<int, string> cards;
    
    
    // big round
    // small round 
    
    // whichcard
    // left or right
    // index assigned to uniquely find each of the card each big round
    public static List<List<List<CardRound>>> cardRoundSetting;

    public GameObject block;
    private GameObject _selectionPanel;

    private void Awake()
    {
  
        big_round_inc_event_subscription = EventBus.Subscribe<BigRoundIncEvent>(OnRoundInc);

        _selectionPanel = Resources.Load<GameObject>("Prefab/SelectionPanel");
        // GameController.instance.selectionPanel = Instantiate(_selectionPanel, transform.position, Quaternion.identity);
        cards = new Dictionary<int, string>();
        
        cards.Add(0, "crowAttack"); // TODO: only works for single sprite (not a split sprite inside a large sprite)
        cards.Add(1, "hideSpikeBlock");
        cards.Add(2, "rectangle");
        cards.Add(3, "square");
        cards.Add(4, "bomb");
        cards.Add(5, "Coin");
        cards.Add(6, "Power Potion");
        cards.Add(7, "Speed Potion");
        cards.Add(8, "Invincible Potion");
        
        
        // instantiate the blocks as the child of the block
        block = GameObject.Find("Block");
        if (block)
        {
            SetRound();
            // // set the first round block as the child of block
            // foreach (List<CardRound> s in cardRoundSetting[0])
            // {
            //     foreach (CardRound i in s)
            //     {
            //         SetEachCardUnderBlock(i.whichCard, i.index);
            //     }
            // }
        }
    } // after that the start func of blockcontroller will be called and load all the blocks.

    private void OnRoundInc(BigRoundIncEvent e)
    {
        // set the round block as the child of block
        foreach (List<CardRound> s in cardRoundSetting[e.round_big - 1])
        {
            foreach (CardRound i in s)
            {
                SetEachCardUnderBlock(i.whichCard, i.index);
            }
        }

        GameController.instance.selectionPanel = Instantiate(_selectionPanel, transform.position, Quaternion.identity);
        // Instantiate(_selectionPanel, transform.position, Quaternion.identity);
        EventBus.Publish<BlockInstantiateEvent>(new BlockInstantiateEvent());
    }
    
    private void SetRound()
    {
        // big round 1
            // small round 1
        CardRound cardRound1_1 = new CardRound(0, 0,  1, 1);
        CardRound cardRound1_2 = new CardRound(1, 1,  1, 2);
        CardRound cardRound1_3 = new CardRound(4, 2,  2, 1);
        CardRound cardRound1_4 = new CardRound(0, 3,  2, 2);
        List<CardRound> s11 = new List<CardRound>();
        s11.Add(cardRound1_1);
        s11.Add(cardRound1_2);
        s11.Add(cardRound1_3);
        s11.Add(cardRound1_4);
        
        // small round 2
        CardRound cardRound1_5 = new CardRound(2, 4,  1, 1);
        CardRound cardRound1_6 = new CardRound(3, 5,  1, 2);
        CardRound cardRound1_7 = new CardRound(2, 6,  2, 1);
        CardRound cardRound1_8 = new CardRound(3, 7,  2, 2);
        List<CardRound> s12 = new List<CardRound>();
        s12.Add(cardRound1_5);
        s12.Add(cardRound1_6);
        s12.Add(cardRound1_7);
        s12.Add(cardRound1_8);

        List<List<CardRound>> b1 = new List<List<CardRound>>();
        b1.Add(s11);
        b1.Add(s12);
        
        // big round 2
            // small round 1
        CardRound cardRound2_1 = new CardRound(6, 8,  1, 1);
        CardRound cardRound2_2 = new CardRound(5, 9,  1, 2);
        CardRound cardRound2_3 = new CardRound(7, 10,  2, 1);
        CardRound cardRound2_4 = new CardRound(8, 11,  2, 2);
        List<CardRound> s21 = new List<CardRound>();
        s21.Add(cardRound2_1);
        s21.Add(cardRound2_2);
        s21.Add(cardRound2_3);
        s21.Add(cardRound2_4);
        
            // small round 2
        CardRound cardRound2_5 = new CardRound(2, 12, 1, 1);
        CardRound cardRound2_6 = new CardRound(3, 13, 1, 2);
        CardRound cardRound2_7 = new CardRound(4, 14, 2, 1);
        CardRound cardRound2_8 = new CardRound(0, 15, 2, 2);
        List<CardRound> s22 = new List<CardRound>();
        s22.Add(cardRound2_5);
        s22.Add(cardRound2_6);
        s22.Add(cardRound2_7);
        s22.Add(cardRound2_8);
        
        List<List<CardRound>> b2 = new List<List<CardRound>>();
        b2.Add(s21);
        b2.Add(s22);
        
        // big round 3
        // small round 1
        CardRound cardRound3_1 = new CardRound(6, 16,  1, 1);
        CardRound cardRound3_2 = new CardRound(5, 17,  1, 2);
        CardRound cardRound3_3 = new CardRound(7, 18,  2, 1);
        CardRound cardRound3_4 = new CardRound(8, 19,  2, 2);
        List<CardRound> s31 = new List<CardRound>();
        s31.Add(cardRound3_1);
        s31.Add(cardRound3_2);
        s31.Add(cardRound3_3);
        s31.Add(cardRound3_4);
        
        // small round 2
        CardRound cardRound3_5 = new CardRound(2, 20, 1, 1);
        CardRound cardRound3_6 = new CardRound(3, 21, 1, 2);
        CardRound cardRound3_7 = new CardRound(4, 22, 2, 1);
        CardRound cardRound3_8 = new CardRound(0, 23, 2, 2);
        List<CardRound> s32 = new List<CardRound>();
        s32.Add(cardRound3_5);
        s32.Add(cardRound3_6);
        s32.Add(cardRound3_7);
        s32.Add(cardRound3_8);
        
        List<List<CardRound>> b3 = new List<List<CardRound>>();
        b3.Add(s31);
        b3.Add(s32);

        cardRoundSetting = new List<List<List<CardRound>>>();
        cardRoundSetting.Add(b1);
        cardRoundSetting.Add(b2);
        cardRoundSetting.Add(b3);
    }

    private void SetEachCardUnderBlock(int _whichCard, int _index)
    {
        Debug.Log("Prefab/Prefabs/" + AllCards.cards[_whichCard]);
        GameObject temp = Resources.Load<GameObject>("Prefab/Prefabs/" + AllCards.cards[_whichCard]);
        GameObject temp1 = Instantiate(temp, Vector3.zero, Quaternion.identity);
        blockMovement bm = temp1.GetComponent<blockMovement>();
        bm.block_id = _index;
        temp1.transform.SetParent(block.transform);
    }
    
    private void OnDestroy()
    {
        EventBus.Unsubscribe(big_round_inc_event_subscription);
    }
}

public class CardRound
{
    public int whichCard = 0;
    public int index = 0;
    public int whichPlayer = 0; // left as 1, right as 2
    public int leftOrRight = 0;
    
    public CardRound(int _whichCard, int _index, int _whichPlayer, int _leftOrRight)
    {
        whichCard = _whichCard;
        index = _index;
        whichPlayer = _whichPlayer;
        leftOrRight = _leftOrRight;
    }
}

public class BlockInstantiateEvent
{
    public BlockInstantiateEvent()
    {
        return;
    }

    public override string ToString()
    {
        return "Blocks instantiate as the child of Block";
    }
}