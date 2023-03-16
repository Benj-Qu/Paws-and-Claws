using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagTransformer : MonoBehaviour
{
    public Sprite flag_UM;
    public Sprite flag_Ohio;
    public Sprite flag_white;
    public string player_1 = "player_1";
    public string player_2 = "player_2";
    private SpriteRenderer spriteRenderer;

    private int state = 0; // record its owner, 1 for player1 and -1 for player2

    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("color flag: " + other.name);
        if (other.name == player_1)
        {
            spriteRenderer.sprite = flag_UM;
            gameController.ChangeFlagCount(state, -1);
            state = 1;
            gameController.ChangeFlagCount(state, 1);
        }
        if (other.name == player_2)
        {
            spriteRenderer.sprite = flag_Ohio;
            gameController.ChangeFlagCount(state, -1);
            state = -1;
            gameController.ChangeFlagCount(state, 1);
        }
    }

    private void OnDisable()
    {
        state = 0;
        spriteRenderer.sprite = flag_white;
    }
}
