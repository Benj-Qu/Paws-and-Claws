using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBlock : MonoBehaviour
{
    private GameObject Player1;
    private GameObject Player2;

    public bool underPlayer1;
    public bool underPlayer2;

    private void Start()
    {
        Player1 = GameObject.Find("player_1");
        Player2 = GameObject.Find("player_2");
        underPlayer1 = false;
        underPlayer2 = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject player = collision.gameObject;
        if (player.CompareTag("Player"))
        {
            ContactPoint2D hitpos = collision.GetContact(0);
            if (hitpos.normal.y < 0)
            {
                if (player.name == Player1.name)
                {
                    underPlayer1 = true;
                }
                else if (player.name == Player2.name)
                {
                    underPlayer2 = true;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject player = collision.gameObject;
        if (player.CompareTag("Player"))
        {
            if (player.name == Player1.name)
            {
                underPlayer1 = false;
            }
            else if (player.name == Player2.name)
            {
                underPlayer2 = false;
            }
        }
    }

    private void OnDestroy()
    {
        if (underPlayer1)
        {
            Player1.GetComponent<PlayerController>().LeaveFloor();
        }
        if (underPlayer2)
        {
            Player2.GetComponent<PlayerController>().LeaveFloor();
        }
    }
}
