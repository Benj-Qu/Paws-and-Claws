using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0Control : MonoBehaviour
{
    public GameObject Flag Hint;
    //public GameObject MoveHint1;
    //public GameObject MoveHint2;
    //public GameObject PlaceHint1;
    //public GameObject PlaceHint2;
    
    //[SerializeField] private float player1_stuck = 0f;
    //[SerializeField] private float player2_stuck = 0f;
    //private Vector3 player1_last;
    //private Vector3 player2_last;
    //private int count_last;

    private GameObject player1;

    private GameObject player2;

    private GameController gameController;

    //private blockController bc;
    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("player_1");
        player2 = GameObject.Find("player_2");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        bc = GameObject.Find("Block").GetComponent<blockController>();
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
                player1_stuck = 0;
                MoveHint1.SetActive(false);
                PlaceHint1.SetActive(false);
            }
            if (player2.transform.position == player2_last)
            {
                player2_stuck += Time.deltaTime;
            }
            else
            {
                player2_last = player2.transform.position;
                player2_stuck = 0;
                MoveHint2.SetActive(false);
                PlaceHint2.SetActive(false);
            }
        } else if (gameController.stage == 1)
        {
            if (bc.count == count_last)
            {
                player2_stuck += Time.deltaTime;
                player1_stuck += Time.deltaTime;
            }
            else
            {
                count_last = bc.count;
                player2_stuck = 0;
                player1_stuck = 0;
                MoveHint1.SetActive(false);
                PlaceHint1.SetActive(false);
                MoveHint2.SetActive(false);
                PlaceHint2.SetActive(false);
            }
        }
        

        if (player1_stuck > 5f)
        {
            Debug.Log("Stage: " + gameController.stage);
            if (gameController.stage == 2)
            {
                MoveHint1.SetActive(true);
            } else if (gameController.stage == 1)
            {
                PlaceHint1.SetActive(true);
            }
            // player1_stuck = 0;
        }

        if (player2_stuck > 5f)
        {
            Debug.Log("Stage: " + gameController.stage);
            if (gameController.stage == 2)
            {
                MoveHint2.SetActive(true);
            } else if (gameController.stage == 1)
            {
                PlaceHint2.SetActive(true);
            }

            // player2_stuck = 0;
        }
    }
}
