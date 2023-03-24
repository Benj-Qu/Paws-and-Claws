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
    public ShowAddScore showAddScore;

    private GameObject owner;
    private SpriteRenderer spriteRenderer;
    private float delta_time = 0f;

    void Start()
    {
        owner = null;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        delta_time += Time.deltaTime;
        // Show "+1"
        if (owner is not null && delta_time >= 0.3334f)
        {
            showAddScore.ShowScore();
            delta_time = 0f;
        }
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
                delta_time = 0f;
            }
            else if (other.name == "player_2")
            {
                spriteRenderer.sprite = flag_Ohio;
                AudioSource.PlayClipAtPoint(player_2_audio, Camera.main.transform.position);
                UpdateFlagNum(other.gameObject);
                delta_time = 0f;
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
