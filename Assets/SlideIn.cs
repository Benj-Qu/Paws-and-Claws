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
            Debug.Log("current_speaking: " + current_speaking);
        }
        if (GameController.instance.guardian_speaking == 0)
        {
            // slide in
            Debug.Log("guardian speaking: " + GameController.instance.guardian_speaking);
            if(name == "DialogueBox")
            {
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
            Debug.Log("guardian speaking: " + GameController.instance.guardian_speaking);
            if (name == "DialogueBox" && Mathf.Abs(transform.position.x - dialogue_start_pos.x) < 16 && updatedText == false)
            {
                updatedText = true;
                StartCoroutine(UpdateStoryTextCoroutine(-100));
                if(GameController.instance.stage == 0)
                {
                    GameController.instance.tutorialCall();
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
        }
        yield return new WaitForSeconds(0.6f);
        if (stage == -2)
        {
            FarmStoryText.updateText("[speed=0.08]<b>Guadians are strong.\n They can climb high walls!</b>");
        }
        if (stage == -1)
        {
            FarmStoryText.updateText("[speed=0.08]<b>Guardians possess exceptional combat skills.\n They can attack opponents!</b>");
        }
        if (stage == 0)
        {
            FarmStoryText.updateText("[speed=0.08]<b>Guadians are smart.\n They can select suitable blocks for themselves!</b>");
        }
        if (stage == 1)
        {
            FarmStoryText.updateText("[speed=0.08]<b>Guardians are excellent planners.\n They can build perfect path towards flags!</b>");
        }
        if (stage == 2)
        {
            FarmStoryText.updateText("[speed=0.08]<b>Game Start!\n MORE FLAGS MORE SCORE!</b>");
        }
    }


}
