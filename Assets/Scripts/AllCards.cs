using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AllCards : MonoBehaviour
{
    Subscription<RoundTextDoneEvent> big_round_inc_event_subscription;
    
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
    private GameObject _selectionPanel_2;

    private void Awake()
    {
  
        big_round_inc_event_subscription = EventBus.Subscribe<RoundTextDoneEvent>(OnRoundInc);

        _selectionPanel = Resources.Load<GameObject>("Prefab/SelectionPanel");
        _selectionPanel_2 = Resources.Load<GameObject>("Prefab/SelectionPanel_2");
        
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
        cards.Add(9, "panda");
        cards.Add(10, "bamboo_steamer");
        cards.Add(11, "Lantern");
        cards.Add(13, "square_china");
        cards.Add(14, "rectangle_china");
        cards.Add(15, "TrapIce");
        cards.Add(16, "Snowman");
        cards.Add(17, "Ice");
        cards.Add(18, "rectangle_snow");
        cards.Add(19, "square_snow");
        cards.Add(20, "penguin");
        cards.Add(21, "rectangle_ice");
        cards.Add(22, "square_ice");
        cards.Add(23, "Trap");
        // crow panda 0-9
        // spike bamboo 1 lantern 1-10 1-11
        // 3-13 2-14



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

    private void OnRoundInc(RoundTextDoneEvent e)
    {
        // set the round block as the child of block
        foreach (List<CardRound> s in cardRoundSetting[e.round_big - 1])
        {
            foreach (CardRound i in s)
            {
                SetEachCardUnderBlock(i.whichCard, i.index);
            }
        }

        int numSmallRound = cardRoundSetting[e.round_big - 1].Count;
        Debug.Log("allcards: " + numSmallRound);
        if (numSmallRound == 2)
        {
            GameController.instance.selectionPanel = Instantiate(_selectionPanel, transform.position, Quaternion.identity);
        }
        else if (numSmallRound == 1)
        {
            GameController.instance.selectionPanel = Instantiate(_selectionPanel_2, transform.position, Quaternion.identity);
        }
        
        // Instantiate(_selectionPanel, transform.position, Quaternion.identity);
        EventBus.Publish<BlockInstantiateEvent>(new BlockInstantiateEvent());
    }
    
    private void SetRound()
    {
        string level = SceneManager.GetActiveScene().name;
        Debug.Log("allcards: " + level);
        if (level == "Lantern Festival")
        {
            // big round 1
            // small round 1
            CardRound cardRoundf1_1 = new CardRound(10, 0,  1, 1);
            CardRound cardRoundf1_2 = new CardRound(10, 1,  1, 2);
            CardRound cardRoundf1_3 = new CardRound(14, 2,  2, 1);
            CardRound cardRoundf1_4 = new CardRound(9, 3,  2, 2);
            List<CardRound> sf11 = new List<CardRound>();
            sf11.Add(cardRoundf1_1);
            sf11.Add(cardRoundf1_2);
            sf11.Add(cardRoundf1_3);
            sf11.Add(cardRoundf1_4);
        
            // small round 2
            CardRound cardRoundf1_5 = new CardRound(14, 4,  1, 1);
            CardRound cardRoundf1_6 = new CardRound(13, 5,  1, 2);
            CardRound cardRoundf1_7 = new CardRound(14, 6,  2, 1);
            CardRound cardRoundf1_8 = new CardRound(13, 7,  2, 2);
            List<CardRound> sf12 = new List<CardRound>();
            sf12.Add(cardRoundf1_5);
            sf12.Add(cardRoundf1_6);
            sf12.Add(cardRoundf1_7);
            sf12.Add(cardRoundf1_8);

            List<List<CardRound>> bf1 = new List<List<CardRound>>();
            bf1.Add(sf11);
            bf1.Add(sf12);
            
            // big round 2
                // small round 1
            CardRound cardRoundf2_1 = new CardRound(6, 8,  1, 1);
            CardRound cardRoundf2_2 = new CardRound(14, 9,  1, 2);
            CardRound cardRoundf2_3 = new CardRound(8, 10,  2, 1);
            CardRound cardRoundf2_4 = new CardRound(11, 11,  2, 2);
            List<CardRound> sf21 = new List<CardRound>();
            sf21.Add(cardRoundf2_1);
            sf21.Add(cardRoundf2_2);
            sf21.Add(cardRoundf2_3);
            sf21.Add(cardRoundf2_4);
            
            //     // small round 2
            // CardRound cardRound2_5 = new CardRound(2, 12, 1, 1);
            // CardRound cardRound2_6 = new CardRound(3, 13, 1, 2);
            // CardRound cardRound2_7 = new CardRound(4, 14, 2, 1);
            // CardRound cardRound2_8 = new CardRound(0, 15, 2, 2);
            // List<CardRound> s22 = new List<CardRound>();
            // s22.Add(cardRound2_5);
            // s22.Add(cardRound2_6);
            // s22.Add(cardRound2_7);
            // s22.Add(cardRound2_8);
            
            List<List<CardRound>> bf2 = new List<List<CardRound>>();
            bf2.Add(sf21);
            // b2.Add(s22);
            
            // big round 3
            // small round 1
            CardRound cardRoundf3_1 = new CardRound(5, 16,  1, 1);
            CardRound cardRoundf3_2 = new CardRound(4, 17,  1, 2);
            CardRound cardRoundf3_3 = new CardRound(7, 18,  2, 1);
            CardRound cardRoundf3_4 = new CardRound(4, 19,  2, 2);
            List<CardRound> sf31 = new List<CardRound>();
            sf31.Add(cardRoundf3_1);
            sf31.Add(cardRoundf3_2);
            sf31.Add(cardRoundf3_3);
            sf31.Add(cardRoundf3_4);
            
            // // small round 2
            // CardRound cardRound3_5 = new CardRound(2, 20, 1, 1);
            // CardRound cardRound3_6 = new CardRound(3, 21, 1, 2);
            // CardRound cardRound3_7 = new CardRound(4, 22, 2, 1);
            // CardRound cardRound3_8 = new CardRound(0, 23, 2, 2);
            // List<CardRound> s32 = new List<CardRound>();
            // s32.Add(cardRound3_5);
            // s32.Add(cardRound3_6);
            // s32.Add(cardRound3_7);
            // s32.Add(cardRound3_8);
            
            List<List<CardRound>> bf3 = new List<List<CardRound>>();
            bf3.Add(sf31);
            // b3.Add(s32);

            cardRoundSetting = new List<List<List<CardRound>>>();
            cardRoundSetting.Add(bf1);
            cardRoundSetting.Add(bf2);
            cardRoundSetting.Add(bf3);
            return;
        }

        if(level == "Winter Land")
        {
            // big round 1
            // small round 1
            CardRound cardRoundf1_1 = new CardRound(16, 0, 1, 1);
            CardRound cardRoundf1_2 = new CardRound(17, 1, 1, 2);
            CardRound cardRoundf1_3 = new CardRound(17, 2, 2, 1);
            CardRound cardRoundf1_4 = new CardRound(16, 3, 2, 2);
            List<CardRound> sf11 = new List<CardRound>();
            sf11.Add(cardRoundf1_1);
            sf11.Add(cardRoundf1_2);
            sf11.Add(cardRoundf1_3);
            sf11.Add(cardRoundf1_4);

            // small round 2
            CardRound cardRoundf1_5 = new CardRound(18, 4, 1, 1);
            CardRound cardRoundf1_6 = new CardRound(19, 5, 1, 2);
            CardRound cardRoundf1_7 = new CardRound(18, 6, 2, 1);
            CardRound cardRoundf1_8 = new CardRound(19, 7, 2, 2);
            List<CardRound> sf12 = new List<CardRound>();
            sf12.Add(cardRoundf1_5);
            sf12.Add(cardRoundf1_6);
            sf12.Add(cardRoundf1_7);
            sf12.Add(cardRoundf1_8);

            List<List<CardRound>> bf1 = new List<List<CardRound>>();
            bf1.Add(sf11);
            bf1.Add(sf12);

            // big round 2
            // small round 1
            CardRound cardRoundf2_1 = new CardRound(6, 8, 1, 1);
            CardRound cardRoundf2_2 = new CardRound(18, 9, 1, 2);
            CardRound cardRoundf2_3 = new CardRound(8, 10, 2, 1);
            CardRound cardRoundf2_4 = new CardRound(15, 11, 2, 2);
            List<CardRound> sf21 = new List<CardRound>();
            sf21.Add(cardRoundf2_1);
            sf21.Add(cardRoundf2_2);
            sf21.Add(cardRoundf2_3);
            sf21.Add(cardRoundf2_4);

            //     // small round 2
            // CardRound cardRound2_5 = new CardRound(2, 12, 1, 1);
            // CardRound cardRound2_6 = new CardRound(3, 13, 1, 2);
            // CardRound cardRound2_7 = new CardRound(4, 14, 2, 1);
            // CardRound cardRound2_8 = new CardRound(0, 15, 2, 2);
            // List<CardRound> s22 = new List<CardRound>();
            // s22.Add(cardRound2_5);
            // s22.Add(cardRound2_6);
            // s22.Add(cardRound2_7);
            // s22.Add(cardRound2_8);

            List<List<CardRound>> bf2 = new List<List<CardRound>>();
            bf2.Add(sf21);
            // b2.Add(s22);

            // big round 3
            // small round 1
            CardRound cardRoundf3_1 = new CardRound(5, 16, 1, 1);
            CardRound cardRoundf3_2 = new CardRound(4, 17, 1, 2);
            CardRound cardRoundf3_3 = new CardRound(7, 18, 2, 1);
            CardRound cardRoundf3_4 = new CardRound(4, 19, 2, 2);
            List<CardRound> sf31 = new List<CardRound>();
            sf31.Add(cardRoundf3_1);
            sf31.Add(cardRoundf3_2);
            sf31.Add(cardRoundf3_3);
            sf31.Add(cardRoundf3_4);

            // // small round 2
            // CardRound cardRound3_5 = new CardRound(2, 20, 1, 1);
            // CardRound cardRound3_6 = new CardRound(3, 21, 1, 2);
            // CardRound cardRound3_7 = new CardRound(4, 22, 2, 1);
            // CardRound cardRound3_8 = new CardRound(0, 23, 2, 2);
            // List<CardRound> s32 = new List<CardRound>();
            // s32.Add(cardRound3_5);
            // s32.Add(cardRound3_6);
            // s32.Add(cardRound3_7);
            // s32.Add(cardRound3_8);

            List<List<CardRound>> bf3 = new List<List<CardRound>>();
            bf3.Add(sf31);
            // b3.Add(s32);

            cardRoundSetting = new List<List<List<CardRound>>>();
            cardRoundSetting.Add(bf1);
            cardRoundSetting.Add(bf2);
            cardRoundSetting.Add(bf3);
            return;
        }
        
         if(level == "Iceland")
        {
            // big round 1
            // small round 1
            CardRound cardRoundf1_1 = new CardRound(20, 0, 1, 1);
            CardRound cardRoundf1_2 = new CardRound(17, 1, 1, 2);
            CardRound cardRoundf1_3 = new CardRound(17, 2, 2, 1);
            CardRound cardRoundf1_4 = new CardRound(20, 3, 2, 2);
            List<CardRound> sf11 = new List<CardRound>();
            sf11.Add(cardRoundf1_1);
            sf11.Add(cardRoundf1_2);
            sf11.Add(cardRoundf1_3);
            sf11.Add(cardRoundf1_4);

            // small round 2
            CardRound cardRoundf1_5 = new CardRound(21, 4, 1, 1);
            CardRound cardRoundf1_6 = new CardRound(22, 5, 1, 2);
            CardRound cardRoundf1_7 = new CardRound(21, 6, 2, 1);
            CardRound cardRoundf1_8 = new CardRound(22, 7, 2, 2);
            List<CardRound> sf12 = new List<CardRound>();
            sf12.Add(cardRoundf1_5);
            sf12.Add(cardRoundf1_6);
            sf12.Add(cardRoundf1_7);
            sf12.Add(cardRoundf1_8);

            List<List<CardRound>> bf1 = new List<List<CardRound>>();
            bf1.Add(sf11);
            bf1.Add(sf12);

            // big round 2
            // small round 1
            CardRound cardRoundf2_1 = new CardRound(6, 8, 1, 1);
            CardRound cardRoundf2_2 = new CardRound(17, 9, 1, 2);
            CardRound cardRoundf2_3 = new CardRound(8, 10, 2, 1);
            CardRound cardRoundf2_4 = new CardRound(15, 11, 2, 2);
            List<CardRound> sf21 = new List<CardRound>();
            sf21.Add(cardRoundf2_1);
            sf21.Add(cardRoundf2_2);
            sf21.Add(cardRoundf2_3);
            sf21.Add(cardRoundf2_4);

            //     // small round 2
            // CardRound cardRound2_5 = new CardRound(2, 12, 1, 1);
            // CardRound cardRound2_6 = new CardRound(3, 13, 1, 2);
            // CardRound cardRound2_7 = new CardRound(4, 14, 2, 1);
            // CardRound cardRound2_8 = new CardRound(0, 15, 2, 2);
            // List<CardRound> s22 = new List<CardRound>();
            // s22.Add(cardRound2_5);
            // s22.Add(cardRound2_6);
            // s22.Add(cardRound2_7);
            // s22.Add(cardRound2_8);

            List<List<CardRound>> bf2 = new List<List<CardRound>>();
            bf2.Add(sf21);
            // b2.Add(s22);

            // big round 3
            // small round 1
            CardRound cardRoundf3_1 = new CardRound(5, 16, 1, 1);
            CardRound cardRoundf3_2 = new CardRound(4, 17, 1, 2);
            CardRound cardRoundf3_3 = new CardRound(7, 18, 2, 1);
            CardRound cardRoundf3_4 = new CardRound(4, 19, 2, 2);
            List<CardRound> sf31 = new List<CardRound>();
            sf31.Add(cardRoundf3_1);
            sf31.Add(cardRoundf3_2);
            sf31.Add(cardRoundf3_3);
            sf31.Add(cardRoundf3_4);

            // // small round 2
            // CardRound cardRound3_5 = new CardRound(2, 20, 1, 1);
            // CardRound cardRound3_6 = new CardRound(3, 21, 1, 2);
            // CardRound cardRound3_7 = new CardRound(4, 22, 2, 1);
            // CardRound cardRound3_8 = new CardRound(0, 23, 2, 2);
            // List<CardRound> s32 = new List<CardRound>();
            // s32.Add(cardRound3_5);
            // s32.Add(cardRound3_6);
            // s32.Add(cardRound3_7);
            // s32.Add(cardRound3_8);

            List<List<CardRound>> bf3 = new List<List<CardRound>>();
            bf3.Add(sf31);
            // b3.Add(s32);

            cardRoundSetting = new List<List<List<CardRound>>>();
            cardRoundSetting.Add(bf1);
            cardRoundSetting.Add(bf2);
            cardRoundSetting.Add(bf3);
            return;
        }

        if (level == "ForAnimation")
        {
            // big round 1
            // small round 1
            CardRound cardRounda1_1 = new CardRound(8, 0, 1, 1);
            CardRound cardRounda1_2 = new CardRound(16, 1, 1, 2);
            CardRound cardRounda1_3 = new CardRound(8, 2, 2, 1);
            CardRound cardRounda1_4 = new CardRound(5, 3, 2, 2);
            List<CardRound> sa11 = new List<CardRound>();
            sa11.Add(cardRounda1_1);
            sa11.Add(cardRounda1_2);
            sa11.Add(cardRounda1_3);
            sa11.Add(cardRounda1_4);

            // // small round 2
            // CardRound cardRoundf1_5 = new CardRound(18, 4, 1, 1);
            // CardRound cardRoundf1_6 = new CardRound(19, 5, 1, 2);
            // CardRound cardRoundf1_7 = new CardRound(18, 6, 2, 1);
            // CardRound cardRoundf1_8 = new CardRound(19, 7, 2, 2);
            // List<CardRound> sf12 = new List<CardRound>();
            // sf12.Add(cardRoundf1_5);
            // sf12.Add(cardRoundf1_6);
            // sf12.Add(cardRoundf1_7);
            // sf12.Add(cardRoundf1_8);

            List<List<CardRound>> ba1 = new List<List<CardRound>>();
            ba1.Add(sa11);
            // bf1.Add(sf12);

            // big round 2
            // small round 1
            // CardRound cardRoundf2_1 = new CardRound(6, 8, 1, 1);
            // CardRound cardRoundf2_2 = new CardRound(18, 9, 1, 2);
            // CardRound cardRoundf2_3 = new CardRound(8, 10, 2, 1);
            // CardRound cardRoundf2_4 = new CardRound(15, 11, 2, 2);
            // List<CardRound> sf21 = new List<CardRound>();
            // sf21.Add(cardRoundf2_1);
            // sf21.Add(cardRoundf2_2);
            // sf21.Add(cardRoundf2_3);
            // sf21.Add(cardRoundf2_4);

            //     // small round 2
            // CardRound cardRound2_5 = new CardRound(2, 12, 1, 1);
            // CardRound cardRound2_6 = new CardRound(3, 13, 1, 2);
            // CardRound cardRound2_7 = new CardRound(4, 14, 2, 1);
            // CardRound cardRound2_8 = new CardRound(0, 15, 2, 2);
            // List<CardRound> s22 = new List<CardRound>();
            // s22.Add(cardRound2_5);
            // s22.Add(cardRound2_6);
            // s22.Add(cardRound2_7);
            // s22.Add(cardRound2_8);

            // List<List<CardRound>> bf2 = new List<List<CardRound>>();
            // bf2.Add(sf21);
            // b2.Add(s22);

            // big round 3
            // small round 1
            // CardRound cardRoundf3_1 = new CardRound(5, 16, 1, 1);
            // CardRound cardRoundf3_2 = new CardRound(4, 17, 1, 2);
            // CardRound cardRoundf3_3 = new CardRound(7, 18, 2, 1);
            // CardRound cardRoundf3_4 = new CardRound(4, 19, 2, 2);
            // List<CardRound> sf31 = new List<CardRound>();
            // sf31.Add(cardRoundf3_1);
            // sf31.Add(cardRoundf3_2);
            // sf31.Add(cardRoundf3_3);
            // sf31.Add(cardRoundf3_4);

            // // small round 2
            // CardRound cardRound3_5 = new CardRound(2, 20, 1, 1);
            // CardRound cardRound3_6 = new CardRound(3, 21, 1, 2);
            // CardRound cardRound3_7 = new CardRound(4, 22, 2, 1);
            // CardRound cardRound3_8 = new CardRound(0, 23, 2, 2);
            // List<CardRound> s32 = new List<CardRound>();
            // s32.Add(cardRound3_5);
            // s32.Add(cardRound3_6);
            // s32.Add(cardRound3_7);
            // s32.Add(cardRound3_8);

            // List<List<CardRound>> bf3 = new List<List<CardRound>>();
            // bf3.Add(sf31);
            // // b3.Add(s32);

            cardRoundSetting = new List<List<List<CardRound>>>();
            cardRoundSetting.Add(ba1);
            // cardRoundSetting.Add(bf2);
            // cardRoundSetting.Add(bf3);
            return;
        }
        if (level == "hyb_anim")
        {
            // big round 1
            // small round 1
            CardRound cardRounda1_1 = new CardRound(1, 0, 1, 1);
            CardRound cardRounda1_2 = new CardRound(0, 1, 1, 2);
            CardRound cardRounda1_3 = new CardRound(9, 2, 2, 1);
            CardRound cardRounda1_4 = new CardRound(10, 3, 2, 2);
            List<CardRound> sa11 = new List<CardRound>();
            sa11.Add(cardRounda1_1);
            sa11.Add(cardRounda1_2);
            sa11.Add(cardRounda1_3);
            sa11.Add(cardRounda1_4);

            // // small round 2
            // CardRound cardRoundf1_5 = new CardRound(18, 4, 1, 1);
            // CardRound cardRoundf1_6 = new CardRound(19, 5, 1, 2);
            // CardRound cardRoundf1_7 = new CardRound(18, 6, 2, 1);
            // CardRound cardRoundf1_8 = new CardRound(19, 7, 2, 2);
            // List<CardRound> sf12 = new List<CardRound>();
            // sf12.Add(cardRoundf1_5);
            // sf12.Add(cardRoundf1_6);
            // sf12.Add(cardRoundf1_7);
            // sf12.Add(cardRoundf1_8);

            List<List<CardRound>> ba1 = new List<List<CardRound>>();
            ba1.Add(sa11);
            // bf1.Add(sf12);

            // big round 2
            // small round 1
            // CardRound cardRoundf2_1 = new CardRound(6, 8, 1, 1);
            // CardRound cardRoundf2_2 = new CardRound(18, 9, 1, 2);
            // CardRound cardRoundf2_3 = new CardRound(8, 10, 2, 1);
            // CardRound cardRoundf2_4 = new CardRound(15, 11, 2, 2);
            // List<CardRound> sf21 = new List<CardRound>();
            // sf21.Add(cardRoundf2_1);
            // sf21.Add(cardRoundf2_2);
            // sf21.Add(cardRoundf2_3);
            // sf21.Add(cardRoundf2_4);

            //     // small round 2
            // CardRound cardRound2_5 = new CardRound(2, 12, 1, 1);
            // CardRound cardRound2_6 = new CardRound(3, 13, 1, 2);
            // CardRound cardRound2_7 = new CardRound(4, 14, 2, 1);
            // CardRound cardRound2_8 = new CardRound(0, 15, 2, 2);
            // List<CardRound> s22 = new List<CardRound>();
            // s22.Add(cardRound2_5);
            // s22.Add(cardRound2_6);
            // s22.Add(cardRound2_7);
            // s22.Add(cardRound2_8);

            // List<List<CardRound>> bf2 = new List<List<CardRound>>();
            // bf2.Add(sf21);
            // b2.Add(s22);

            // big round 3
            // small round 1
            // CardRound cardRoundf3_1 = new CardRound(5, 16, 1, 1);
            // CardRound cardRoundf3_2 = new CardRound(4, 17, 1, 2);
            // CardRound cardRoundf3_3 = new CardRound(7, 18, 2, 1);
            // CardRound cardRoundf3_4 = new CardRound(4, 19, 2, 2);
            // List<CardRound> sf31 = new List<CardRound>();
            // sf31.Add(cardRoundf3_1);
            // sf31.Add(cardRoundf3_2);
            // sf31.Add(cardRoundf3_3);
            // sf31.Add(cardRoundf3_4);

            // // small round 2
            // CardRound cardRound3_5 = new CardRound(2, 20, 1, 1);
            // CardRound cardRound3_6 = new CardRound(3, 21, 1, 2);
            // CardRound cardRound3_7 = new CardRound(4, 22, 2, 1);
            // CardRound cardRound3_8 = new CardRound(0, 23, 2, 2);
            // List<CardRound> s32 = new List<CardRound>();
            // s32.Add(cardRound3_5);
            // s32.Add(cardRound3_6);
            // s32.Add(cardRound3_7);
            // s32.Add(cardRound3_8);

            // List<List<CardRound>> bf3 = new List<List<CardRound>>();
            // bf3.Add(sf31);
            // // b3.Add(s32);

            cardRoundSetting = new List<List<List<CardRound>>>();
            cardRoundSetting.Add(ba1);
            // cardRoundSetting.Add(bf2);
            // cardRoundSetting.Add(bf3);
            return;
        }

        if(level == "Farm")
        {
            Debug.Log("Farm");
            // big round 1
            // small round 1
            CardRound cardRoundfarm_1 = new CardRound(0, 0, 1, 1);
            CardRound cardRoundfarm_2 = new CardRound(1, 1, 1, 2);
            CardRound cardRoundfarm_3 = new CardRound(4, 2, 2, 1);
            CardRound cardRoundfarm_4 = new CardRound(0, 3, 2, 2);
            List<CardRound> sfarm11 = new List<CardRound>();
            sfarm11.Add(cardRoundfarm_1);
            sfarm11.Add(cardRoundfarm_2);
            sfarm11.Add(cardRoundfarm_3);
            sfarm11.Add(cardRoundfarm_4);

            // small round 2
            CardRound cardRoundfarm_5 = new CardRound(2, 4, 1, 1);
            CardRound cardRoundfarm_6 = new CardRound(3, 5, 1, 2);
            CardRound cardRoundfarm_7 = new CardRound(2, 6, 2, 1);
            CardRound cardRoundfarm_8 = new CardRound(3, 7, 2, 2);
            List<CardRound> sfarm12 = new List<CardRound>();
            sfarm12.Add(cardRoundfarm_5);
            sfarm12.Add(cardRoundfarm_6);
            sfarm12.Add(cardRoundfarm_7);
            sfarm12.Add(cardRoundfarm_8);

            List<List<CardRound>> bfarm1 = new List<List<CardRound>>();
            bfarm1.Add(sfarm11);
            bfarm1.Add(sfarm12);

            // big round 2
            // small round 1
            CardRound cardRoundfarm2_1 = new CardRound(6, 8, 1, 1);
            CardRound cardRoundfarm2_2 = new CardRound(2, 9, 1, 2);
            CardRound cardRoundfarm2_3 = new CardRound(8, 10, 2, 1);
            CardRound cardRoundfarm2_4 = new CardRound(1, 11, 2, 2);
            List<CardRound> sfarm21 = new List<CardRound>();
            sfarm21.Add(cardRoundfarm2_1);
            sfarm21.Add(cardRoundfarm2_2);
            sfarm21.Add(cardRoundfarm2_3);
            sfarm21.Add(cardRoundfarm2_4);

            //     // small round 2
            // CardRound cardRound2_5 = new CardRound(2, 12, 1, 1);
            // CardRound cardRound2_6 = new CardRound(3, 13, 1, 2);
            // CardRound cardRound2_7 = new CardRound(4, 14, 2, 1);
            // CardRound cardRound2_8 = new CardRound(0, 15, 2, 2);
            // List<CardRound> s22 = new List<CardRound>();
            // s22.Add(cardRound2_5);
            // s22.Add(cardRound2_6);
            // s22.Add(cardRound2_7);
            // s22.Add(cardRound2_8);

            List<List<CardRound>> bfarm2 = new List<List<CardRound>>();
            bfarm2.Add(sfarm21);
            // b2.Add(s22);

            // big round 3
            // small round 1
            CardRound cardRoundfarm3_1 = new CardRound(5, 16, 1, 1);
            CardRound cardRoundfarm3_2 = new CardRound(4, 17, 1, 2);
            CardRound cardRoundfarm3_3 = new CardRound(7, 18, 2, 1);
            CardRound cardRoundfarm3_4 = new CardRound(4, 19, 2, 2);
            List<CardRound> sfarm31 = new List<CardRound>();
            sfarm31.Add(cardRoundfarm3_1);
            sfarm31.Add(cardRoundfarm3_2);
            sfarm31.Add(cardRoundfarm3_3);
            sfarm31.Add(cardRoundfarm3_4);

            // // small round 2
            // CardRound cardRound3_5 = new CardRound(2, 20, 1, 1);
            // CardRound cardRound3_6 = new CardRound(3, 21, 1, 2);
            // CardRound cardRound3_7 = new CardRound(4, 22, 2, 1);
            // CardRound cardRound3_8 = new CardRound(0, 23, 2, 2);
            // List<CardRound> s32 = new List<CardRound>();
            // s32.Add(cardRound3_5);
            // s32.Add(cardRound3_6);
            // s32.Add(cardRound3_7);
            // s32.Add(cardRound3_8);

            List<List<CardRound>> bfarm3 = new List<List<CardRound>>();
            bfarm3.Add(sfarm31);
            // b3.Add(s32);

            cardRoundSetting = new List<List<List<CardRound>>>();
            cardRoundSetting.Add(bfarm1);
            cardRoundSetting.Add(bfarm2);
            cardRoundSetting.Add(bfarm3);
            return;
        }

        if (level == "Volcano")
        {
            Debug.Log("Volcano");
            // big round 1
            // small round 1
            CardRound cardRoundfarm_1 = new CardRound(0, 0, 1, 1);
            CardRound cardRoundfarm_2 = new CardRound(1, 1, 1, 2);
            CardRound cardRoundfarm_3 = new CardRound(4, 2, 2, 1);
            CardRound cardRoundfarm_4 = new CardRound(0, 3, 2, 2);
            List<CardRound> sfarm11 = new List<CardRound>();
            sfarm11.Add(cardRoundfarm_1);
            sfarm11.Add(cardRoundfarm_2);
            sfarm11.Add(cardRoundfarm_3);
            sfarm11.Add(cardRoundfarm_4);

            // small round 2
            CardRound cardRoundfarm_5 = new CardRound(2, 4, 1, 1);
            CardRound cardRoundfarm_6 = new CardRound(3, 5, 1, 2);
            CardRound cardRoundfarm_7 = new CardRound(2, 6, 2, 1);
            CardRound cardRoundfarm_8 = new CardRound(3, 7, 2, 2);
            List<CardRound> sfarm12 = new List<CardRound>();
            sfarm12.Add(cardRoundfarm_5);
            sfarm12.Add(cardRoundfarm_6);
            sfarm12.Add(cardRoundfarm_7);
            sfarm12.Add(cardRoundfarm_8);

            List<List<CardRound>> bfarm1 = new List<List<CardRound>>();
            bfarm1.Add(sfarm11);
            bfarm1.Add(sfarm12);

            // big round 2
            // small round 1
            CardRound cardRoundfarm2_1 = new CardRound(6, 8, 1, 1);
            CardRound cardRoundfarm2_2 = new CardRound(2, 9, 1, 2);
            CardRound cardRoundfarm2_3 = new CardRound(8, 10, 2, 1);
            CardRound cardRoundfarm2_4 = new CardRound(1, 11, 2, 2);
            List<CardRound> sfarm21 = new List<CardRound>();
            sfarm21.Add(cardRoundfarm2_1);
            sfarm21.Add(cardRoundfarm2_2);
            sfarm21.Add(cardRoundfarm2_3);
            sfarm21.Add(cardRoundfarm2_4);

            //     // small round 2
            // CardRound cardRound2_5 = new CardRound(2, 12, 1, 1);
            // CardRound cardRound2_6 = new CardRound(3, 13, 1, 2);
            // CardRound cardRound2_7 = new CardRound(4, 14, 2, 1);
            // CardRound cardRound2_8 = new CardRound(0, 15, 2, 2);
            // List<CardRound> s22 = new List<CardRound>();
            // s22.Add(cardRound2_5);
            // s22.Add(cardRound2_6);
            // s22.Add(cardRound2_7);
            // s22.Add(cardRound2_8);

            List<List<CardRound>> bfarm2 = new List<List<CardRound>>();
            bfarm2.Add(sfarm21);
            // b2.Add(s22);

            // big round 3
            // small round 1
            CardRound cardRoundfarm3_1 = new CardRound(5, 16, 1, 1);
            CardRound cardRoundfarm3_2 = new CardRound(4, 17, 1, 2);
            CardRound cardRoundfarm3_3 = new CardRound(7, 18, 2, 1);
            CardRound cardRoundfarm3_4 = new CardRound(4, 19, 2, 2);
            List<CardRound> sfarm31 = new List<CardRound>();
            sfarm31.Add(cardRoundfarm3_1);
            sfarm31.Add(cardRoundfarm3_2);
            sfarm31.Add(cardRoundfarm3_3);
            sfarm31.Add(cardRoundfarm3_4);

            // // small round 2
            // CardRound cardRound3_5 = new CardRound(2, 20, 1, 1);
            // CardRound cardRound3_6 = new CardRound(3, 21, 1, 2);
            // CardRound cardRound3_7 = new CardRound(4, 22, 2, 1);
            // CardRound cardRound3_8 = new CardRound(0, 23, 2, 2);
            // List<CardRound> s32 = new List<CardRound>();
            // s32.Add(cardRound3_5);
            // s32.Add(cardRound3_6);
            // s32.Add(cardRound3_7);
            // s32.Add(cardRound3_8);

            List<List<CardRound>> bfarm3 = new List<List<CardRound>>();
            bfarm3.Add(sfarm31);
            // b3.Add(s32);

            cardRoundSetting = new List<List<List<CardRound>>>();
            cardRoundSetting.Add(bfarm1);
            cardRoundSetting.Add(bfarm2);
            cardRoundSetting.Add(bfarm3);
            return;
        }
        // other scene, including tutorial and trail level use the same currently

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
        CardRound cardRound2_2 = new CardRound(2, 9,  1, 2);
        CardRound cardRound2_3 = new CardRound(8, 10,  2, 1);
        CardRound cardRound2_4 = new CardRound(1, 11,  2, 2);
        List<CardRound> s21 = new List<CardRound>();
        s21.Add(cardRound2_1);
        s21.Add(cardRound2_2);
        s21.Add(cardRound2_3);
        s21.Add(cardRound2_4);
        
        //     // small round 2
        // CardRound cardRound2_5 = new CardRound(2, 12, 1, 1);
        // CardRound cardRound2_6 = new CardRound(3, 13, 1, 2);
        // CardRound cardRound2_7 = new CardRound(4, 14, 2, 1);
        // CardRound cardRound2_8 = new CardRound(0, 15, 2, 2);
        // List<CardRound> s22 = new List<CardRound>();
        // s22.Add(cardRound2_5);
        // s22.Add(cardRound2_6);
        // s22.Add(cardRound2_7);
        // s22.Add(cardRound2_8);
        
        List<List<CardRound>> b2 = new List<List<CardRound>>();
        b2.Add(s21);
        // b2.Add(s22);
        
        // big round 3
        // small round 1
        CardRound cardRound3_1 = new CardRound(5, 16,  1, 1);
        CardRound cardRound3_2 = new CardRound(4, 17,  1, 2);
        CardRound cardRound3_3 = new CardRound(7, 18,  2, 1);
        CardRound cardRound3_4 = new CardRound(4, 19,  2, 2);
        List<CardRound> s31 = new List<CardRound>();
        s31.Add(cardRound3_1);
        s31.Add(cardRound3_2);
        s31.Add(cardRound3_3);
        s31.Add(cardRound3_4);
        
        // // small round 2
        // CardRound cardRound3_5 = new CardRound(2, 20, 1, 1);
        // CardRound cardRound3_6 = new CardRound(3, 21, 1, 2);
        // CardRound cardRound3_7 = new CardRound(4, 22, 2, 1);
        // CardRound cardRound3_8 = new CardRound(0, 23, 2, 2);
        // List<CardRound> s32 = new List<CardRound>();
        // s32.Add(cardRound3_5);
        // s32.Add(cardRound3_6);
        // s32.Add(cardRound3_7);
        // s32.Add(cardRound3_8);
        
        List<List<CardRound>> b3 = new List<List<CardRound>>();
        b3.Add(s31);
        // b3.Add(s32);

        cardRoundSetting = new List<List<List<CardRound>>>();
        cardRoundSetting.Add(b1);
        cardRoundSetting.Add(b2);
        cardRoundSetting.Add(b3);
    }

    private void SetEachCardUnderBlock(int _whichCard, int _index)
    {
        Debug.Log("Prefab/Prefabs/" + AllCards.cards[_whichCard]);
        GameObject temp = Resources.Load<GameObject>("Prefab/Prefabs/" + AllCards.cards[_whichCard]);
        Debug.Log("whichcardtemp" + temp);
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