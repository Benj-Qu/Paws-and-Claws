using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPotionController : MonoBehaviour
{
    public float LastPeriod = 5f;
    public float SpeedUp = 1.5f;
    public float JumpUp = 1.5f;
    public float SizeUp = 2f;
    public bool invincible = false;

    public AudioClip PowerUp;

    private GameController gc;

    private void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = collision.gameObject;
        if (player.CompareTag("Player") && gc.stage == 2)
        {
            AudioSource.PlayClipAtPoint(PowerUp, Camera.main.transform.position);
            player.GetComponent<PlayerController>().PowerUp(LastPeriod, SpeedUp, JumpUp, SizeUp, invincible);
            Destroy(gameObject);
        }
    }
}
