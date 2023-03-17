using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0Control : MonoBehaviour
{
    public GameObject hint1;
    public GameObject hint2;
    
    private float player1_stuck = 0f;
    private float player2_stuck = 0f;
    private Vector3 player1_last;
    private Vector3 player2_last;

    private GameObject player1;

    private GameObject player2;

    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("player_1");
        player2 = GameObject.Find("player_2");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.stage == 2)
        {
            if (player1.transform.position == player1_last)
            {
                player1_stuck += Time.deltaTime;
            }
            else
            {
                player1_last = player1.transform.position;
            }
            if (player2.transform.position == player2_last)
            {
                player2_stuck += Time.deltaTime;
            }
            else
            {
                player2_last = player2.transform.position;
            }

            if (player1_stuck > 3f)
            {
                hint1.SetActive(true);
            }
        
            if (player2_stuck > 3f)
            {
                hint2.SetActive(true);
            }
        }
    }
}
