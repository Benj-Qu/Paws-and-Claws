using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMenuSelection : MonoBehaviour
{
    private float joystickInputy = 0f;
    private int activeButton = 0;
    private int numButton;

    private bool respond = true;
    private float notRespondTime = 0.08f;
    private float notRespondTimer = 0f;

    public GameObject[] ButtonList;
    public LevelButton levelButton;
    // Start is called before the first frame update
    void Start()
    {
        numButton = ButtonList.Length;
    }

    // Update is called once per frame
    void Update()
    {
        joystickInputy = Input.GetAxis("Vertical1") + Input.GetAxis("Vertical2");
        if (respond)
        {
            respond = false;
        }
        else
        {
            if (joystickInputy != 0)
            {
                notRespondTimer += Time.deltaTime;
                if (notRespondTimer >= notRespondTime)
                {
                    notRespondTimer = 0f;
                    respond = true;
                }
            }
            joystickInputy = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || joystickInputy < 0)
        {
            if (activeButton > 0)
            {
                activeButton--;
                gameObject.transform.position = ButtonList[activeButton].transform.position;
            }
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || joystickInputy > 0)
        {
            if (activeButton < numButton - 1)
            {
                activeButton++;
                gameObject.transform.position = ButtonList[activeButton].transform.position;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("A1") || Input.GetButtonDown("A2"))
        {
            SceneManager.LoadScene(ButtonList[activeButton].name);
        }
    }
}
