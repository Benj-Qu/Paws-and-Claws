using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float CoinScore = 10f;

    public AudioClip CollectCoin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = collision.gameObject;
        if (player.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(CollectCoin, Camera.main.transform.position);
            player.GetComponent<PlayerScore>().updateScore(CoinScore);
            Destroy(gameObject);
        }
    }
}
