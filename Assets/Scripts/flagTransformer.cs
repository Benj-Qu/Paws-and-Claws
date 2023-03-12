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

    private Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == player_1)
        {
            spriteRenderer.sprite = flag_UM;
            inventory.ChangeScore(-state);
            state = 1;
            inventory.ChangeScore(state);
        }
        if (other.name == player_2)
        {
            spriteRenderer.sprite = flag_Ohio;
            inventory.ChangeScore(-state);
            state = -1;
            inventory.ChangeScore(state);
        }
    }
}
