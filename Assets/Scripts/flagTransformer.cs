using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagTransformer : MonoBehaviour
{
    public Sprite flag_UM;
    public Sprite flag_Ohio;
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
        if (other.name == player_1)
        {
            spriteRenderer.sprite = flag_UM;
            gameController.ChangeScore(-state);
            state = 1;
            gameController.ChangeScore(state);
        }
        if (other.name == player_2)
        {
            spriteRenderer.sprite = flag_Ohio;
            gameController.ChangeScore(-state);
            state = -1;
            gameController.ChangeScore(state);
        }
    }
}
