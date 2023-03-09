using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuelDisplayer : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float countdownTime = 60f;

    private GameController gameController;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }


    // Update is called once per frame
    void Update()
    {
        countdownTime -= Time.deltaTime;

        if (countdownTime <= 0)
        {
            gameController.GameOver();
            countdownTime = 0;
        }
        if (text)
        {
            text.text = "Time remain: " + countdownTime.ToString("F2") + "s";
        }
    }
}