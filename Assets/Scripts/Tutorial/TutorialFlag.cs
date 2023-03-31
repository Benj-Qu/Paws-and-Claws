using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFlag : MonoBehaviour
{
    public TutorialController tc;
    public bool touched = false;
    public Sprite flag_UM;
    public Sprite flag_Ohio;
    public GameObject WinLeft;
    public GameObject WinRight;
    public string player_1 = "player_1";
    public string player_2 = "player_2";
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && touched == false)
        {
            tc.flags += 1;
            touched = true;
            if (collision.name == player_1)
            {
                spriteRenderer.sprite = flag_UM;
                WinLeft.SetActive(true);
            }
            if (collision.name == player_2)
            {
                spriteRenderer.sprite = flag_Ohio;
                WinRight.SetActive(true);
            }
        }
    }
}
