using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagTransformer : MonoBehaviour
{
    public Sprite flag_UM;
    public Sprite flag_Ohio;
    public Sprite flag_white;

    private GameObject owner;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        owner = null;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Modify Flag Color
        if (other.name == "player_1")
        {
            spriteRenderer.sprite = flag_UM;
            UpdateFlagNum(other.gameObject);
        }
        else if (other.name == "player_2")
        {
            spriteRenderer.sprite = flag_Ohio;
            UpdateFlagNum(other.gameObject);
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
