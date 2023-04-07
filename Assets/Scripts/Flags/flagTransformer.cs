using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagTransformer : MonoBehaviour
{
    public Sprite flag_1;
    public Sprite flag_2;
    public Sprite flag_white;
    public AudioClip player_1_audio;
    public AudioClip player_2_audio;
    public ShowAddScore showAddScore;
    public float ControlPeriod = 5f;

    private GameObject owner;
    private SpriteRenderer spriteRenderer;
    private float delta_time = 0f;
    private float control_time = 0f;

    private bool player_1_on = false;
    private bool player_2_on = false;

    void Start()
    {
        owner = null;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        delta_time += Time.deltaTime;
        if (owner is not null && delta_time >= (1f / 3f))
        {
            showAddScore.ShowScore();
            delta_time = 0f;
        }
        if (owner && !OwnerOn())
        {
            control_time += Time.deltaTime;
            Color tmp = GetComponent<SpriteRenderer>().color;
            tmp.a = (ControlPeriod - control_time) / ControlPeriod;
            GetComponent<SpriteRenderer>().color = tmp;
            if (control_time > ControlPeriod)
            {
                reset();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(GameController.instance.stage == 2 && other.CompareTag("Player"))
        {
            // Update player_on
            if (other.name == "player_1")
            {
                player_1_on = true;
            }
            if (other.name == "player_2")
            {
                player_2_on = true;
            }
            // Determine Ownership
            if (OwnerOn() && (other.gameObject != owner))
            {
                return;
            }
            else if (other.name == "player_1")
            {
                Color tmp = GetComponent<SpriteRenderer>().color;
                tmp.a = 1f;
                GetComponent<SpriteRenderer>().color = tmp;
                spriteRenderer.sprite = flag_1;
                AudioSource.PlayClipAtPoint(player_1_audio, Camera.main.transform.position);
                showAddScore.SetColor(1);
                UpdateFlagNum(other.gameObject);
                control_time = 0f;
                delta_time = 0f;
            }
            else if (other.name == "player_2")
            {
                Color tmp = GetComponent<SpriteRenderer>().color;
                tmp.a = 1f;
                GetComponent<SpriteRenderer>().color = tmp;
                spriteRenderer.sprite = flag_2;
                AudioSource.PlayClipAtPoint(player_2_audio, Camera.main.transform.position);
                showAddScore.SetColor(2);
                UpdateFlagNum(other.gameObject);
                control_time = 0f;
                delta_time = 0f;
            }
        } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "player_1")
        {
            player_1_on = false;
            if (player_2_on)
            {
                GameObject player2 = GameObject.Find("player_2");
                spriteRenderer.sprite = flag_2;
                AudioSource.PlayClipAtPoint(player_2_audio, Camera.main.transform.position);
                showAddScore.SetColor(2);
                UpdateFlagNum(player2);
                control_time = 0f;
                delta_time = 0f;
            }
        }
        if (other.name == "player_2")
        {
            player_2_on = false;
            if (player_1_on)
            {
                GameObject player1 = GameObject.Find("player_1");
                spriteRenderer.sprite = flag_1;
                AudioSource.PlayClipAtPoint(player_1_audio, Camera.main.transform.position);
                showAddScore.SetColor(1);
                UpdateFlagNum(player1);
                control_time = 0f;
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

    private void reset()
    {
        if (owner)
        {
            owner.GetComponent<PlayerScore>().loseFlag();
            owner = null;
        }
        spriteRenderer.sprite = flag_white;
        player_1_on = false;
        player_2_on = false;
        delta_time = 0f;
        control_time = 0f;
    }

    private void OnDisable()
    {
        reset();
    }

    public void StartPartyTime()
    {
        foreach(Transform child in transform)
        {
            if(child.name == "FlagPartyEffect")
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void EndPartyTime()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "FlagPartyEffect")
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private bool OwnerOn()
    {
        if (owner)
        {
            if (owner.name == "player_1")
            {
                return player_1_on;
            }
            if (owner.name == "player_2")
            {
                return player_2_on;
            }
        }
        return false;
    }
}
