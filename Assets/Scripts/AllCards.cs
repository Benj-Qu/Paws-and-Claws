using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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

    public bool staticOrRandom = true;
    private Dictionary<string, RandomCardDraw> selectionPool;

    public Dictionary<string, RandomCardDraw> terrainPool;
    public Dictionary<string, RandomCardDraw> powerupPool;
    public RandomCardDraw bombPool;

    private void Awake()
    {
  
        big_round_inc_event_subscription = EventBus.Subscribe<RoundTextDoneEvent>(OnRoundInc);

        _selectionPanel = Resources.Load<GameObject>("Prefab/SelectionPanel");
        _selectionPanel_2 = Resources.Load<GameObject>("Prefab/SelectionPanel_2");
        
        cards = new Dictionary<int, string>();
        
        // farm
        cards.Add(0, "crowAttack"); // TODO: only works for single sprite (not a split sprite inside a large sprite)
        cards.Add(1, "hideSpikeBlock");
        cards.Add(2, "rectangle");
        cards.Add(3, "square");
        cards.Add(24, "rectangle2");
        
        // china
        cards.Add(9, "panda");
        cards.Add(10, "bamboo_steamer");
        cards.Add(11, "Lantern");
        cards.Add(13, "square_china");
        cards.Add(14, "rectangle_china");
        cards.Add(27, "rectangle2_china");
        //24
        
        // winterland
        cards.Add(15, "TrapIce");
        cards.Add(16, "Snowman");
        cards.Add(18, "rectangle_snow");
        cards.Add(19, "square_snow");
        cards.Add(30, "rectangle2_snow");
        // also has ice
        //24
        
        // iceland
        cards.Add(17, "Ice");
        cards.Add(20, "penguin");
        cards.Add(21, "rectangle_ice");
        cards.Add(22, "square_ice");
        cards.Add(28, "rectangle2_ice");
        // also has 15
        //24
        
        // volcano
        cards.Add(23, "Trap");
        cards.Add(25, "rectangle_volcano");
        cards.Add(26, "square_volcano");
        cards.Add(29, "rectangle2_volcano");
        // also has spike 1
        // also has 2*1 24
        
        cards.Add(4, "bomb");
        
        cards.Add(5, "Coin");
        cards.Add(6, "Power Potion");
        cards.Add(7, "Speed Potion");
        cards.Add(8, "Invincible Potion");
        
        // TODO: add 2*1 block to the pool
        // cards.Add(31, );

        selectionPool = new Dictionary<string, RandomCardDraw>();
        terrainPool = new Dictionary<string, RandomCardDraw>();
        powerupPool = new Dictionary<string, RandomCardDraw>();
        
        bombPool = new RandomCardDraw(new List<int> {4});
        RandomCardDraw terrainPool_farm = new RandomCardDraw(new List<int> {0, 1, 2, 3, 24});
        RandomCardDraw terrainPool_china = new RandomCardDraw(new List<int> {9, 10, 11, 13, 14, 27});
        RandomCardDraw terrainPool_winterland = new RandomCardDraw(new List<int> {15, 16, 18, 19, 17, 30});
        RandomCardDraw terrainPool_iceland = new RandomCardDraw(new List<int> {17, 20, 21, 22, 15, 28});
        RandomCardDraw terrainPool_volcano = new RandomCardDraw(new List<int> {23, 25, 26, 1, 29});
        
        terrainPool.Add("Lantern Festival", terrainPool_china);
        terrainPool.Add("Winter Land", terrainPool_winterland);
        terrainPool.Add("Iceland", terrainPool_iceland);
        terrainPool.Add("Farm", terrainPool_farm);
        terrainPool.Add("Volcano", terrainPool_volcano);
        
        RandomCardDraw powerupPool_ = new RandomCardDraw(new List<int> {5, 6, 7, 8});
        powerupPool.Add("Lantern Festival", powerupPool_);
        powerupPool.Add("Winter Land", powerupPool_);
        powerupPool.Add("Iceland", powerupPool_);
        powerupPool.Add("Farm", powerupPool_);
        powerupPool.Add("Volcano", powerupPool_);
        
        RandomCardDraw pool = new RandomCardDraw(new List<int> {10, 13, 14, 9, 6, 8, 11, 5, 4, 7, 4, 27});
        RandomCardDraw pool1 = new RandomCardDraw(new List<int> {15, 16, 18, 19, 17, 30, 6, 8, 5, 7, 4, 4});
        RandomCardDraw pool2 = new RandomCardDraw(new List<int> {17, 20, 21, 22, 15, 28, 6, 8, 5, 7, 4, 4});
        RandomCardDraw pool3 = new RandomCardDraw(new List<int> {0, 1, 2, 3, 24, 6, 8, 5, 7});
        RandomCardDraw pool4 = new RandomCardDraw(new List<int> {23, 25, 26, 1, 29, 6, 8, 5, 7});
        selectionPool.Add("Lantern Festival", pool);
        selectionPool.Add("Winter Land", pool1);
        selectionPool.Add("Iceland", pool2);
        selectionPool.Add("Farm", pool3);
        selectionPool.Add("Volcano", pool4);

        // instantiate the blocks as the child of the block
        block = GameObject.Find("Block");
        if (block)
        {
            SetRound();
        }
    } // after that the start func of block controller will be called and load all the blocks.

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

    private void RandomSetRound()
    {
        string level = SceneManager.GetActiveScene().name;
        List<int> round_ = new List<int>();
        round_.Add(2);
        round_.Add(1);
        round_.Add(1);
        cardRoundSetting = CardRoundGenerate(round_, level);
    }
    
    private void SetRound()
    {
        string level = SceneManager.GetActiveScene().name;

        if (!staticOrRandom)
        {
            RandomSetRound();
            return;
        }
        
        if (level == "Lantern Festival")
        {
            // big round 1
            // small round 1
            CardRound cardRoundf1_1 = new CardRound(10, 0,  1, 1);
            CardRound cardRoundf1_2 = new CardRound(14, 1,  1, 2);
            CardRound cardRoundf1_3 = new CardRound(10, 2,  2, 1);
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
            CardRound cardRounda1_1 = new CardRound(20, 0, 1, 1);
            CardRound cardRounda1_2 = new CardRound(5, 1, 1, 2);
            CardRound cardRounda1_3 = new CardRound(1, 2, 2, 1);
            CardRound cardRounda1_4 = new CardRound(4, 3, 2, 2);
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
            CardRound cardRoundfarm_1 = new CardRound(23, 0, 1, 1);
            CardRound cardRoundfarm_2 = new CardRound(1, 1, 1, 2);
            CardRound cardRoundfarm_3 = new CardRound(24, 2, 2, 1);
            CardRound cardRoundfarm_4 = new CardRound(1, 3, 2, 2);
            List<CardRound> sfarm11 = new List<CardRound>();
            sfarm11.Add(cardRoundfarm_1);
            sfarm11.Add(cardRoundfarm_2);
            sfarm11.Add(cardRoundfarm_3);
            sfarm11.Add(cardRoundfarm_4);

            // small round 2
            CardRound cardRoundfarm_5 = new CardRound(25, 4, 1, 1);
            CardRound cardRoundfarm_6 = new CardRound(26, 5, 1, 2);
            CardRound cardRoundfarm_7 = new CardRound(25, 6, 2, 1);
            CardRound cardRoundfarm_8 = new CardRound(26, 7, 2, 2);
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
            CardRound cardRoundfarm2_4 = new CardRound(23, 11, 2, 2);
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
            CardRound cardRoundfarm3_4 = new CardRound(8, 19, 2, 2);
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

        else
        {
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

    public List<List<List<CardRound>>> CardRoundGenerate(List<int> roundSetting, string whichLevel)
    {
        int k = 0;
        List<List<List<CardRound>>> cardRoundResult = new List<List<List<CardRound>>>();
        
        RandomCardDraw rd = selectionPool[whichLevel];
        RandomCardDraw allrd = selectionPool[whichLevel];
        RandomCardDraw terrianRd = terrainPool[whichLevel];
        RandomCardDraw powerupRd = powerupPool[whichLevel];
        
        // big round
        int whichRound = 0;
        foreach (int i in roundSetting)
        {
            List<List<CardRound>> bf1 = new List<List<CardRound>>();
            if (whichRound == 0)
            {
                rd = terrianRd;
            }
            else if (whichRound == 1)
            {
                rd = allrd;
            }
            else
            {
                rd = powerupRd;
            }
            // small round
            for (int j = 0; j < i; j++)
            {
                CardRound cardRoundf1_1 = new CardRound(rd.GetRandomCard(), k,  1, 1);
                k++;
                CardRound cardRoundf1_2 = new CardRound(rd.GetRandomCard(), k,  1, 2);
                k++;
                CardRound cardRoundf1_3 = new CardRound(rd.GetRandomCard(), k,  2, 1);
                k++;
                CardRound cardRoundf1_4 = new CardRound(rd.GetRandomCard(), k,  2, 2);
                k++;
                List<CardRound> sf11 = new List<CardRound>();
                sf11.Add(cardRoundf1_1);
                sf11.Add(cardRoundf1_2);
                sf11.Add(cardRoundf1_3);
                sf11.Add(cardRoundf1_4);
                bf1.Add(sf11);
            }
            cardRoundResult.Add(bf1);
            whichRound++;
        }

        return cardRoundResult;
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

public class RandomCardDraw
{
    private List<int> pool;
    public RandomCardDraw(List<int> _pool)
    {
        pool = _pool;
    }

    public int GetRandomCard()
    {
        if (pool.Count == 1)
        {
            return pool[0];
        }
        
        // Create a random number generator with a seed based on a new GUID
        System.Random random = new System.Random(Guid.NewGuid().GetHashCode());

        // Generate a random integer between 0 and 5 (inclusive)
        int randomNumber = random.Next(0, pool.Count);
        
        return pool[randomNumber];
    }
}