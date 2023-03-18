using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagTransformer : MonoBehaviour
{
    public Sprite flag_UM;
    public Sprite flag_Ohio;
    public Sprite flag_white;
    public AudioClip player_1_audio;
    public AudioClip player_2_audio;

    private GameObject owner;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        owner = null;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(GameController.instance.stage == 2)
        {
            // Modify Flag Color
            if (other.name == "player_1")
            {
                spriteRenderer.sprite = flag_UM;
                AudioSource.PlayClipAtPoint(player_1_audio, Camera.main.transform.position);
                UpdateFlagNum(other.gameObject);
            }
            else if (other.name == "player_2")
            {
                spriteRenderer.sprite = flag_Ohio;
                AudioSource.PlayClipAtPoint(player_2_audio, Camera.main.transform.position);
                UpdateFlagNum(other.gameObject);
            }
        } 
    }

    private void UpdateFlagNum(GameObject other)
    {
        if (owner == null)
        {
            owner = other;
            owner.GetComponent<PlayerScore>().getFlag();
        }
        else if (owner != other)
        {
            owner.GetComponent<PlayerScore>().loseFlag();
            owner = other;
            owner.GetComponent<PlayerScore>().getFlag();
        }
    }

    private void OnDisable()
    {
        owner = null;
        spriteRenderer.sprite = flag_white;
    }
}
