using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplayer : MonoBehaviour
{
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public float countdownTime = 60f;

    private GameController gameController;
    // private HasInventory inventory;
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        // inventory = GameObject.Find("Player").GetComponent<HasInventory>();
    }


    // Update is called once per frame
    void Update()
    {
        var scores = gameController.GetScores();
        if (text1)
        {
            text1.text = "Score: " + scores.Item1.ToString("F2");
        }
        if (text2)
        {
            text2.text = "Score: " + scores.Item2.ToString("F2");
        }
    }
}
