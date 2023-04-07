using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour
{
    public GameObject story1;
    public GameObject story2;
    public GameObject story3;
    public GameObject story4;
    public GameObject story_cat;
    public GameObject story_dog;
    public GameObject mask;
    public GameObject story5;
    public GameObject scarecrow;

    public Animator story1_animator;
    public Text text;
    private Queue<string> scripts = new Queue<string>();
    int story_stage = 1;

    // Start is called before the first frame update
    void Start()
    {
        story1_animator.speed = 0.0f;
        StartCoroutine(Story1Coroutine());
        scripts.Enqueue("[speed=0.12]Once upon a time, there was a magic land,\nwhere cats and dogs lived together happily.");
        scripts.Enqueue("[speed=0.1]However, one day, a dark shadow loomed over the land,\n threatening to destroy their peaceful existence.");
        scripts.Enqueue("[speed=0.12]Ever since then, a Guardian would be selected\nevery year to protect the magic land.");
        scripts.Enqueue("[speed=0.12]This year, the competition is fierce between\n<size=45>DOGS</size> and <size=45>CATS</size>!");
        scripts.Enqueue("[speed=0.12]Ready to help DOGS or CATS become the next guardian?\n Start from the center of magic land!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowScript()
    {
        if (scripts.Count <= 0)
        {
            return;
        }
        text.TypeText(scripts.Dequeue(), onComplete: () => {
            Debug.Log("TypeText Complete");
            story_stage++;
            if(story_stage == 2)
            {
                StartCoroutine(Story2Coroutine());
            }
            if(story_stage == 3)
            {
                StartCoroutine(Story3Coroutine());
            }
            if(story_stage == 4)
            {
                StartCoroutine(Story4Coroutine());
            }
            if (story_stage == 5)
            {
                StartCoroutine(Story5Coroutine());
            }
        });

    }

    public void updateText(string text)
    {
        scripts.Enqueue(text);
        ShowScript();
    }

    public void OnClickWindow()
    {
        if (text.IsSkippable())
        {
            text.SkipTypeText();
        }
        else
        {
            ShowScript();
        }
    }

    private IEnumerator Story1Coroutine()
    {
        yield return new WaitForSeconds(5.0f);
        ShowScript();
        story1_animator.speed = 1.0f;
    }

    private IEnumerator Story2Coroutine()
    {
        yield return new WaitForSeconds(1.5f);
        ShowScript();
        yield return new WaitForSeconds(2f);
        story2.SetActive(true);
    }

    private IEnumerator Story3Coroutine()
    {
        yield return new WaitForSeconds(2f);
        story1.SetActive(false);
        story2.SetActive(false);
        story3.SetActive(true);
        Vector3 temp = text.rectTransform.anchoredPosition;
        temp.y = -200;
        text.rectTransform.anchoredPosition = temp;
        ShowScript();
    }

    private IEnumerator Story4Coroutine()
    {
        yield return new WaitForSeconds(2f);
        story3.SetActive(false);
        Color tmp = Color.black;
        tmp.a = 0.7f;
        mask.GetComponent<SpriteRenderer>().color = tmp;
        story4.SetActive(true);
        Vector3 temp = text.rectTransform.anchoredPosition;
        temp.y = 200;
        text.rectTransform.anchoredPosition = temp;
        text.color = Color.white;
        ShowScript();
        yield return new WaitForSeconds(6f);
        story_dog.SetActive(true);
        story_dog.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 3f, 0);
        yield return new WaitForSeconds(1f);
        story_cat.SetActive(true);
        story_cat.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 3f, 0);
    }

    private IEnumerator Story5Coroutine()
    {
        yield return new WaitForSeconds(2f);
        story4.SetActive(false);
        story5.SetActive(true);
        Vector3 temp = text.rectTransform.anchoredPosition;
        temp.y = 0;
        text.rectTransform.anchoredPosition = temp;
        ShowScript();
        yield return new WaitForSeconds(10.5f);
        scarecrow.SetActive(true);
    }

}
