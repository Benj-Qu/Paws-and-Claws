using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Card> cards;
    public List<int> amounts;
    public int cardAddedFront = 0;
    public int cardAddedBack = 0;

    // Start is called before the first frame update
    void Start()
    {
        cards = new List<Card>();
        amounts = new List<int>();
        
        foreach (Transform child in transform)
        {
            cards.Add(child.GetChild(0).GetComponent<Card>());
            amounts.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
