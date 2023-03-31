using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeDisplayer : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float countdownTime;
    public float maxTime = 45f;
    public bool gameStarted = false;
    private GameObject Player1;
    private GameObject Player2;

    private GameController gameController;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        Player1 = GameObject.Find("player_1");
        Player2 = GameObject.Find("player_2");
    }


    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            // time up
            if (countdownTime <= 0)
            {
                countdownTime = 0;
                if (gameController.stage == 2 && (gameController.round_big == 3 || gameController.level == "Farm"))
                {
                    gameController.GameOver();
                    // destroy itself
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("stage, progressbar");
                    Debug.Log("progress call");
                    gameController.StartGame();
                    countdownTime = maxTime;
                }
            }
            // Party Time!
            else if (countdownTime <= 10)
            {
                Player1.GetComponent<PlayerScore>().Fierce();
                Player2.GetComponent<PlayerScore>().Fierce();
            }
        
            // decrement time
            countdownTime -= Time.deltaTime;
        }

        if (countdownTime <= 0)
        {
            gameController.GameOver();
            countdownTime = 0;
        }
        if (text)
        {
            text.text = "Time: " + Mathf.Floor(countdownTime).ToString() + " s";
        }
    }
    
    public void StartGame()
    {
        if (GameController.instance.level == "Farm")
        {
            maxTime = 30;
        }
        else
        {
            maxTime = 45;
        }
        gameStarted = true;
        countdownTime = maxTime;
    }
}