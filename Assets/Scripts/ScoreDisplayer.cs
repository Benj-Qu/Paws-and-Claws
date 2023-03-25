using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplayer : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    private PlayerScore Score1;
    private PlayerScore Score2;
    // private HasInventory inventory;
    void Start()
    {
        Score1 = GameObject.Find("player_1").GetComponent<PlayerScore>();
        Score2 = GameObject.Find("player_2").GetComponent<PlayerScore>();
    }


    // Update is called once per frame
    void Update()
    {
        if (text1)
        {
            text1.text = "Score: " + Score1.getScore().ToString("G");
        }
        if (text2)
        {
            text2.text = "Score: " + Score2.getScore().ToString("G");
        }
    }

    public void Reset()
    {
        Score1.reset();
        Score2.reset();
    }
}
