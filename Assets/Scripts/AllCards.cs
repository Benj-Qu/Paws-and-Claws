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
        
        // example with id 0, 1, 2, 3
        cards.Add(0, "flag_ohio"); // TODO: only works for single sprite (not a split sprite inside a large sprite)
        cards.Add(1, "flag_um");
        cards.Add(2, "IMG_1828");
        cards.Add(3, "IMG_1829");
    }
}