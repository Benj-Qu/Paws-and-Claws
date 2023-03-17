using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTutorial : MonoBehaviour
{
    public GameObject FlagRight;
    public GameObject FlagLeft;
    public GameObject JumpBox1;
    public GameObject JumpBox2;
    public GameObject WallBox1;
    public GameObject WallBox2;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject HintText2;
    public int target = 0;


    // 1: basic, 2: jump, 3: wall jump, 4: finished;
    private int status = 1;

    // Start is called before the first frame update
    void Start()
    {
        FlagLeft.transform.position = new Vector3(-1.5f, -2.2f, 0);
        FlagRight.transform.position = new Vector3(1.5f, -2.2f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == 2 && status == 1)
        {
            JumpBox1.SetActive(true);
            JumpBox2.SetActive(true);
            Player1.transform.position = new Vector3(-6f, -2.2f, 0);
            Player2.transform.position = new Vector3(6f, -2.2f, 0);
            FlagLeft.transform.position = new Vector3(-1.2f, -1.2f, 0);
            FlagLeft.GetComponent<TutorialFlag>().touched = false;
            FlagRight.transform.position = new Vector3(1f, -1.2f, 0);
            FlagRight.GetComponent<TutorialFlag>().touched = false;
            target = 0;
            status += 1;
        }
        if (target == 2 && status == 2)
        {
            HintText2.SetActive(true);
            JumpBox1.SetActive(false);
            JumpBox2.SetActive(false);
            WallBox1.SetActive(true);
            WallBox2.SetActive(true);
            Player1.transform.position = new Vector3(-6f, -2.2f, 0);
            Player2.transform.position = new Vector3(6f, -2.2f, 0);
            FlagLeft.transform.position = new Vector3(-0.8f, 0f, 0);
            FlagLeft.GetComponent<TutorialFlag>().touched = false;
            FlagRight.transform.position = new Vector3(0.7f, 0f, 0);
            FlagRight.GetComponent<TutorialFlag>().touched = false;
            target = 0;
            status += 1;
        }
        if (target == 2 && status == 3)
        {
            target = 0;
            status += 1;
        }
    }
}
