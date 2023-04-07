using System;
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
    private Subscription<CameraEvent> camera_event;

    private void Awake()
    {
        Debug.Log("timedisplayer sub");
        camera_event = EventBus.Subscribe<CameraEvent>(OnCameraEvent);
    }

    private void OnCameraEvent(CameraEvent e)
    {
        if (e.startOrFinish == false) // finished
        {
            gameStarted = true;
        }
    }

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
        if (gameStarted && !(GameController.instance.level == "Farm" && GameController.instance.stage == 1))
        {
            // time up
            if (countdownTime <= 0)
            {
                countdownTime = 0;
                gameStarted = false;
                if (gameController.stage == 2 && (gameController.round_big == 3 || gameController.level == "Farm"))
                {
                    gameController.GameOver();
                    // destroy itself
                    text.text = "";
                    countdownTime = 45;
                    gameStarted = false;
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
        if (text)
        {
            text.text = "Time: " + Mathf.Floor(countdownTime).ToString() + " s";
        }
    }
    
    public void StartGame()
    {
        if (GameController.instance.level == "Farm" && GameController.instance.stage == 2)
        {
            maxTime = 30;
            // if (GameController.instance.stage == )
        }
        else
        {
            maxTime = 45;
        }
        countdownTime = maxTime;
        Debug.Log(camera_event);
        Debug.Log("gamestarted:" + gameStarted);
        // gameStarted = true;
    }

    public void StartFight()
    {
        StartGame();
        gameStarted = true;
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(camera_event);
    }
}