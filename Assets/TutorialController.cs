using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject ExitMenu;
    public PlayerController pc1;
    public PlayerController pc2;
    public GameObject p1;
    public GameObject p2;
    public GameObject WinLeft;
    public GameObject WinRight;
    public GameObject Stage1Objects;
    public GameObject Stage1Texts;
    public GameObject Stage2Objects;
    public GameObject Stage2Texts;

    public int stage = 1; // 1: movement, 2: attack, 3: selection, 4: placement
    public int flags = 0;
    public bool pause = false;

    // Start is called before the first frame update
    void Start()
    {
        pc1.activate();
        pc2.activate();
        ResetPlayers();
        Stage1Objects.SetActive(true);
        Stage1Texts.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("X1") || Input.GetButtonDown("X2"))
        {
            if (!pause)
            {
                ExitMenu.SetActive(true);
                Time.timeScale = 0f;
                pause = true;
            }
            else
            {
                ExitMenu.SetActive(false);
                Time.timeScale = 1f;
                pause = false;
            }
        }
        if (stage == 1 && flags == 2)
        {
            flags = 0;
            StartCoroutine(finishStage1());
        }
        if(stage == 2 && flags == 0)
        {
            flags = 0;
            StartCoroutine(finishStage2());
        }
    }

    IEnumerator finishStage1()
    {
        yield return new WaitForSeconds(2f);
        WinLeft.SetActive(false);
        WinRight.SetActive(false);
        Stage1Objects.SetActive(false);
        Stage1Texts.SetActive(false);
        ResetPlayers();
        stage = 2;
    }

    IEnumerator finishStage2()
    {
        yield return new WaitForSeconds(2f);
        Stage2Objects.SetActive(false);
        Stage2Texts.SetActive(false);
        ResetPlayers();
        stage = 3;
    }

    public void ResetPlayers()
    {
        p1.transform.position = GameObject.Find("StartPoint").transform.position;
        p2.transform.position = GameObject.Find("StartPoint2").transform.position;
    }

    public void Killed(GameObject player)
    {

    }
}
