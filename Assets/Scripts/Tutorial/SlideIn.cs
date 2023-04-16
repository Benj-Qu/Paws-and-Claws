using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlideIn : MonoBehaviour
{
    public Vector3 guardian_start_pos = new Vector3(12f, -3.54f, 0f);
    public Vector3 dialogue_start_pos = new Vector3(-20f, 0f, 0f);
    public Vector3 guardian_end_pos = new Vector3(6.58f, -3.54f, 0f);
    public Vector3 dialogue_end_pos = new Vector3(0f, 0f, 0f);
    public textController FarmStoryText;
    public float speed = 100f;
    public TextMeshProUGUI storyHint;
    public GameObject FarmStage1Texts;
    public GameObject FarmStage2Texts;
    public GameObject FarmStage3Texts;
    public GameObject SelectionPanel;
    public GameObject Follower;
    public GameObject ScorePanel;
    public GameObject flagFollower;


    private bool updatedText = false;
    private int current_speaking = -1;

    // Start is called before the first frame update
    void Start()
    {
        InPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(current_speaking != GameController.instance.guardian_speaking)
        {
            current_speaking = GameController.instance.guardian_speaking;
            updatedText = false;
        }
        if (GameController.instance.guardian_speaking == 0)
        {
            // slide in
            if(name == "DialogueBox")
            {
                if (GameController.instance.stage == 1 && SelectionPanel == null)
                {
                    // deactivate SelectionPanel when guardian is speaking
                    SelectionPanel = GameObject.Find("SelectionPanel(Clone)");
                    if (SelectionPanel)
                    {
                        SelectionPanel.SetActive(false);
                    }
                    // deactivate follower when guardian is speaking
                    if (Follower)
                    {
                        Follower.SetActive(false);
                    }
                }
                transform.position = Vector3.MoveTowards(transform.position, dialogue_end_pos, speed * Time.deltaTime);
            }
            if(name == "guardian")
            {
                transform.position = Vector3.MoveTowards(transform.position, guardian_end_pos, speed * Time.deltaTime);
            }
            if (name == "DialogueBox" && Mathf.Abs(transform.position.x - dialogue_end_pos.x) < 0.5 && updatedText == false)
            {
                updatedText = true;
                StartCoroutine(UpdateStoryTextCoroutine(GameController.instance.stage));
            }
        }

        if (GameController.instance.guardian_speaking == 2)
        {
            // slide out
            if (name == "DialogueBox" && Mathf.Abs(transform.position.x - dialogue_start_pos.x) < 16 && updatedText == false)
            {
                updatedText = true;
                StartCoroutine(UpdateStoryTextCoroutine(-100));
                if(GameController.instance.stage == 0)
                {
                    GameController.instance.tutorialCall();
                    FarmStage3Texts.SetActive(true);
                    ScorePanel = GameObject.Find("ScorePanel");
                    if (ScorePanel) ScorePanel.SetActive(false);
                }
                if (GameController.instance.stage == -2)
                {
                    FarmStage1Texts.SetActive(true);
                }
                else if (GameController.instance.stage == -1)
                {
                    FarmStage2Texts.SetActive(true);
                }
                else
                {
                    FarmStage3Texts.SetActive(true);
                    if (GameController.instance.stage == 2)
                    {
                        if(ScorePanel) ScorePanel.SetActive(true);
                    }
                    if(GameController.instance.stage == 1)
                    {
                        flagFollower.SetActive(true);
                    }
                }
            }
            if (name == "DialogueBox")
            {
                transform.position = Vector3.MoveTowards(transform.position, dialogue_start_pos, speed * Time.deltaTime);
            }
            if (name == "guardian")
            {
                transform.position = Vector3.MoveTowards(transform.position, guardian_start_pos, speed * Time.deltaTime);
            }
        }
    }

    void InPosition()
    {
        if (name == "DialogueBox") transform.position = dialogue_start_pos;
        if (name == "guardian") transform.position = guardian_start_pos;
    }

    IEnumerator UpdateStoryTextCoroutine(int stage)
    {
        Debug.Log("stage: " + stage);
        if (stage == -100)
        {
            FarmStoryText.updateText("");
            storyHint.enabled = false;
            if(GameController.instance.stage == 1)
            {
                SelectionPanel.SetActive(true);
                Follower.SetActive(true);
            }
        }
        yield return new WaitForSeconds(0.6f);
        if (stage == -2)
        {
            FarmStoryText.updateText("[speed=0.08]<b>Welcome candidates!\n You'll learn skills to help you win the game.</b>");
        }
        if (stage == -1)
        {
            FarmStoryText.updateText("[speed=0.08]<b>Well done! Next thing:\n ATTACKING!</b>");
        }
        if (stage == 0)
        {
            FarmStoryText.updateText("[speed=0.08]<b>BLOCKS can help you seize more FLAGS:\n Choose wisely!</b>");
        }
        if (stage == 1)
        {
            FarmStoryText.updateText("[speed=0.08]<b>Build your path towards the FLAGS!</b>");
        }
        if (stage == 2)
        {
            FarmStoryText.updateText("[speed=0.08]<b>Good job! MORE FLAGS MORE SCORE!</b>");
        }
    }


}
