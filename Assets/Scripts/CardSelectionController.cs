using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectionController : MonoBehaviour
{
    public int activeButton = 0;
    private float joystickInputx = 0f;
    private int numButton;

    private bool respond = true;
    private float notRespondTime = 0.08f;
    private float notRespondTimer = 0f;

    public GameObject[] ButtonList;
    private PlayerController pc;

    public string joystickNum;

    public GameObject image;
    // Start is called before the first frame update
    void Start()
    {
        numButton = ButtonList.Length;
        pc = GameObject.Find("player_" + joystickNum).GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        joystickInputx = Input.GetAxis("Horizontal" + joystickNum);
        if (respond)
        {
            respond = false;
        }
        else
        {
            if (joystickInputx != 0)
            {
                notRespondTimer += Time.deltaTime;
                if (notRespondTimer >= notRespondTime)
                {
                    notRespondTimer = 0f;
                    respond = true;
                }
            }
            joystickInputx = 0;
        }
        
        if (Input.GetKeyDown(pc.LeftButton) || joystickInputx < 0)
        {
            if (activeButton > 0)
            {
                activeButton--;
                image.transform.position = ButtonList[activeButton].transform.position;
            }
        }
        if (Input.GetKeyDown(pc.RightButton) || joystickInputx > 0)
        {
            if (activeButton < numButton - 1)
            {
                activeButton++;
                image.transform.position = ButtonList[activeButton].transform.position;
            }
        }

        // if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("A1") || Input.GetButtonDown("A2"))
        // {
        //     if (activeButton == 0)
        //     {
        //         levelButton.ExitGame();
        //     } else if (activeButton == 1)
        //     {
        //         levelButton.RestartGame();
        //     }
        //     else
        //     {
        //         levelButton.Cancel();
        //     }
        // }
    }

    public void Reset()
    {
        activeButton = 0;
        image.transform.position = ButtonList[0].transform.position;
    }

    public void Disappear()
    {
        image.SetActive(false);
    }
    
    // public void
}
