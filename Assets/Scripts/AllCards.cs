using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllCards : MonoBehaviour
{
    // id, name
    public static Dictionary<int, string> cards;

    private void Awake()
    {
        cards = new Dictionary<int, string>();
        
        cards.Add(0, "scarecrow_"); // TODO: only works for single sprite (not a split sprite inside a large sprite)
        cards.Add(1, "hideSpike_");
        cards.Add(2, "rectangle_");
        cards.Add(3, "square_");
        cards.Add(4, "bomb_");

        // cards.Add(0, "scarecrow_");
        // cards.Add(1, "hideSpike_");
        // cards.Add(2, "rectangle_");
        // cards.Add(3, "square_");
        // cards.Add(4, "bomb_");
        // cards.Add(5, "scarecrow_");
        // cards.Add(6, "rectangle_");
        // cards.Add(7, "square_");
    }
}