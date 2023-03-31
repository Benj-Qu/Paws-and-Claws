using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar_Main : MonoBehaviour
{
    public TextMeshProUGUI countTime;
    public GameObject choice;

    private Slider _slider;
    // private CardSelection _cardSelection;
    private GameController gameController;
    private bool gameStarted = false; // If the game starts

    private GameObject Player1;
    private GameObject Player2;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        Debug.Log("_slider: " + _slider);
        // _cardSelection = choice.GetComponent<CardSelection>();
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
            if (_slider.value <= 0)
            {
                _slider.value = 0;
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
                    _slider.value = _slider.maxValue;
                }
            }
            // Party Time!
            else if (_slider.value <= 10)
            {
                Player1.GetComponent<PlayerScore>().Fierce();
                Player2.GetComponent<PlayerScore>().Fierce();
            }
        
            // decrement time
            _slider.value -= Time.deltaTime;
        
            // modify text
            countTime.text = Mathf.Floor(_slider.value).ToString() + " s";
        }
    }
    
    private void OnDestroy()
    {
        // TODO: upon destroy, send a signal to the GameController
        // _cardSelection.SetTimeUp();
    }

    public void StartGame()
    {
        if (GameController.instance.level == "Farm")
        {
            _slider.maxValue = 30;
        }
        else
        {
            _slider.maxValue = 60;
        }
        gameStarted = true;
        _slider.value = _slider.maxValue;
    }
}
